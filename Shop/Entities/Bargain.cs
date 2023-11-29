namespace Shop.Entities
{
    public enum BargainStatus
    {
        Pending,
        Accepted,
        Declined
    }


    public class Bargain
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OfferId { get;set; }
        public int NewPrice { get; set; }
        public BargainStatus Status { get; set; }
    }

    
}
