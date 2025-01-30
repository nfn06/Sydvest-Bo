namespace Sydvest_Bo;

public class PersonManager : ItemManager
{
    public Pagination Pagi = new();
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

    internal override void PrintItems()
    {
        (CurrentResults, bool hasMore) = Pagi.GetPaginatedResults("person", "*", CurrentPage, 10);

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
        Console.Write("Please enter new person's name: ");
        string input = Console.ReadLine();

        SqlManager.Add("person", "full_name", input);
    }

    internal override void Update(string value)
    {
        string[] split = value.Split("   ");
        Console.WriteLine($"Please type in the new name for {split[1]}");
        string input = Console.ReadLine();

        SqlManager.Update("Person", $"full_name = '{input}'", $"full_name = '{split[1]}'");
    }

    internal override void Delete(string value)
    {
        SqlManager.Delete("Person", $"full_name = '{value}'");
    }
}
