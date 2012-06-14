using Machine.Specifications;

namespace RealEx.Tests
{
    public class TransactionRequestSpecs
    {
        [Subject(typeof(TransactionRequest))]
        public class when_auto_settle_is_true
        {
            private static TransactionRequest subject;

            Establish context = () => subject = new TransactionRequest();

            Because of = () => subject.AutoSettle = true;

            It auto_settle_flag_should_be_one = () => subject.AutoSettleFlag.ShouldEqual("1");
        }

        [Subject(typeof(TransactionRequest))]
        public class when_auto_settle_is_false
        {
            private static TransactionRequest subject;

            Establish context = () => subject = new TransactionRequest();

            Because of = () => subject.AutoSettle = false;

            It auto_settle_flag_should_be_zero = () => subject.AutoSettleFlag.ShouldEqual("0");
        }
    }
}