using System.Web.Mvc;
using Machine.Specifications;
using RealEx.Mvc;

namespace RealEx.Tests.Mvc
{
    public class TransactionResponseModelBinderSpecs
    {
        [Subject(typeof(TransactionResponseModelBinder))]
        public class when_binding_from_form_collection
        {
            private static TransactionResponseModelBinder binder;
            private static ModelBindingContext bindingContext;
            private static TransactionResponse result;

            Establish context = () =>
                {
                    var postData = new FormCollection
                    {
                        {"AUTHCODE", "1234"},
                        {"CVNRESULT", "M"},
                        {"MERCHANT_ID", "TestMerchant"},
                        {"PASREF", "20036839SomeGUIDGoesHere"},
                        {"RESULT", "00"},
                        {"MESSAGE", "Successful"},
                        {"SHA1HASH", "e34e77dd24d6b66dd6a6226149544806033f6600"},
                        {"ACCOUNT", "test"},
                        {"TIMESTAMP", "20120615120000"},
                        {"ORDER_ID", "00000000-0000-0000-0000-000000000000"}
                    };

                    bindingContext = new ModelBindingContext
                    {
                        ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(TransactionResponse)),
                        ValueProvider = postData.ToValueProvider()
                    };

                    binder = new TransactionResponseModelBinder();
                };

            Because of = () => result = (TransactionResponse)binder.BindModel(new ControllerContext(), bindingContext);

            It should_set_merchant_id = () => result.MerchantId.ShouldEqual("TestMerchant");

            It should_set_sub_account = () => result.SubAccount.ShouldEqual("test");

            It should_set_transaction_id = () => result.TransactionId.ShouldEqual("00000000-0000-0000-0000-000000000000");

            It should_set_timestamp = () => result.Timestamp.ShouldEqual("20120615120000");

            It should_set_auth_code = () => result.AuthCode.ShouldEqual("1234");

            It should_set_result_code = () => result.ResultCode.ShouldEqual("00");

            It should_set_result_message = () => result.ResultMessage.ShouldEqual("Successful");

            It should_set_cvn_result = () => result.CvnResult.ShouldEqual("M");

            It should_set_realex_transaction_reference = () => result.RealExTransactionReference.ShouldEqual("20036839SomeGUIDGoesHere");

            It should_set_sha1_hash = () => result.Sha1Hash.ShouldEqual("e34e77dd24d6b66dd6a6226149544806033f6600");
        }
    }
}