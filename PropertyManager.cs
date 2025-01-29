namespace Sydvest_Bo;

public class PropertyManager : ItemManager
{
    public Pagination Pagi = new();
    SqlManager SqlManager;
    public int CurrentPage = 1;
    public int SelectedRegionId;

    public PropertyManager(SqlManager sqlManager, int regionId)
    {
        SqlManager = sqlManager;
        SelectedRegionId = regionId;
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
        (CurrentResults, bool hasMore) = Pagi.GetPaginatedResults("property", "address", CurrentPage, 10);

        CurrentResults = CurrentResults.Where(item => item.Contains($"Region {SelectedRegionId}")).ToList();

        foreach (var item in CurrentResults)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(hasMore ? "More results available..." : "End of results.");
    }

    internal override void Add()
    {
        Console.Clear();
        Console.Write("Please enter new property address: ");
        string address = Console.ReadLine();

        Console.Write("Enter property type ID: ");
        int typeId = int.Parse(Console.ReadLine() ?? "1");
        Console.Write("Enter owner ID: ");
        int ownerId = int.Parse(Console.ReadLine() ?? "1");

        SqlManager.Add("property", "address, fk_type, fk_owner, fk_region", $"'{address}', {typeId}, {ownerId}, {SelectedRegionId}");
    }

    internal override void Update(string value)
    {
        Console.WriteLine($"Please type in the new address for property {value}");
        string input = Console.ReadLine();

        SqlManager.Update("Property", $"address = '{input}'", $"address = '{value}' AND fk_region = {SelectedRegionId}");
    }

    internal override void Delete(string value)
    {
        SqlManager.Delete("Property", $"address = '{value}' AND fk_region = {SelectedRegionId}");
    }
}