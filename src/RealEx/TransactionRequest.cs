namespace RealEx
{
    public class TransactionRequest
    {
        public string UrlEndPoint { get; set; }
        public string MerchantId { get; set; }
        public string SubAccount { get; set; }
        public string TransactionId { get; set; }
        public string Currency { get; set; }
        public string Amount { get; set; }
        public string Timestamp { get; set; }
        public string Sha1Hash { get; set; }
        public string BillingCode { get; set; }
        public string BillingCountry { get; set; }
        public string ShippingCode { get; set; }
        public string ShippingCountry { get; set; }
        public bool AutoSettle { get; set; }
        public string AutoSettleFlag
        {
            get { return AutoSettle ? "1" : "0"; }
        }
    }
}