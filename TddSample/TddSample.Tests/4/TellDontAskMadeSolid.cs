using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddSample.Tests
{
    public class Purchase
    {
        private readonly double _subTotal;

        public Purchase(double subTotal)
        {
            _subTotal = subTotal;
        } 

        public double Total 
        {
            get 
            { 
                double discount = _subTotal > 10000 ? .10 : 0; 
                return _subTotal * (1 - discount); 
            } 
        }
    }

    public interface IPurchaseMessenger
    {
        void RejectPurchase(Purchase purchase, Account account);
    }

    public class Account
    {
        private double _balance;
        private readonly IPurchaseMessenger messenger;

        public Account(IPurchaseMessenger purchaseManager)
        {
            messenger = purchaseManager;
        }

        public void Deduct(Purchase purchase)
        {
            if (purchase.Total < _balance) 
            { 
                _balance -= purchase.Total; 
            }
            else 
            {
                messenger.RejectPurchase(purchase, this); 
            }
        }
    }

    public class ClassThatObeysTellDontAsk
    {
        public void MakePurchase(Purchase purchase, Account account)
        {
            account.Deduct(purchase);
        }
    }
}