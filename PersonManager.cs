namespace Sydvest_Bo;

public class PersonManager : ItemManager
{
    public Pagination Pagi = new();
    List<string> CurrentResults = new();
    SqlManager SqlManager;
    public int CurrentPage = 1;

    public PersonManager(SqlManager sqlManager)
    {
        SqlManager = sqlManager;
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

    internal override void PrintMenu()
    {
        Console.Clear();
        PrintItems();
        Console.WriteLine("----------------------------");
        Console.WriteLine("[N]ext, [P]revious, [A]dd, [S]elect or [Q]uit.");
    }

    internal override void PrintItems()
    {
        (CurrentResults, bool hasMore) = Pagi.GetPaginatedResults("person", "full_name", CurrentPage, 10);

        foreach (var item in CurrentResults)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine(hasMore ? "More results available..." : "End of results.");
    }

    internal override void Add()
    {
        Console.Clear();
        Console.Write("Please enter new person's name: ");
        string input = Console.ReadLine();

        SqlManager.Add("person", "full_name", input);
    }

    internal override void Select()
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

        string input = "";

        while (input != "u" && input != "d")
        {
            Console.Clear();
            Console.WriteLine("[U]pdate or [D]elete?");
            input = Console.ReadLine().ToLower();
        }

        switch (input)
        {
            case "u":
                Update(CurrentResults[selectedNumber]);
                break;

            case "d":
                Delete(CurrentResults[selectedNumber]);
                break;
        }
    }

    internal override void Update(string value)
    {
        Console.WriteLine($"Please type in the new name for {value}");
        string input = Console.ReadLine();

        SqlManager.Update("person", $"full_name = '{input}'", $"full_name = '{value}'");
    }

    internal override void Delete(string value)
    {
        SqlManager.Delete("person", $"full_name = '{value}'");
    }
}
