using System;
namespace ZooManager
{
    public class Zone
    {
        //encapsulate
        //the cell with occupant value
        private Animal _occupant = null;
        public Animal occupant
        {
            get { return _occupant; }
            set {
                _occupant = value;
                //if an animal occupied a cell then set the location of the animal as the cell's location
                if (_occupant != null) {
                    _occupant.location = location;
                }
            }
        }

        public Point location;

        public string emoji
        {
            get
            {
                if (occupant == null) return "";
                return occupant.emoji;
            }
        }

        public string rtLabel
        {
            get
            {
                if (occupant == null) return "";
                return occupant.reactionTime.ToString();
            }
        }

        public Zone(int x, int y, Animal animal)
        {
            location.x = x;
            location.y = y;

            occupant = animal;
        }
    }
}
