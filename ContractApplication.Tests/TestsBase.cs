using AutoFixture;
using NUnit.Framework;

namespace ContractApplication.Tests
{
    [TestFixture]
    public abstract class TestsBase
    {
        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Fixture = new Fixture();
            this.Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
    }
}
