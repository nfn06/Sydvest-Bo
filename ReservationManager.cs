namespace Sydvest_Bo;

public class ReservationManager : ItemManager
{
    public Pagination Pagi = new();
    public int PropertyId;
    SqlManager SqlManager;
    public int CurrentPage = 1;

    public ReservationManager(SqlManager sqlManager, int propertyId)
    {
        SqlManager = sqlManager;
        PropertyId = propertyId;
    }

    internal override void Main()
    {
        PrintMenu();
        string input = Console.ReadLine()?.ToLower();
        while (input != "q")
        {
            switch (input)
            {
                case "p":
                    CurrentPage--;
                    PrintMenu();
                    input = Console.ReadLine()?.ToLower();
                    break;

                case "n":
                    CurrentPage++;
                    PrintMenu();
                    input = Console.ReadLine()?.ToLower();
                    break;

                case "a":
                    Add();
                    input = "q";
                    break;

                case "s":
                    Select();
                    input = "q";
                    break;

                default:
                    break;
            }
        }
    }

    internal override void PrintItems()
    {
        (CurrentResults, bool hasMore) = Pagi.GetPaginatedResults("reservation", "*", CurrentPage, 10);

        int index = (CurrentPage - 1) * 10 + 1;

        foreach (var item in CurrentResults)
        {
            string[] columns = item.Split(new[] { "   " }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (columns.Length > 1)
            {
                Console.WriteLine($"{index}. {columns[1]}");
                index++;
            }
        }

        Console.WriteLine(hasMore ? "More results available..." : "End of results.");
    }

    internal override void Add()
    {
        Console.Clear();
        Console.Write("Please enter customer name: ");
        string name = Console.ReadLine();

        Console.Write("Please enter reservation start date (dd-mm-yyyy): ");
        string startDate = Console.ReadLine();
        while (!DateOnly.TryParse(startDate, out _))
        {
            Console.WriteLine("Invalid Input. Please enter reservation start date (dd-mm-yyyy): ");
            startDate = Console.ReadLine();
        }

        Console.Write("Please enter reservation end date (dd-mm-yyyy): ");
        string endDate = Console.ReadLine();
        while (!DateOnly.TryParse(endDate, out _))
        {
            Console.WriteLine("Invalid Input. Please enter reservation end date (dd-mm-yyyy): ");
            endDate = Console.ReadLine();
        }

        SqlManager.Add("reservation", "customer, start_date, end_date, fk_property", $"'{name}','{startDate}','{startDate}',{PropertyId}");
    }

    internal override void Update(string value)
    {
        Console.Clear();
        Console.Write("Please enter new customer name for this reservation: ");
        string name = Console.ReadLine();

        Console.Write("Please enter new reservation start date (dd-mm-yyyy): ");
        string startDate = Console.ReadLine();
        while (!DateOnly.TryParse(startDate, out _))
        {
            Console.WriteLine("Invalid Input. Please enter new reservation start date (dd-mm-yyyy): ");
            startDate = Console.ReadLine();
        }

        Console.Write("Please enter new reservation end date (dd-mm-yyyy): ");
        string endDate = Console.ReadLine();
        while (!DateOnly.TryParse(endDate, out _))
        {
            Console.WriteLine("Invalid Input. Please enter new reservation end date (dd-mm-yyyy): ");
            endDate = Console.ReadLine();
        }

        string[] split = value.Split("   ");

        SqlManager.Update("reservation", $"customer = '{name}', start_date = '{startDate}', end_date = '{endDate}'", $"Id = {split[0]}");
    }

    internal override void Delete(string value)
    {
        string[] split = value.Split("   ");


        SqlManager.Delete("reservation", $"Id = {split[0]}");
    }
}
