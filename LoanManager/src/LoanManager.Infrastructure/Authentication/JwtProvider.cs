using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoanManager.Application.Abstractions.Authentication;
using LoanManager.Application.Abstractions.Data;
using LoanManager.Domain.Users;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LoanManager.Infrastructure.Authentication;


public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    

    public JwtProvider(
        ISqlConnectionFactory sqlConnectionFactory,
        IOptions<JwtOptions> options
        )
    {
        _options = options.Value;
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<string> Generate(User user)
    {

       const string sql = @"
        SELECT DISTINCT
            p.name 
        FROM users usr 
        INNER JOIN user_roles usrl 
            ON usr.id = usrl.user_id
        INNER JOIN roles rl 
            ON rl.id = usrl.role_id
        INNER JOIN roles_permissions rp
            ON rl.id = rp.role_id
        INNER JOIN permissions p
            ON p.id = rp.permission_id
        WHERE usr.id = @UserId
        AND p.name IS NOT NULL;
        ";


    using var connection = _sqlConnectionFactory.CreateConnection();
    var permissions = 
    await connection.QueryAsync<string>(sql, new {UserId = user.Id.Value});

    var permissionCollection = permissions.ToHashSet();


        var claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Sub, user.Id!.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!.Value)
        };

        foreach(var permission in permissionCollection)
        {
            claims.Add(new (CustomClaims.Permissions, permission));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddDays(365),
            signingCredentials
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);   
        return tokenValue;
    }

public string Decode(string token)
{
    // Crea un JwtSecurityTokenHandler para leer el JWT
    var handler = new JwtSecurityTokenHandler();

    // Verifica si el token es válido
    if (handler.CanReadToken(token))
    {
        // Lee el token JWT
        var jwtToken = handler.ReadJwtToken(token);

        // Extrae el 'UsuarioId' desde los claims
        var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

        // Si el userIdClaim tiene el formato "UsuarioId { Value = ... }", extrae solo el GUID
        if (!string.IsNullOrEmpty(userIdClaim) && userIdClaim.Contains("UsuarioId"))
        {
            // Usa expresiones regulares para extraer el GUID
            var guid = System.Text.RegularExpressions.Regex.Match(userIdClaim, @"\{ Value = ([^}]+) \}").Groups[1].Value;
            return guid; // Devuelve solo el GUID
        }

        return string.Empty; // Devuelve una cadena vacía si no se encuentra el GUID
    }
    else
    {
        throw new Exception("Invalid JWT token.");
    }
}

}