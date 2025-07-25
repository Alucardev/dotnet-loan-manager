using LoanManager.Application.Abstractions.Messaging;
using LoanManager.Domain.Abstractions;
using LoanManager.Domain.Clients;
using LoanManager.Domain.Shared;

namespace LoanManager.Application.Clients.CreateClient;

public class CrearClienteCommandHandler : ICommandHandler<CreateClientCommand, Guid>
{
    private IUnitOfWork _unitOfWork;
    private IClientRepository _clientRepository;
    public CrearClienteCommandHandler(IUnitOfWork unitOfWork, IClientRepository clienteRepository){
        _unitOfWork = unitOfWork;
        _clientRepository = clienteRepository;
    }
    public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = Client.Create(
        new LastName(request.LastName), 
        new Name(request.Name),
        new Dni(request.Dni),
        new Phone(request.Phone)
        );

        _clientRepository.Add(client);
        await _unitOfWork.SaveChangesAsync();
        return Result.Success(client.Id.Value);
    }
}