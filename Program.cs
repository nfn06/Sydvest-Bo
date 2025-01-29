namespace Sydvest_Bo;

class Program
{
    public static SqlManager sqlManager = new();
    static void Main(string[] args)
    {
        SqlManager.Connect();
        MainMenu();
    }

    public static void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Sydvest-Bi Management System!\n");
        Console.WriteLine("Please select an option by entering the corresponding number:");
        Console.WriteLine("1. Summer Houses");
        Console.WriteLine("2. Owners");
        Console.WriteLine("3. Exit the Program");
        Console.Write("\nEnter your choice: ");
        string input = Console.ReadLine();

        switch (input)
        {
            case "1":
                {
                    Console.Clear();
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