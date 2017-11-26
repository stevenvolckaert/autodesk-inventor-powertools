namespace StevenVolckaert.InventorPowerTools
{
    public interface IDocument
    {
        bool IsSelected { get; set; }
        string Name { get; }
        string FileName { get; }
    }
}
