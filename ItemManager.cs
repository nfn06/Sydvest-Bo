namespace Sydvest_Bo;

public abstract class ItemManager
{
    internal abstract void Main();
    internal abstract void PrintMenu();
    internal abstract void PrintItems();
    internal abstract void Add();
    internal abstract void Select();
    internal abstract void Update(string values);
    internal abstract void Delete(string values);
}
