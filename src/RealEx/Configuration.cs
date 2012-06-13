using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq.Expressions;

namespace RealEx
{
    public class ConfigurationFromConfigSettings : IConfiguration
    {
        public const string DefaultUrl = "https://redirect.globaliris.com/epage.cgi";

        public const AccountMode DefaultMode = AccountMode.Test;
        public const string DefaultCurrency = CurrencyCodes.PoundSterling;
        public const string DefaultSubAccount = "internet";

        private string merchantId;
        private string sharedSecret;
        private string subAccount = DefaultSubAccount;
        private string currency = DefaultCurrency;
        private string url = DefaultUrl;

        public string MerchantId
        {
            get
            {
                CheckRequiredConfigValue(merchantId, "MerchantId");
                return merchantId;
            }
            set
            {
                CheckRequiredConfigValue(value, "MerchandId");
                merchantId = value;
            }
        }

        public string SharedSecret
        {
            get
            {
                CheckRequiredConfigValue(sharedSecret, "SharedSecret");
                return sharedSecret;
            }
            set
            {
                CheckRequiredConfigValue(value, "SharedSecret");
                sharedSecret = value;
            }
        }

        public string SubAccount
        {
            get { return subAccount; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    subAccount = value;
                }
            }
        }

        public string Currency
        {
            get { return currency; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    currency = value;
                }
            }
        }

        public string UrlEndPoint
        {
            get { return url; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    url = value;
                }
            }
        }

        public AccountMode Mode { get; set; }

        private void CheckRequiredConfigValue(string value, string name)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(name, name + " must be specified in the configuration");
            }
        }

        private static ConfigurationFromConfigSettings currentConfiguration;

        public static ConfigurationFromConfigSettings Current
        {
            get
            {
                if (currentConfiguration == null)
                {
                    currentConfiguration = LoadConfigurationFromConfigFile();
                }

                return currentConfiguration;
            }
        }

        private static ConfigurationFromConfigSettings LoadConfigurationFromConfigFile()
        {
            var section = ConfigurationManager.GetSection("realEx") as NameValueCollection;

            if (section == null)
            {
                return new ConfigurationFromConfigSettings();
            }

            var configuration = new ConfigurationFromConfigSettings
            {
                Currency = GetValue(x => x.Currency, section),
                MerchantId = GetValue(x => x.MerchantId, section),
                Mode = (AccountMode)Enum.Parse(typeof(AccountMode), (GetValue(x => x.Mode, section) ?? "Test")),
                SharedSecret = GetValue(x => x.SharedSecret, section),
                SubAccount = GetValue(x => x.SubAccount, section),
                UrlEndPoint = GetValue(x => x.UrlEndPoint, section)
            };
            return configuration;
        }

        private static string GetValue(Expression<Func<ConfigurationFromConfigSettings, object>> expression, NameValueCollection collection)
        {
            var body = expression.Body as MemberExpression;
            if (body == null && expression.Body is UnaryExpression)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            string name = body.Member.Name;
            return collection[name];
        }
    }
}