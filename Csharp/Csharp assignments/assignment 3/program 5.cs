using System;

public class SaleTransaction
{
    protected int salesNo;
    protected int productNo;
    protected DateTime dateOfSale;

    public SaleTransaction(int salesNo, int productNo, DateTime dateOfSale)
    {
        salesNo = salesNo;      
        productNo = productNo;  
        dateOfSale = dateOfSale; 
    }

    public void ShowTransactionInfo()
    {
        Console.WriteLine($"Sales No: {salesNo}");
        Console.WriteLine($"Product No: {productNo}");
        Console.WriteLine($"Date of Sale: {dateOfSale.ToShortDateString()}");
    }
}

public class SaleDetails : SaleTransaction
{
    private double price;
    private int qty;
    private double totalAmount;

    public SaleDetails(int salesNo, int productNo, DateTime dateOfSale, double price, int qty)
        : base(salesNo, productNo, dateOfSale)
    {
        price = price; 
        qty = qty; 
        CalculateTotalAmount(); 
    }

    private void CalculateTotalAmount()
    {
        totalAmount = price * qty;
    }

    public static void ShowSaleDetails()
    {
        Console.WriteLine("Enter Sales Information:");

        Console.Write("Sales No: ");
        int salesNo = int.Parse(Console.ReadLine());

        Console.Write("Product No: ");
        int productNo = int.Parse(Console.ReadLine());

        Console.Write("Enter Date of Sale (yyyy-mm-dd): ");
        DateTime dateOfSale = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter Price: ");
        double price = double.Parse(Console.ReadLine());

        Console.Write("Enter Quantity: ");
        int qty = int.Parse(Console.ReadLine());

        SaleDetails sale = new SaleDetails(salesNo, productNo, dateOfSale, price, qty);

        sale.ShowTransactionInfo(); 
        Console.WriteLine($"Price: {price}");
        Console.WriteLine($"Quantity: {qty}");
        Console.WriteLine($"Total Amount: {sale.totalAmount}");
    }
}

class Program
{
    static void Main()
    {
        SaleDetails.ShowSaleDetails();
    }
}