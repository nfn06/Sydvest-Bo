using System.Runtime.CompilerServices;

namespace Sydvest_Bo;

class Program
{
    public static SqlManager sqlManager = new();
    static void Main(string[] args)
    {
        SqlManager.Connect();
        MainMenu();

        SqlManager.Close();
    }

    public static void MainMenu()
    {
        string input = "";

        while (input != "3")
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Sydvest-Bi Management System!\n");
            Console.WriteLine("Please select an option by entering the corresponding number:");
            Console.WriteLine("1. Summer Houses");
            Console.WriteLine("2. Owners");
            Console.WriteLine("3. Exit the Program");
            Console.Write("\nEnter your choice: ");
            input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        RegionMenu();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        PersonManager personManager = new(sqlManager);
                        personManager.Main();
                        break;
                    }
                case "3":
                    {
                        Console.Clear();
                        Console.WriteLine("Exiting the program. Goodbye!");
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                        break;
                    }
            }
        }
    }

    public static void RegionMenu()
    {
        bool validChoice = false;
        while (!validChoice)
        {
            Console.Clear();
            Console.WriteLine("Select a region or press 'Q' to return to the main menu:");

            List<string> regions = SqlManager.GetAllRegions();

            if (regions.Count > 0)
            {
                for (int i = 0; i < regions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {regions[i]}");
                }

                Console.Write("\nEnter your choice: ");
                string input = Console.ReadLine()?.ToLower();

                if (input == "q")
                {
                    MainMenu();
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= regions.Count)
                {
                    string selectedRegion = regions[choice - 1];
                    Console.Clear();
                    Console.WriteLine($"You selected: {selectedRegion}");
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("No regions found.");
                break;
            }
        }
    }
}