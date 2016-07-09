using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

/*
 *关于引用：
 *引用中的 Microsoft.Visualstudio.QualityTools.UnitTestFramework 
 *为微软自带的单元测试框架。
 */

namespace BankTest
{
    //说明以下是一个测试类，不能被继承
    [TestClass]
    public class BankTest
    {
        //说明了以下方法是一个测试用例，不能被继承

        //selonsy mark：TestClass和TestMethod缺少任何一个，编译不会出错，但是VS不会将其作为一个单元测试方法
        /// <summary>
        /// 取款测试
        /// </summary>
        [TestMethod]
        public void DebitTest()
        {
            //创建一个账户shenjl，余额为100元
            BankAccount bank = new BankAccount("shenjl", 100);
            //取款10元
            bank.Debit(10);
            //Assert在这里可以理解成断言：在VSTS里做单元测试是基于断言的测试
            //预计的结果是90元，如果等于实际的结果的话就通过，否则不通过。
            Assert.AreEqual(90, bank.Balance);
        }

        /// <summary>
        /// 存款测试
        /// </summary>
        [TestMethod]
        public void CreditTest()
        {
            //创建一个账户shenjl，余额为100元
            BankAccount bank = new BankAccount("shenjl", 100);
            //存款10元
            bank.Credit(10);
            //Assert在这里可以理解成断言：在VSTS里做单元测试是基于断言的测试
            //预计的结果是90元，如果等于实际的结果的话就通过，否则不通过。
            Assert.AreEqual(110, bank.Balance);
        }

        //测试Debit方法中的非法取款
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert
            double actual = account.Balance;
            
            //验证两个数在指定的精度里
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        //测试Debit方法，如果取款金额小于0，那么应该抛出超限的异常
        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // 此处的断言被ExpectedException异常处理了，在Bank类中有相应的设置，可以看出是怎样的异常
            // assert is handled by ExpectedException
        }

        //测试Debit方法，如果金额大于余额，那么应该抛出超限的异常
        [TestMethod]
        public void Debit_WhenAmountIsGreaterThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 21.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount. DebitAmountExceedsBalanceMessage);
                return;
            }
            //如果不进入catch的话，在不检查任何条件的情况下使断言失败，显示消息
            //Assert.Fail("No exception was thrown.");
        }
    }
}
