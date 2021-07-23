using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Szkolenie.Testy
{
    [TestClass]
    public class Test
    {
        private static List<String> _lista;

        [ClassInitialize]
        public static void ClassInit(TestContext ctx)
        {
            _lista = new List<string>();
        }

        [ClassCleanup]
        public static void ClassClean()
        {
            _lista = null;
        }

        [TestInitialize]
        public void Init()
        {

        }

        [TestCleanup]
        public void Clean()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
            var wynik = 2 + 2;
            Assert.AreEqual(4, wynik);
            Assert.IsNotNull(_lista);
        }

        [TestMethod]
        public void MyTestMethod2()
        {

        }
    }
}
