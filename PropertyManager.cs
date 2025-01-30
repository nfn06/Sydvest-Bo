namespace Sydvest_Bo;

public class PropertyManager : ItemManager
{
    public Pagination Pagi = new();
    SqlManager SqlManager;
    public int CurrentPage = 1;
    public int RegionId;
    public PropertyManager(SqlManager sqlManager, int regionId)
    {
        SqlManager = sqlManager;
        RegionId = regionId;
    }

    internal override void Main()
    {
        PrintMenu();
        Console.WriteLine(" -- Press C to check reservations for a property --");
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

                case "c":
                    CheckReservations();
                    input = "q";
                    break;

                default:
                    break;
            }
        }
    }

    internal override void PrintItems()
    {
        (CurrentResults, bool hasMore) = Pagi.GetPaginatedResults("property", "*", CurrentPage, 10);

        int index = (CurrentPage - 1) * 10 + 1;

        foreach (var item in CurrentResults)
        {
            string[] columns = item.Split(new[] { "   " }, 2, StringSplitOptions.RemoveEmptyEntries);

            if (columns.Length > 1)
            {
                if (columns[1].Split("   ")[2].Replace(" ", "") == RegionId.ToString())
                    Console.WriteLine($"{index}. {columns[1]}");
                index++;
            }
        }

        Console.WriteLine(hasMore ? "More results available..." : "End of results.");
    }

    internal override void Add()
    {
        Console.Clear();
        Console.Write("Please enter address: ");
        string address = Console.ReadLine();

        Console.Clear();
        Console.Write("Please enter owner's name: ");
        string name = Console.ReadLine();

        SqlManager.Add("property", "address, owner, fk_region", $"'{address}', '{name}', {RegionId}");
    }

    internal override void Update(string value)
    {
        Console.Clear();
        Console.Write("Please enter new owner's name: ");
        string name = Console.ReadLine();


        string[] split = value.Split("   ");

        SqlManager.Update("property", $"owner = '{name}'", $"Id = {split[0]}");
    }

    internal override void Delete(string value)
    {
        string[] split = value.Split("   ");

        SqlManager.Delete("property", $"Id = {split[0]}");
    }

    internal void CheckReservations()
    {
        bool validInput = false;
        int selectedNumber = 0;

        while (!validInput)
        {
            Console.Write("Please select a valid number from the above: ");
            validInput = int.TryParse(Console.ReadLine(), out selectedNumber);

            if (CurrentResults.Count() < selectedNumber)
                validInput = false;
        }

        selectedNumber--;

        ReservationManager reservationManager = new(SqlManager, Convert.ToInt16(CurrentResults[selectedNumber].Split("   ")[0].Split(" ")[1]));
        reservationManager.Main();
    }
}
