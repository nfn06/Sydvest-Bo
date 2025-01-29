namespace Sydvest_Bo;

public class OwnerManager : ItemManager
{
    public Pagination Pagi = new();
    public int CurrentPage = 1;
    internal override void Main()
    {
        PrintMenu();
        string input = Console.ReadLine()?.ToLower();
        
        switch (input)
        {
            case "p":
                CurrentPage--;
                PrintMenu();
                break;

            case "n":
                CurrentPage++;
                PrintMenu();
                break;

            case "a":
                Add();
                break;

            case "s":
                break;

            default:
                break;
        }

        throw new NotImplementedException();
    }

    internal override void PrintMenu()
    {
        PrintItems();
        Console.WriteLine("----------------------------");
        Console.WriteLine("[N]ext, [P]revious, [A]dd, [S]elect or [Q]uit.");
        throw new NotImplementedException();
    }

    internal override void PrintItems()
    {
        Pagi.GetPaginatedResults("property", "address", CurrentPage, 10);
        throw new NotImplementedException();
    }

    internal override void Add()
    {
        throw new NotImplementedException();
    }

    internal override void Update()
    {
        throw new NotImplementedException();
    }

    internal override void Delete()
    {
        throw new NotImplementedException();
    }
}
