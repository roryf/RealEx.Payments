using System;

namespace RealEx
{
    public interface IRedirectTransactionService
    {
        TransactionRequest CreateTransactionRequest(Address billingAddress, Address shippingAddress, string transactionId, decimal amount, DateTime transactionTime);
        TransactionStatus VerifyTransactionResponse(TransactionResponse response);
    }
}