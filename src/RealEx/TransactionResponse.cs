namespace RealEx
{
    public class TransactionResponse
    {
        public string MerchantId { get; set; }
        public string SubAccount { get; set; }
        public string TransactionId { get; set; }
        public string Timestamp { get; set; }
        public string AuthCode { get; set; }
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
        public string CvnResult { get; set; }
        public string RealExTransactionReference { get; set; }
        public string Sha1Hash { get; set; }
    }
}