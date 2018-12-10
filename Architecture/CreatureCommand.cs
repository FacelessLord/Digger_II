using Digger.Architecture;

namespace Digger
{
    public class CreatureCommand
    {
        public int _deltaX;
        public int _deltaY;
        public IObject _transformTo;
        public IObject _create;
    }
}