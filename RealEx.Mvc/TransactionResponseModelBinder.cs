using System;
using System.Web.Mvc;

namespace RealEx.Mvc
{
    public class TransactionResponseModelBinder : IModelBinder
    {
        const string AuthCode = "AUTHCODE";
        const string CvnResult = "CVNRESULT";
        const string MerchantId = "MERCHANT_ID";
        const string RealExTransactionReference = "PASREF";
        const string ResultCode = "RESULT";
        const string ResultMessage = "MESSAGE";
        const string Sha1Hash = "SHA1HASH";
        const string SubAccount = "ACCOUNT";
        const string Timestamp = "TIMESTAMP";
        const string TransactionId = "ORDER_ID";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new TransactionResponse
            {
                AuthCode = GetFormField(AuthCode, bindingContext.ValueProvider),
                CvnResult = GetFormField(CvnResult, bindingContext.ValueProvider),
                MerchantId = GetFormField(MerchantId, bindingContext.ValueProvider),
                RealExTransactionReference = GetFormField(RealExTransactionReference, bindingContext.ValueProvider),
                ResultCode = Convert.ToInt32(GetFormField(ResultCode, bindingContext.ValueProvider)),
                ResultMessage = GetFormField(ResultMessage, bindingContext.ValueProvider),
                Sha1Hash = GetFormField(Sha1Hash, bindingContext.ValueProvider),
                SubAccount = GetFormField(SubAccount, bindingContext.ValueProvider),
                Timestamp = GetFormField(Timestamp, bindingContext.ValueProvider),
                TransactionId = GetFormField(TransactionId, bindingContext.ValueProvider)
            };
        }

        string GetFormField(string key, IValueProvider provider)
        {
            ValueProviderResult result = provider.GetValue(key);

            if (result != null)
            {
                return (string)result.ConvertTo(typeof(string));
            }

            return null;
        }
    }
}