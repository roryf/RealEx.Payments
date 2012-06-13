using Machine.Specifications;

namespace RealEx.Tests
{
    public class ConfigurationSpecs
    {
        [Subject(typeof(ConfigurationFromConfigSettings))]
        public class when_loading_from_config_file
        {
            private static ConfigurationFromConfigSettings config;

            Establish context = () =>
                {
                    config = ConfigurationFromConfigSettings.Current;
                };

            It should_set_currency = () => config.Currency.ShouldEqual("USD");

            It should_set_merchant_id = () => config.MerchantId.ShouldEqual("TestMerchant");

            It should_set_account_mode = () => config.Mode.ShouldEqual(AccountMode.Live);

            It should_set_shared_secret = () => config.SharedSecret.ShouldEqual("foobar");

            It should_set_sub_account = () => config.SubAccount.ShouldEqual("test");

            It should_set_url = () => config.UrlEndPoint.ShouldEqual("http://example.com/");
        }

        [Subject(typeof(ConfigurationFromConfigSettings))]
        public class when_sub_account_is_not_specified
        {
            private static ConfigurationFromConfigSettings config;

            Establish context = () => config = new ConfigurationFromConfigSettings();

            It should_use_default = () => config.SubAccount.ShouldEqual("internet");
        }

        [Subject(typeof(ConfigurationFromConfigSettings))]
        public class when_currency_is_not_specified
        {
            private static ConfigurationFromConfigSettings config;

            Establish context = () => config = new ConfigurationFromConfigSettings();

            It should_use_default = () => config.Currency.ShouldEqual("GBP");
        }

        [Subject(typeof(ConfigurationFromConfigSettings))]
        public class when_url_is_not_specified
        {
            private static ConfigurationFromConfigSettings config;

            Establish context = () => config = new ConfigurationFromConfigSettings();

            It should_use_default = () => config.UrlEndPoint.ShouldEqual("https://redirect.globaliris.com/epage.cgi");
        }

        [Subject(typeof(ConfigurationFromConfigSettings))]
        public class when_mode_is_not_specified
        {
            private static ConfigurationFromConfigSettings config;

            Establish context = () => config = new ConfigurationFromConfigSettings();

            It should_use_default = () => config.Mode.ShouldEqual(AccountMode.Test);
        }
    }
}