using System;
using System.Transactions;

using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace GigHub.IntegrationTests
{
    public sealed class IsolatedAttribute : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public ActionTargets Targets => ActionTargets.Test;

        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }
    }
}
