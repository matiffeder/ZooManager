using System;
namespace ZooManager
{
    public class Zone
    {
        //encapsulated animal info in a cell
        private Animal _occupant = null;
        public Animal occupant
        {
            get { return _occupant; }
            set {
                //set the value of occupant
                _occupant = value;
                //if the cell is not empty, get the location of the cell and set this location to the occupant
                if (_occupant != null) {
                    _occupant.location = location;
                }
            }
        }

        //zone location
        public Point location;

        public string emoji
        {
            get
            {
                //if is empty return the empty emoji string 
                if (occupant == null) return "";
                //if not empty return the emoji
                return occupant.emoji;
            }
        }

        //the label to show reaction time
        public string rtLabel
        {
            get
            {
                //if is empty return the empty string 
                if (occupant == null) return "";
                //if not empty return reaction time in string
                return occupant.reactionTime.ToString();
            }
        }

        //for a set up a cell with location and animal(can be null)
        public Zone(int x, int y, Animal animal)
        {
            location.x = x;
            location.y = y;

            occupant = animal;
        }
    }
}
