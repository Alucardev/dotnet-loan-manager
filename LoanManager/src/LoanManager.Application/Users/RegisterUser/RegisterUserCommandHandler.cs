using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Shared;
using LoanManager.Domain.Users;

namespace LoanManager.Application.Users.RegisterUser;

internal class RegisterUserCommandHandler: ICommandHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository; 
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(RegisterUserCommand request, 
    CancellationToken cancellationToken)
    {
        
        var email = new Email(request.Email);
        var userExists = await _userRepository.IsUserExists(email);

        if(userExists)
        {
            return Result.Failure<Guid>(UserErrors.AlreadyExists);
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(
            new Name(request.Name),
            new LastName(request.LastName),
            new PasswordHash(passwordHash),
            new Email(request.Email)
        );

        _userRepository.Add(user);
        await _unitOfWork.SaveChangesAsync();
        return user.Id.Value;
    }
}