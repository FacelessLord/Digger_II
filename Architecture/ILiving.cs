namespace Digger.Architecture
{
    public abstract class Living : GameObject
    {
        public abstract bool CanCreateBlocks(int x, int y);
    }
}