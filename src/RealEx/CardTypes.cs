namespace RealEx
{
    public class CardTypes
    {
        public const string Visa = "VISA";
        public const string Mastercard = "MC";
        public const string Switch = "SWITCH";
        public const string AmericanExpress = "AMEX";
        public const string Laser = "LASER";
        public const string Diners = "DINERS";

        public static string FromCode(string code)
        {
            switch (code)
            {
                case "VISA":
                    return "Visa";
                case "MC":
                    return "Mastercard";
                case "SWITCH":
                    return "Switch/Solo";
                case "AMEX":
                    return "American Express";
                case "LASER":
                    return "Laser";
                case "DINERS":
                    return "Diners";
                default:
                    return "Unknown card type";
            }
        }
    }
}