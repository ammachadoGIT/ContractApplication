using AutoFixture;
using NUnit.Framework;

namespace ContractApplication.Tests
{
    public abstract class TestsBase
    {
        protected Fixture Fixture { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Fixture = new Fixture();
        }
    }
}
