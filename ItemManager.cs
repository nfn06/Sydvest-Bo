namespace Sydvest_Bo;

public abstract class ItemManager
{
    internal List<string> CurrentResults = new();
    internal abstract void Main();
    internal void PrintMenu()
    {
        Console.Clear();
        PrintItems();
        Console.WriteLine("----------------------------");
        Console.WriteLine("[N]ext, [P]revious, [A]dd, [S]elect or [Q]uit.");
    }
    internal abstract void PrintItems();
    internal abstract void Add();
    internal void Select()
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
                Update(CurrentResults[selectedNumber].Split(". ")[1]);
                break;

            case "d":
                Delete(CurrentResults[selectedNumber].Split(". ")[1]);
                break;
        }
    }
    internal abstract void Update(string values);
    internal abstract void Delete(string values);
}
