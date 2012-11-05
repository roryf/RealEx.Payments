using System;
using Machine.Specifications;

namespace RealEx.Tests
{
    public class RedirectionTransactionServiceSpecs
    {
        private static ConfigurationFromConfigSettings TestConfig = new ConfigurationFromConfigSettings
        {
            Currency = "GBP",
            MerchantId = "TestMerchant",
            Mode = AccountMode.Test,
            SharedSecret = "foobar",
            SubAccount = "test"
        };

        [Subject(typeof(RedirectTransactionService))]
        public class when_creating_transaction_request
        {
            private static Address billingAddress;
            private static Address shippingAddress;
            private static string transactionId;
            private static decimal amount;
            private static DateTime transactionDate;

            private static RedirectTransactionService service;
            private static TransactionRequest result;

            Establish context = () =>
                {
                    service = new RedirectTransactionService(TestConfig);

                    billingAddress = shippingAddress = new Address {CountryIso3166Alpha2Code = "UK", Line1 = "123 some street", Postcode = "EH6 7BS"};
                    transactionId = Guid.Empty.ToString();
                    amount = 100m;
                    transactionDate = new DateTime(2012, 6, 13, 12, 0, 0);
                };

            Because of = () => result = service.CreateTransactionRequest(billingAddress, shippingAddress, transactionId, amount, transactionDate, true);

            It should_create_hash = () => result.Sha1Hash.ShouldEqual("e34e77dd24d6b66dd6a6226149544806033f6600");

            It should_set_billing_code = () => result.BillingCode.ShouldEqual("67|123");

            It should_set_shipping_code = () => result.ShippingCode.ShouldEqual("67|123");

            It should_set_endpoint_to_default = () => result.UrlEndPoint.ShouldEqual("https://redirect.globaliris.com/epage.cgi");

            It should_set_amount_with_no_decimal_points = () => result.Amount.ShouldEqual("10000");

            It should_set_sub_account = () => result.SubAccount.ShouldEqual("test");

            It should_set_autosettle = () => result.AutoSettle.ShouldBeTrue();
        }

        [Subject(typeof(RedirectTransactionService), "validating transaction response")]
        public class when_transaction_is_successful
        {
            public static RedirectTransactionService service;
            public static TransactionResponse response;
            public static TransactionStatus result;

            Establish context = () =>
                {
                    service = new RedirectTransactionService(TestConfig);
                    response = new TransactionResponse
                    {
                        ResultCode = "0",
                        Sha1Hash = "d92cc7e3b0c2df1c47df8ce949b76524255e82c3"
                    };
                };

            Because of = () => result = service.VerifyTransactionResponse(response);

            It should_return_successful_status = () => result.ShouldEqual(TransactionStatus.Successful);
        }

        [Subject(typeof(RedirectTransactionService), "validating transaction response")]
        public class when_hash_is_invalid
        {
            public static RedirectTransactionService service;
            public static TransactionResponse response;
            public static TransactionStatus result;

            Establish context = () =>
            {
                service = new RedirectTransactionService(TestConfig);
                response = new TransactionResponse
                {
                    ResultCode = "0",
                    Sha1Hash = "foobar"
                };
            };

            Because of = () => result = service.VerifyTransactionResponse(response);

            It should_return_hash_invalid_status = () => result.ShouldEqual(TransactionStatus.HashMismatch);
        }

        [Subject(typeof(RedirectTransactionService), "validating transaction response")]
        public class when_transaction_is_declined
        {
            public static RedirectTransactionService service;
            public static TransactionResponse response;
            public static TransactionStatus result;

            Establish context = () =>
            {
                service = new RedirectTransactionService(TestConfig);
                response = new TransactionResponse
                {
                    ResultCode = "101",
                    Sha1Hash = "064964b16cd80dd11ea0a14ed93b73f8b0cdcd0c"
                };
            };

            Because of = () => result = service.VerifyTransactionResponse(response);

            It should_return_declined_status = () => result.ShouldEqual(TransactionStatus.Declined);
        }
    }
}