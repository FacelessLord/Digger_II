namespace Digger.Objects.Api
{
    public abstract class Living : GameObject
    {
        public abstract bool CanCreateBlocks(int x, int y);
    }
}