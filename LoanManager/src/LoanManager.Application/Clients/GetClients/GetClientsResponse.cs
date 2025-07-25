namespace LoanManager.Application.Clients.GetClients
{
    public class GetClientsResponse
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public string? Name { get; set; }
        public string? Phone{ get; set; }
        public string? Dni { get; set; }
    }
}
