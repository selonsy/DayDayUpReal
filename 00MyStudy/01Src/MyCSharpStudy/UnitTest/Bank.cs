using System;

namespace BankAccountNS
{
    /// <summary> 
    /// Bank Account demo class. 
    /// </summary> 
    public class BankAccount
    {

        #region 字段

        //selonsy mark：字段和属性的命名示例，字段名称（_name），属性名称（Name）

        //用户名
        private readonly string _customerName;

        //账户余额
        private double _balance;

        //账户状态
        private bool _frozen = false;

        //超限异常信息，自定义，用于获取具体抛出的异常
        public const string DebitAmountExceedsBalanceMessage = "Debit amount exceeds balance";

        public const string DebitAmountLessThanZeroMessage = "Debit amount less than zero";

        #endregion

        #region 构造方法

        private BankAccount()
        {
        }

        public BankAccount(string customerName, double balance)
        {
            _customerName = customerName;
            _balance = balance;
        }

        #endregion

        #region 属性

        public string CustomerName
        {
            get { return _customerName; }
        }

        public double Balance
        {
            get { return _balance; }
        }

        #endregion

        /// <summary>
        /// 取款
        /// </summary>
        /// <param name="amount"></param>
        public void Debit(double amount)
        {
            if (_frozen)
            {
                throw new Exception("您的账户被禁用了！");
            }

            if (amount > _balance)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountExceedsBalanceMessage);
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }

            _balance -= amount;
        }

        /// <summary>
        /// 存款
        /// </summary>
        /// <param name="amount"></param>
        public void Credit(double amount)
        {
            if (_frozen)
            {
                throw new Exception("您的账户被禁用了！");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, DebitAmountLessThanZeroMessage);
            }

            _balance += amount;
        }

        /// <summary>
        /// 冻结账户
        /// </summary>
        private void FreezeAccount()
        {
            _frozen = true;
        }

        /// <summary>
        /// 解冻账户
        /// </summary>
        private void UnfreezeAccount()
        {
            _frozen = false;
        }

        /// <summary>
        /// 主函数
        /// </summary>
        public static void Main()
        {
            BankAccount ba = new BankAccount("Mr. Bryan Walton", 11.99);

            ba.Credit(5.77);
            ba.Debit(11.22);
            Console.WriteLine("Current balance is ${0}", ba.Balance);
        }

    }
}
