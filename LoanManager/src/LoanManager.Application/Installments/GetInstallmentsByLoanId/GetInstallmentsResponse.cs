namespace LoanManager.Application.Installments.GetInstallmentsByLoanId;


public sealed class GetInstallmentsResponse {  

        public Guid Id { get; set; }     // ID de la cuota
        public Guid LoanId { get; set; }   // ID del préstamo relacionado
        public int InstallmentNumber { get; set; }  // Número de la cuota
        public decimal TotalAmount { get; set; } // Monto total de la cuota
        public string Status { get; set; }     // Estado de la cuota (pendiente, pagada, etc.)
        public DateTime ExpirationDate { get; set; } // Fecha de vencimiento de la cuota
}





















