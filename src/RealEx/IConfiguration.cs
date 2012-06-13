namespace RealEx
{
    public interface IConfiguration
    {
        string MerchantId { get; }
        string SharedSecret { get; }
        string SubAccount { get; }
        string Currency { get; }
        AccountMode Mode { get; }
        string UrlEndPoint { get; }
    }
}