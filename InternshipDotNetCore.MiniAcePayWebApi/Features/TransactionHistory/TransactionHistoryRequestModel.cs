using InternshipDotNetCore.MiniAcePayWebApi.Features.Models;

namespace InternshipDotNetCore.MiniAcePayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryRequestModel
    {
        public string? CustomerCode {  get; set; }
    }

    public class TransactionHistoryResponseModel
    {
        public bool IsSucces {  get; set; }

        public string Message { get; set; }

        public List<CustomerTransactionHistoryModel> Data { get; set; }
    }
}
