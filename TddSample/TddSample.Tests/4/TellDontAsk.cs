using System;

namespace TddSample.Tests
{
    public class DumbPurchase
    { 
        public double SubTotal { get; set; } 
        public double Discount { get; set; } 
        public double Total { get; set; } 
    }

    public class DumbAccount { 
        public double Balance { get; set; }
    }

    public class ClassThatUsesDumbEntities
    {
        public void MakePurchase(DumbPurchase purchase, DumbAccount account)
        {
            purchase.Discount = purchase.SubTotal > 10000 ? .10 : 0;
            purchase.Total = purchase.SubTotal * (1 - purchase.Discount);

            if (purchase.Total < account.Balance)
            {
                account.Balance -= purchase.Total;
            }
            else
            {
                RejectPurchase(purchase, "You don't have enough money.");
            }
        }

        private void RejectPurchase(DumbPurchase purchase, string message)
        {
            throw new NotImplementedException();
        }
    }
}