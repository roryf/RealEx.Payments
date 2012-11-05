using System;

namespace RealEx
{
    public interface IRedirectTransactionService
    {
        TransactionRequest CreateTransactionRequest(Address billingAddress, Address shippingAddress, string transactionId, decimal amount, DateTime transactionTime);
        TransactionRequest CreateTransactionRequest(Address billingAddress, Address shippingAddress, string transactionId, decimal amount, DateTime transactionTime, bool autoSettle);
        TransactionStatus VerifyTransactionResponse(TransactionResponse response);
    }
}