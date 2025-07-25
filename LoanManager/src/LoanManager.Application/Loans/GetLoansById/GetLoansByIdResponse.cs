namespace LoanManager.Application.Loans.GetLoansById;

public sealed class GetLoanByIdResponse
{
    public Guid Id {get; init;}
    public Guid Plan_Id { get; init; }
    public Guid Cliente_Id { get; init; }
    public string Apellido { get; set; }
    public string Dni { get; set; }
    public string Descripcion { get; set; }
    public string Nombre { get; set; }    
    public DateTime Fecha_Emision { get; init; }
    public decimal Capital_Total{ get; init; }
    public string? Capital_Tipo_Moneda { get; init; }
    public string? Estado { get; init; }
}