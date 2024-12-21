using System;
using System.Data;
using System.Data.SqlClient;

namespace RailwayReservation
{
    class Program
    {
        static string connectionString = "Data Source=ICS-LT-BXXLV44\\SQLEXPRESS01;Initial Catalog=mini;Integrated Security=True";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Admin\n2. User\n3. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AdminMenu();
                        break;
                    case 2:
                        UserMenu();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        // Admin Menu
        static void AdminMenu()
        {
            Console.WriteLine("1. Insert Train\n2. Update Train\n3. Delete Train\n4. Exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    InsertTrain();
                    break;
                case 2:
                    UpdateTrain();
                    break;
                case 3:
                    DeleteTrain();
                    break;
                case 4:
                    return;
            }
        }

        // Insert Train
        static void InsertTrain()
        {
            Console.Write("-----Enter Train No:----- ");
            int trainNo = int.Parse(Console.ReadLine());
            Console.Write("-----Enter Train Name: ------");
            string name = Console.ReadLine();
            Console.Write("Enter Source: ");
            string source = Console.ReadLine();
            Console.Write("Enter Destination: ");
            string destination = Console.ReadLine();
            Console.Write("First Class Berths: ");
            int firstClass = int.Parse(Console.ReadLine());
            Console.Write("Second Class Berths: ");
            int secondClass = int.Parse(Console.ReadLine());
            Console.Write("Sleeper Berths: ");
            int sleeper = int.Parse(Console.ReadLine());
            Console.Write("Enter Departure Time (HH:MM): ");
            string departureTime = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Trains (TrainNo, TrainName, Source, Destination, FirstClassBerths, SecondClassBerths, SleeperBerths, DepartureTime, IsActive) " +
                               "VALUES (@TrainNo, @Name, @Source, @Destination, @FirstClass, @SecondClass, @Sleeper, @DepartureTime, 1)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@FirstClass", firstClass);
                cmd.Parameters.AddWithValue("@SecondClass", secondClass);
                cmd.Parameters.AddWithValue("@Sleeper", sleeper);
                cmd.Parameters.AddWithValue("@DepartureTime", departureTime);

                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Train Added Successfully!");
            }
        }

        // Update Train
        static void UpdateTrain()
        {
            Console.Write("Enter Train No to Modify: ");
            int trainNo = int.Parse(Console.ReadLine());

            Console.Write("New Train Name: ");
            string name = Console.ReadLine();
            Console.Write("New First Class Berths: ");
            int firstClass = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Trains SET TrainName=@Name, FirstClassBerths=@FirstClass WHERE TrainNo=@TrainNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@FirstClass", firstClass);

                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Train Modified Successfully!");
            }
        }

        // Delete Train (Soft Delete)
        static void DeleteTrain()
        {
            Console.Write("Enter Train No to Delete (Soft Delete): ");
            int trainNo = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Trains SET IsActive=0 WHERE TrainNo=@TrainNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);

                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Train Deleted Successfully!");
            }
        }

        // User Menu
        static void UserMenu()
        {
            Console.WriteLine("1. Book Ticket\n2. Cancel Ticket\n3. Show All Trains\n4. Exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    TicketBooking();
                    break;
                case 2:
                    TicketCancellation();
                    break;
                case 3:
                    DisplayTrains();
                    break;
                case 4:
                    return;
            }
        }

        // Ticket Booking
        static void TicketBooking()
        {
            Console.Write("Enter Train No: ");
            int trainNo = int.Parse(Console.ReadLine());
            Console.Write("Enter Class (First/Second/Sleeper): ");
            string trainClass = Console.ReadLine();
            Console.Write("Enter Passenger Name: ");
            string passenger = Console.ReadLine();
            Console.Write("Enter Journey Date (YYYY-MM-DD): ");
            string date = Console.ReadLine();

            // Check if there are available seats for the specified class
            int availableSeats = GetAvailableSeats(trainNo, trainClass);

            if (availableSeats > 0)
            {
                // Proceed with the booking if seats are available
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Bookings (TrainNo, Class, PassengerName, JourneyDate) VALUES (@TrainNo, @Class, @Passenger, @JourneyDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                    cmd.Parameters.AddWithValue("@Class", trainClass);
                    cmd.Parameters.AddWithValue("@Passenger", passenger);
                    cmd.Parameters.AddWithValue("@JourneyDate", date);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Ticket Booked Successfully!");

                    // Display the booking details
                    ShowBookedTicketDetails(trainNo, trainClass, passenger, date);
                }
            }
            else
            {
                // Display error message if no seats are available
                Console.WriteLine("Error: No available seats in the selected class for the specified train.");
            }
        }

        // Method to check available seats for a specific train and class
        static int GetAvailableSeats(int trainNo, string trainClass)
        {
            int availableSeats = 0;

            // Query to get available seats based on the class
            string classColumn = trainClass == "First" ? "FirstClassBerths" :
                                 trainClass == "Second" ? "SecondClassBerths" : "SleeperBerths";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = $"SELECT {classColumn} - ISNULL((SELECT COUNT(*) FROM Bookings WHERE TrainNo = @TrainNo AND Class = @Class), 0) AS AvailableSeats " +
                               "FROM Trains WHERE TrainNo = @TrainNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                cmd.Parameters.AddWithValue("@Class", trainClass);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    availableSeats = Convert.ToInt32(reader["AvailableSeats"]);
                }
            }

            return availableSeats;
        }

        // Display the details of the ticket that was just booked
        static void ShowBookedTicketDetails(int trainNo, string trainClass, string passenger, string date)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT b.BookingID, b.PassengerName, b.Class, b.JourneyDate, t.TrainName, t.Source, t.Destination, t.DepartureTime " +
                               "FROM Bookings b INNER JOIN Trains t ON b.TrainNo = t.TrainNo " +
                               "WHERE b.TrainNo = @TrainNo AND b.PassengerName = @Passenger AND b.JourneyDate = @JourneyDate " +
                               "ORDER BY b.BookingID DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                cmd.Parameters.AddWithValue("@Passenger", passenger);
                cmd.Parameters.AddWithValue("@JourneyDate", date);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("\n---- Ticket Details ----");
                        Console.WriteLine($"Booking ID: {reader["BookingID"]}");
                        Console.WriteLine($"Passenger Name: {reader["PassengerName"]}");
                        Console.WriteLine($"Class: {reader["Class"]}");
                        Console.WriteLine($"Journey Date: {reader["JourneyDate"]}");
                        Console.WriteLine($"Train Name: {reader["TrainName"]}");
                        Console.WriteLine($"From: {reader["Source"]}");
                        Console.WriteLine($"To: {reader["Destination"]}");
                        Console.WriteLine($"Departure Time: {reader["DepartureTime"]}");
                    }
                }
                else
                {
                    Console.WriteLine("No ticket found!");
                }
            }
        }

        // Display All Active Trains
        static void DisplayTrains()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Trains WHERE IsActive=1";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Train No: {reader["TrainNo"]}, Name: {reader["TrainName"]}, From: {reader["Source"]}, To: {reader["Destination"]}, Departure Time: {reader["DepartureTime"]}");
                }
            }
        }

        // Ticket Cancellation
        static void TicketCancellation()
        {
            Console.Write("Enter Booking ID to Cancel: ");
            int bookingID = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Bookings SET Status='Cancelled' WHERE BookingID=@BookingID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                conn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Ticket Cancelled!");
            }
        }
    }
}
