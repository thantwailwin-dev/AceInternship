namespace InternshipDotNetCore.MiniAcePayWebApi.Features.Models
{
    public class CustomerTransactionHistoryModel
    {
        public int TransactionHistoryId {  get; set; }
        public string FromMobileNo { get; set; }
        public string ToMobileNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
