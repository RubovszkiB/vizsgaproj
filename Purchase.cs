namespace VizsgaAPI.Models
{
    public class Purchase
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long PlanId { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
