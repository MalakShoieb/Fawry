using System.Xml;

namespace Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var cheese = new Products
            {
                Name = "Cheese",
                Quantity = 10,
                Price = 100m,
                IsExpirable = true,
                ExpiryDate = new DateTime(2025, 11, 20),
                Weight = 0.4,
                IsShippable = true
            };

            var tv = new Products
            {
                Name = "TV",
                Quantity = 5,
                Price = 5000m,
                IsExpirable = false,
                Weight = 11,
                IsShippable = true
            };

            var scratchCard = new Products
            {
                Name = "ScratchCard",
                Quantity = 20,
                Price = 50m,
                IsExpirable = false,
                IsShippable = false
            };
            var Mobile = new Products
            {
                Name = "Mobile",
                Quantity = 5,
                Price = 3000m,
                IsExpirable = false,
                Weight = 0.2,
                IsShippable = true
            };
            var Biscuits= new Products
            {
                Name = "Biscuits",
                Quantity = 30,
                Price = 20m,
                IsExpirable = true,
                ExpiryDate = new DateTime(2025, 6, 30),
                Weight = 0.1,
                IsShippable = true
            };
            //note: please uncomment each case to test it separately, otherwise it will throw an exception for the first case and stop the execution.
            #region  FirstCase successful
            //var customer1 = new Customer("ahmed", 6200);

            //var Cart1 = new Cart();
            //Cart1.Add(cheese, 2);
            //Cart1.Add(tv, 1);
            //CheckoutService.Checkout(customer1, Cart1);
            #endregion

            #region SecondCase empty cart

            var customer2 = new Customer("Ali", 1000);
            var Cart2 = new Cart();
            CheckoutService.Checkout(customer2, Cart2);
            #endregion
            #region ThirdCase Expire
            //var customer3 = new Customer("Tamer", 3200);
            //var cart3 = new Cart();
            //cart3.Add(Biscuits, 3);
            //CheckoutService.Checkout(customer3, cart3);
            #endregion
            #region FourhtCase Insufficient 
            //var customer4 = new Customer("aya", 320);
            //var cart4 = new Cart();
            //cart4.Add(Mobile, 1);
            //CheckoutService.Checkout(customer4, cart4);
            #endregion

            #region Fifth Case
            //var customer5 = new Customer("Malak", 40000);
            //var cart5 = new Cart();
            //cart5.Add(Mobile, 10);
            //CheckoutService.Checkout(customer5, cart5); 
            #endregion

            try
            {
                var customer = new Customer("Ali", 1000);
                var cart = new Cart();
                cart.Add(Biscuits, 2);
                cart.Add(tv, 1);
                CheckoutService.Checkout(customer, cart);
            }
            catch (Exception ex)
            {
                Console.WriteLine("⚠️ Error: " + ex.Message);
            }
        }


    }
}
