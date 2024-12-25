using System;
using System.Collections.Generic;
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
                Console.WriteLine("Welcome!!\n\n1. Admin\n2. User\n3. Exit");
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
                string checkQuery = "SELECT COUNT(*) FROM Trains WHERE TrainNo = @TrainNo";
                string insertQuery = "INSERT INTO Trains (TrainNo, TrainName, Source, Destination, FirstClassBerths, SecondClassBerths, SleeperBerths, DepartureTime) " +
                                     "VALUES (@TrainNo, @Name, @Source, @Destination, @FirstClass, @SecondClass, @Sleeper, @DepartureTime)";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@TrainNo", trainNo);

                conn.Open();

                int existingRecords = (int)checkCmd.ExecuteScalar();
                if (existingRecords > 0)
                {
                    Console.WriteLine("Error: A train with this Train No already exists.");
                }
                else
                {
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@TrainNo", trainNo);
                    insertCmd.Parameters.AddWithValue("@Name", name);
                    insertCmd.Parameters.AddWithValue("@Source", source);
                    insertCmd.Parameters.AddWithValue("@Destination", destination);
                    insertCmd.Parameters.AddWithValue("@FirstClass", firstClass);
                    insertCmd.Parameters.AddWithValue("@SecondClass", secondClass);
                    insertCmd.Parameters.AddWithValue("@Sleeper", sleeper);
                    insertCmd.Parameters.AddWithValue("@DepartureTime", departureTime);

                    insertCmd.ExecuteNonQuery();
                    Console.WriteLine("Train Added Successfully!");
                }
            }
        }


        // Update Train
        static void UpdateTrain()
        {
            Console.Write("Enter Train No to Modify: ");
            int trainNo = int.Parse(Console.ReadLine());

            Console.Write("New Train Name: ");
            string name = Console.ReadLine();
            Console.Write("New Source: ");
            string source = Console.ReadLine();
            Console.Write("New Destination: ");
            string destination = Console.ReadLine();
            Console.Write("New Departure Time (HH:MM): ");
            string departureTime = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Trains SET TrainName=@Name, Source=@Source, Destination=@Destination, DepartureTime=@DepartureTime WHERE TrainNo=@TrainNo";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Source", source);
                cmd.Parameters.AddWithValue("@Destination", destination);
                cmd.Parameters.AddWithValue("@DepartureTime", departureTime);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    Console.WriteLine("Train Modified Successfully!");
                }
                else
                {
                    Console.WriteLine("Error: Train not found.");
                }
            }
        }



        // Delete Train
        static void DeleteTrain()
        {
            Console.Write("Enter Train No to Delete: ");
            int trainNo = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // First delete all bookings referencing the TrainNo
                    string deleteBookingsQuery = "DELETE FROM Bookings WHERE TrainNo=@TrainNo";
                    SqlCommand deleteBookingsCmd = new SqlCommand(deleteBookingsQuery, conn);
                    deleteBookingsCmd.Parameters.AddWithValue("@TrainNo", trainNo);
                    deleteBookingsCmd.ExecuteNonQuery();

                    // Then delete the train
                    string deleteTrainQuery = "DELETE FROM Trains WHERE TrainNo=@TrainNo";
                    SqlCommand deleteTrainCmd = new SqlCommand(deleteTrainQuery, conn);
                    deleteTrainCmd.Parameters.AddWithValue("@TrainNo", trainNo);
                    int rowsAffected = deleteTrainCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Train deleted successfully!");
                    }
                    else
                    {
                        Console.WriteLine("Error: Train not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }


        // User Menu
        static void UserMenu()
        {
            Console.WriteLine("1. Book Ticket\n2. Cancel Ticket\n3. Show All Trains\n4. View Booked Tickets\n5. Exit");
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
                    ShowBookedTickets();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }


        // Ticket Booking
        static void TicketBooking()
        {
            Console.Write("Enter Train No: ");
            int trainNo = int.Parse(Console.ReadLine());
            Console.Write("Enter Class (First/Second/Sleeper): ");
            string trainClass = Console.ReadLine();
            Console.Write("Enter Journey Date (YYYY-MM-DD): ");
            string date = Console.ReadLine();
            Console.Write("Enter Number of Passengers: ");
            int numPassengers = int.Parse(Console.ReadLine());

            List<string> passengerNames = new List<string>();
            for (int i = 0; i < numPassengers; i++)
            {
                Console.Write($"Enter Passenger Name {i + 1}: ");
                passengerNames.Add(Console.ReadLine());
            }

            // Check if there are enough available seats for the specified class
            int availableSeats = GetAvailableSeats(trainNo, trainClass);

            if (availableSeats >= numPassengers)
            {
                // Proceed with booking for each passenger if seats are available
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction(); // Begin transaction

                    try
                    {
                        List<int> bookingIDs = new List<int>(); // List to store generated booking IDs

                        foreach (string passenger in passengerNames)
                        {
                            string query = "INSERT INTO Bookings (TrainNo, Class, PassengerName, JourneyDate) " +
                                           "OUTPUT INSERTED.BookingID VALUES (@TrainNo, @Class, @Passenger, @JourneyDate)";
                            SqlCommand cmd = new SqlCommand(query, conn, transaction);
                            cmd.Parameters.AddWithValue("@TrainNo", trainNo);
                            cmd.Parameters.AddWithValue("@Class", trainClass);
                            cmd.Parameters.AddWithValue("@Passenger", passenger);
                            cmd.Parameters.AddWithValue("@JourneyDate", date);

                            int bookingID = (int)cmd.ExecuteScalar(); // Retrieve the BookingID of the inserted row
                            bookingIDs.Add(bookingID);
                        }

                        transaction.Commit();
                        Console.WriteLine("Tickets Booked Successfully!");

                        // Display the details of all booked tickets
                        Console.WriteLine("\n---- Ticket Details ----");
                        for (int i = 0; i < numPassengers; i++)
                        {
                            Console.WriteLine($"Booking ID: {bookingIDs[i]}");
                            Console.WriteLine($"Passenger Name: {passengerNames[i]}");
                            Console.WriteLine($"Train No: {trainNo}");
                            Console.WriteLine($"Class: {trainClass}");
                            Console.WriteLine($"Journey Date: {date}");
                            Console.WriteLine("-----------------------");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Rollback transaction on error
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Error: can't book the ticket.");
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

        static void DisplayTrains()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Trains";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"Train No: {reader["TrainNo"]}, Name: {reader["TrainName"]}, From: {reader["Source"]}, To: {reader["Destination"]}, Departure Time: {reader["DepartureTime"]}");
                }
            }
        }
        static void ShowBookedTickets()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT b.BookingID, b.PassengerName, b.Class, b.JourneyDate, b.Status, " +
                               "t.TrainName, t.Source, t.Destination, t.DepartureTime " +
                               "FROM Bookings b " +
                               "INNER JOIN Trains t ON b.TrainNo = t.TrainNo " +
                               "WHERE b.Status = 'Booked'";

                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("\n---- Booked Tickets ----");

                    while (reader.Read())
                    {
                        Console.WriteLine($"Booking ID: {reader["BookingID"]}");
                        Console.WriteLine($"Passenger Name: {reader["PassengerName"]}");
                        Console.WriteLine($"Class: {reader["Class"]}");
                        Console.WriteLine($"Journey Date: {reader["JourneyDate"]}");
                        Console.WriteLine($"Train Name: {reader["TrainName"]}");
                        Console.WriteLine($"From: {reader["Source"]}");
                        Console.WriteLine($"To: {reader["Destination"]}");
                        Console.WriteLine($"Departure Time: {reader["DepartureTime"]}");
                        Console.WriteLine($"Status: {reader["Status"]}");
                        Console.WriteLine("------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No booked tickets found.");
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
