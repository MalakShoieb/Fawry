using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace Task
{
    
    public class Products:IExpired,IShipped
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsExpirable { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public bool IsShippable { get; set; }
        public double Weight { get; set; }
        public string GetName()
        {
            return Name;
        }
        public double GetWeight()
        {
            return IsShippable? Weight:0;
        }

        public bool IsExpired()
        {
            if (!IsExpirable) return false;
            if (ExpiryDate == null) return false; 
            return DateTime.Now > ExpiryDate;

        }
    }
       
    }
    public class CartItems
    {
        public int CustomerQuantity { get; set; }
        public Products products { get; set; }
    }




public class Cart
{
    public List<CartItems> cartItems { get; set; }
    public Cart()
    {
        cartItems = new List<CartItems>();
    }
    public void Add(Products product, int quantity)
    {
        if (product == null || string.IsNullOrEmpty(product.Name))
        {
            throw new Exception("Invalid product details.");
        }
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero.");
        }
        if (product.Quantity < quantity)
        {
            throw new ArgumentException("Insufficient product quantity.");
        }
        if (product.IsExpirable && product.IsExpired())
        {
            throw new ArgumentException($"The {product.Name} is expired, please choose another product.");
        }
        var existingItem = cartItems.FirstOrDefault(ci => ci.products.Name == product.Name);
        if (existingItem != null)
        {
            existingItem.CustomerQuantity += quantity;
        }
        else
        {
            cartItems.Add(new CartItems { products = product, CustomerQuantity = quantity });
        }
    }


}

public class CheckoutService
{
    public static void Checkout(Customer customer, Cart cart)
    {
        if (cart == null || !cart.cartItems.Any())
        {
            throw new ArgumentException("Cart is empty.");
        }

        foreach (var item in cart.cartItems)
        {
            if (item.products.IsExpirable && item.products.IsExpired())
            {
                throw new ArgumentException($"The product {item.products.Name} is expired, please choose another product.");
            }

            if (item.products.Quantity < item.CustomerQuantity)
            {
                throw new ArgumentException($"Insufficient quantity for {item.products.Name}.");
            }
        }

        List<IShipped> shippableItems = new List<IShipped>();
        double totalWeight = 0;

        foreach (var item in cart.cartItems)
        {
            if (item.products is IShipped shipped && item.products.IsShippable)
            {
                shippableItems.Add(shipped);
                totalWeight += shipped.GetWeight() * item.CustomerQuantity;
            }
        }

        decimal subtotal = 0;
        foreach (var item in cart.cartItems)
        {
            subtotal += item.products.Price * item.CustomerQuantity;
        }

        decimal shippingFee =30;
        decimal totalAmount = subtotal + shippingFee;

        Shipping.Ship(shippableItems);

        Console.WriteLine("** Checkout receipt **");
        foreach (var item in cart.cartItems)
        {
            decimal lineTotal = item.products.Price * item.CustomerQuantity;
            Console.WriteLine($"{item.CustomerQuantity}x {item.products.Name} {lineTotal}");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine($"Subtotal {subtotal}");
        Console.WriteLine($"Shipping {shippingFee}");
        Console.WriteLine($"Amount {totalAmount}");

        if (customer.Balance < totalAmount)
        {
            Console.WriteLine("Insufficient balance to complete the purchase.");
            return;
        }

        customer.Deduct(totalAmount);
        Console.WriteLine($"Customer balance after payment: {customer.Balance}");
    }

}
public class Customer
{
    public string Name { get; set; }
    public decimal Balance { get;private set; }
   
    public Customer(string name, decimal Balance)
    {
        Name = name;
        this.Balance = Balance;


    }

    public void Deduct(decimal amount)
    {
        if (amount > Balance)
        {
            throw new Exception("Insufficient balance.");
        }

        Balance -= amount;
    }
}


public class Shipping
{
    public static void Ship(List<IShipped> items)
    {
        if (items == null || !items.Any()) return;

        Console.WriteLine("** Shipment notice **");

        double totalWeight = 0;

        var grouped = items.GroupBy(i => new { Name = i.GetName(), Weight = i.GetWeight() });

        foreach (var group in grouped)
        {
            string name = group.Key.Name;
            double weight = group.Key.Weight;
            int count = group.Count();

            Console.WriteLine($"{count}x {name} {weight * 1000}g");
            totalWeight += weight * count;
        }

        Console.WriteLine($"Total package weight: {totalWeight}kg\n");
    }

}
