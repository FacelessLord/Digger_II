using Digger.Objects.Api;

namespace Digger.Architecture
{
    public class CreatureCommand
    {
        public int _deltaX;
        public int _deltaY;
        public GameObject _transformTo;
        public GameObject _create;
        
        public CreatureCommand(int dx,int dy)
        {
            _deltaX = dx;
            _deltaY = dy;
        }
        
        public CreatureCommand(int dx,int dy,GameObject transformTo)
        {
            _deltaX = dx;
            _deltaY = dy;
            _transformTo = transformTo;
        }
    }
}