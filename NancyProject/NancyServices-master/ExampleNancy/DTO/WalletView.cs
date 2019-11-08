using System;



namespace WalletsApi.Views
{
    public class WalletView
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ExternalId { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; }
        
        public decimal CurrentBalance { get; set; }
        public string Currency { get; set; }
        public DateTime? LastUpdate { get; set; }

        
        public string BankEntityNumber { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankEntityName { get; set; }

        public decimal PhysicalCardFee { get; set; }

        
        
        public string AccountId { get; set; }
    }
}