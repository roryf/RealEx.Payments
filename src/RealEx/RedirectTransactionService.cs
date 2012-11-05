using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RealEx
{
    public class RedirectTransactionService : IRedirectTransactionService
    {
        private readonly IConfiguration _configuration;

        public RedirectTransactionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TransactionRequest CreateTransactionRequest(Address billingAddress, Address shippingAddress, string transactionId, decimal total, DateTime transactionTime)
        {
            var amount = (total * 100).ToString("##");
            var timestamp = transactionTime.ToString("yyyyMMddHHmmss");
            var hashInput = string.Format("{0}.{1}.{2}.{3}.{4}", timestamp, _configuration.MerchantId, transactionId, amount, _configuration.Currency);
            var hash = GetHash(string.Format("{0}.{1}", GetHash(hashInput), _configuration.SharedSecret));
            var billingCode = GetAddressCode(billingAddress);
            var shippingCode = GetAddressCode(shippingAddress);

            return new TransactionRequest
            {
                Amount = amount,
                BillingCode = billingCode,
                BillingCountry = billingAddress.CountryIso3166Alpha2Code,
                Currency = _configuration.Currency,
                MerchantId = _configuration.MerchantId,
                Sha1Hash = hash,
                ShippingCode = shippingCode,
                ShippingCountry = shippingAddress.CountryIso3166Alpha2Code,
                SubAccount = _configuration.SubAccount,
                Timestamp = timestamp,
                TransactionId = transactionId,
                UrlEndPoint = _configuration.UrlEndPoint
            };
        }

        public TransactionStatus VerifyTransactionResponse(TransactionResponse response)
        {
            var hashInput = string.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}", response.Timestamp, response.MerchantId, response.TransactionId, response.ResultCode, response.ResultMessage, response.RealExTransactionReference, response.AuthCode);
            var hash = GetHash(string.Format("{0}.{1}", GetHash(hashInput), _configuration.SharedSecret));

            if (hash != response.Sha1Hash)
            {
                return TransactionStatus.HashMismatch;
            }

            var responseCode = Convert.ToInt32(response.ResultCode);

            if (responseCode == 0)
            {
                return TransactionStatus.Successful;
            }
            if (responseCode > 100 && responseCode < 200)
            {
                return TransactionStatus.Declined;
            }
            if (responseCode >= 200 && responseCode < 400)
            {
                return TransactionStatus.UnknownError;
            }
            if (responseCode >= 500 && responseCode < 600)
            {
                return TransactionStatus.IncorrectTransactionRequest;
            }
            if (responseCode == 666)
            {
                return TransactionStatus.AccountDeactivated;
            }

            return TransactionStatus.UnknownResponseCode;
        }

        private string GetAddressCode(Address address)
        {
            var postcodeDigits = string.IsNullOrWhiteSpace(address.Postcode)
                                     ? string.Empty
                                     : Regex.Replace(address.Postcode, "[^\\d]", string.Empty);
            return string.Format("{0}|{1}", postcodeDigits, Regex.Replace(address.Line1 + address.Line2, "[^\\d]", ""));
        }

        private string GetHash(string value)
        {
            var sha = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}