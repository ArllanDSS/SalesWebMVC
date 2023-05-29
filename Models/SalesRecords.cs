using SalesWebMVC.Models.Enums;

namespace SalesWebMVC.Models
{
    public class SalesRecords
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        public Seller Seller { get; set; }
        public SalesRecords() { }
        
        public SalesRecords(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
