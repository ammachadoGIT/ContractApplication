using System;

namespace ContractApplication.Exceptions
{
    public class SelfContractException : Exception
    {
        public SelfContractException()
            : base("Cannot create a contract with self.")
        {
        }
    }
}
