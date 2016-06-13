using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Devin;
namespace Devin.Tests
{
    [TestClass()]
    public class EncrptionTest
    {
        [TestMethod()]
        public void MyEncodeTest()
        {
            string a = "Mysoft95938";
            string expected = "50626qclpvJ";
            string actual = EncrptionHelper.MyEncode(a);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MyDecodeTest()
        {
            string a = "6.707";
            string expected = "95938";
            string actual = EncrptionHelper.MyDecode(a);
            Assert.AreEqual(expected, actual);
        }
    }
}

