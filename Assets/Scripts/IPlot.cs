public interface IPlot
{
    bool IsOccupied { get; set; }
    void TryPlaceTower();
}
