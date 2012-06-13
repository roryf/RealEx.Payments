namespace RealEx
{
    public enum TransactionStatus
    {
        Successful,
        Declined,
        UnknownError,
        IncorrectTransactionRequest,
        AccountDeactivated,
        HashMismatch,
        UnknownResponseCode
    }
}