using Digger.Architecture;

namespace Digger
{
    public class CreatureCommand
    {
        public int _deltaX;
        public int _deltaY;
        public IObject _transformTo;
        public IObject _create;
        
        public CreatureCommand(int dx,int dy)
        {
            _deltaX = dx;
            _deltaY = dy;
        }
        
        public CreatureCommand(int dx,int dy,IObject transformTo)
        {
            _deltaX = dx;
            _deltaY = dy;
            _transformTo = transformTo;
        }
    }
}