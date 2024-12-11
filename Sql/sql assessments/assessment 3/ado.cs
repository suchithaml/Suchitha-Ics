using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=ICS-LT-BXXLV44\\SQLEXPRESS01;Database=assessment3;Trusted_Connection=True";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("InsertProductDetails", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ProductName", "Laptop");
                command.Parameters.AddWithValue("@Price", 55000);

                SqlParameter productIdParam = new SqlParameter("@GeneratedProductId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(productIdParam);

                SqlParameter discountedPriceParam = new SqlParameter("@DiscountedPrice", SqlDbType.Float)
                {
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(discountedPriceParam);

                command.ExecuteNonQuery();

                int generatedProductId = (int)productIdParam.Value;
                double discountedPriceAsDouble = (double)discountedPriceParam.Value;
                float discountedPrice = (float)discountedPriceAsDouble; 

                Console.WriteLine("Generated Product ID: " + generatedProductId);
                Console.WriteLine("Discounted Price: " + discountedPrice);
                Console.ReadKey();
            }
        }
    }
}
