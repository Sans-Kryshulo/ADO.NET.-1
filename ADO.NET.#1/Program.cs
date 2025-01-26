using System;
using System.Data.SqlClient;

class Program
{
    static SqlConnection connection; // Глобальне з'єднання

    static void Main()
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VegetablesFruits;Integrated Security=True;";
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Connect to the database");
            Console.WriteLine("2. Disconnect from the database");
            Console.WriteLine("3. Display all information");
            Console.WriteLine("4. Display all names");
            Console.WriteLine("5. Display all colors");
            Console.WriteLine("6. Show maximum calories");
            Console.WriteLine("7. Show minimum calories");
            Console.WriteLine("8. Show average calories");
            Console.WriteLine("9. Show vegetable count");
            Console.WriteLine("10. Show fruit count");
            Console.WriteLine("11. Show count by specified color");
            Console.WriteLine("12. Show count by each color");
            Console.WriteLine("13. Show items below specified calories");
            Console.WriteLine("14. Show items above specified calories");
            Console.WriteLine("15. Show items within calorie range");
            Console.WriteLine("16. Show all yellow or red items");
            Console.WriteLine("17. Exit");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ConnectToDatabase(connectionString);
                    break;
                case "2":
                    DisconnectFromDatabase();
                    break;
                case "3":
                    DisplayAllInformation();
                    break;
                case "4":
                    DisplayAllNames();
                    break;
                case "5":
                    DisplayAllColors();
                    break;
                case "6":
                    ShowMaxCalories();
                    break;
                case "7":
                    ShowMinCalories();
                    break;
                case "8":
                    ShowAverageCalories();
                    break;
                case "9":
                    ShowVegetableCount();
                    break;
                case "10":
                    ShowFruitCount();
                    break;
                case "11":
                    ShowCountByColor();
                    break;
                case "12":
                    ShowCountByEachColor();
                    break;
                case "13":
                    ShowItemsBelowCalories();
                    break;
                case "14":
                    ShowItemsAboveCalories();
                    break;
                case "15":
                    ShowItemsWithinCalorieRange();
                    break;
                case "16":
                    ShowYellowOrRedItems();
                    break;
                case "17":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void ConnectToDatabase(string connectionString)
    {
        if (connection == null)
        {
            connection = new SqlConnection(connectionString);
        }

        if (connection.State == System.Data.ConnectionState.Closed)
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to the database!");
                Console.WriteLine($"Server: {connection.DataSource}");
                Console.WriteLine($"Database: {connection.Database}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("You are already connected to the database.");
        }
    }

    static void DisconnectFromDatabase()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Successfully disconnected from the database.");
        }
        else
        {
            Console.WriteLine("The connection is already closed or was not established.");
        }
    }

    static void DisplayAllInformation()
    {
        string query = "SELECT * FROM Products";
        ExecuteAndPrintQuery(query);
    }

    static void DisplayAllNames()
    {
        string query = "SELECT Name FROM Products";
        ExecuteAndPrintQuery(query);
    }

    static void DisplayAllColors()
    {
        string query = "SELECT DISTINCT Color FROM Products";
        ExecuteAndPrintQuery(query);
    }

    static void ShowMaxCalories()
    {
        string query = "SELECT MAX(Calories) FROM Products";
        ExecuteAndPrintQuery(query);
    }

    static void ShowMinCalories()
    {
        string query = "SELECT MIN(Calories) FROM Products";
        ExecuteAndPrintQuery(query);
    }

    static void ShowAverageCalories()
    {
        string query = "SELECT AVG(Calories) FROM Products";
        ExecuteAndPrintQuery(query);
    }
    //////////
    static void ShowVegetableCount()
    {
        ExecuteAndPrintQuery("SELECT COUNT(*) FROM Products WHERE Type='Vegetable'");
    }

    static void ShowFruitCount()
    {
        ExecuteAndPrintQuery("SELECT COUNT(*) FROM Products WHERE Type='Fruit'");
    }

    static void ShowCountByColor()
    {
        Console.Write("Enter color: ");
        string color = Console.ReadLine();
        ExecuteAndPrintQuery($"SELECT COUNT(*) FROM Products WHERE Color='{color}'");
    }

    static void ShowCountByEachColor()
    {
        ExecuteAndPrintQuery("SELECT Color, COUNT(*) FROM Products GROUP BY Color");
    }

    static void ShowItemsBelowCalories()
    {
        Console.Write("Enter maximum calories: ");
        int maxCalories = int.Parse(Console.ReadLine());
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE Calories < {maxCalories}");
    }

    static void ShowItemsAboveCalories()
    {
        Console.Write("Enter minimum calories: ");
        int minCalories = int.Parse(Console.ReadLine());
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE Calories > {minCalories}");
    }

    static void ShowItemsWithinCalorieRange()
    {
        Console.Write("Enter minimum calories: ");
        int minCalories = int.Parse(Console.ReadLine());
        Console.Write("Enter maximum calories: ");
        int maxCalories = int.Parse(Console.ReadLine());
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE Calories BETWEEN {minCalories} AND {maxCalories}");
    }

    static void ShowYellowOrRedItems()
    {
        ExecuteAndPrintQuery("SELECT * FROM Products WHERE Color IN ('Yellow', 'Red')");
    }
    ////////
    static void ExecuteAndPrintQuery(string query)
    {
        if (connection == null || connection.State == System.Data.ConnectionState.Closed)
        {
            Console.WriteLine("Database connection is not established.");
            return;
        }

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}