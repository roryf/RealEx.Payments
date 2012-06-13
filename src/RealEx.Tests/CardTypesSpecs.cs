using Machine.Specifications;

namespace RealEx.Tests
{
    public class CardTypesSpecs
    {
        [Subject(typeof(CardTypes))]
        public class when_converting_from_code
        {
            It should_parse_visa = () => CardTypes.FromCode("VISA").ShouldEqual("Visa");

            It should_parse_mastercard = () => CardTypes.FromCode("MC").ShouldEqual("Mastercard");

            It should_parse_switch = () => CardTypes.FromCode("SWITCH").ShouldEqual("Switch/Solo");

            It should_parse_amex = () => CardTypes.FromCode("AMEX").ShouldEqual("American Express");

            It should_parse_laser = () => CardTypes.FromCode("LASER").ShouldEqual("Laser");

            It should_parse_diners = () => CardTypes.FromCode("DINERS").ShouldEqual("Diners");
        }
    }
}