namespace Tiling;
public interface ITileNode
{
    List<ITileNode> Children { get; }
    bool Visible { get; }
}
