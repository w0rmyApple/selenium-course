using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.tests
{
    [TestFixture]
    public class WorkWithCart : TestBase
    {
        [Test(Description = "Работа с корзиной"), Order(1)]
        public void AddAndDeleteInCard()
        {
            app.AddAndDeleteProducts("Small");
        }
    }
}

