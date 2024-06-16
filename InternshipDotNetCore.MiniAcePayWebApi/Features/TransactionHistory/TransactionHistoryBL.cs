namespace InternshipDotNetCore.MiniAcePayWebApi.Features.TransactionHistory
{
    public class TransactionHistoryBL
    {
        private readonly TransactionHistoryDA _transactionHistoryDA;
        public TransactionHistoryBL(TransactionHistoryDA transactionHistoryDA)
        {
            _transactionHistoryDA = transactionHistoryDA;
        }

        public TransactionHistoryResponseModel TransactionHistory(TransactionHistoryRequestModel requestModel)
        {
            TransactionHistoryResponseModel model = new TransactionHistoryResponseModel();
            //Exist Customer Code
            bool isExist = _transactionHistoryDA.IsExistCustomerCode(requestModel.CustomerCode);
            if(!isExist)
            {
                model.IsSucces = false;
                model.Message = "Customer doesn't exist";
                return model;
            }

            //Transaction History By Customer Code
            var lst = _transactionHistoryDA.TransactionHistoryByCustomerCode(requestModel.CustomerCode);
            model.IsSucces = true;
            model.Message = "Success";
            model.Data = lst;
            return model;

        }
    }
}
