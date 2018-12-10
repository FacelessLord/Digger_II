using System.Drawing;
using Digger.Architecture;

namespace Digger
{
    public class CreatureAnimation
    {
        public CreatureCommand Command;
        public ICreature Creature;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}