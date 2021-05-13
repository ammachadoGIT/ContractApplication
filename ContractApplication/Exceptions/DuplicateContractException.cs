using System;

namespace ContractApplication.Exceptions
{
    public class DuplicateContractException : Exception
    {
        public DuplicateContractException()
            : base("Cannot create a duplicate contract.")
        {
        }
    }
}
