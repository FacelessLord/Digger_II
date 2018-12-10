namespace Digger.Architecture
{
    public interface ILiving : IObject
    {
        bool CanCreateBlocks(int x, int y);
    }
}