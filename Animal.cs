using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Animal
    {
        public string emoji;
        public string species;
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        
        public Point location;

        /*public Dictionary<Direction, int> directionInfo = new Dictionary<Direction, int>();
        public List<Direction> targetDirections = new List<Direction>();*/

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }

        //virtual is used in parent class, so that the child can base on it and override it
        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }
        virtual public void Hunt(List<string> targets, int distance)
        {
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, distance);
            if (targetDirections.Count > 0)
            {
                Game.Attack(this, targetDirections[new Random().Next(0, targetDirections.Count)], distance);
            }
        }
        virtual public void Flee(List<string> targets, int distance)
        {
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
            if (targetDirections.Count > 0)
            {
                (int movedCell, Direction? origDirection) = Game.Move(this, distance, null);
                int allMovedCell = movedCell;
                while (movedCell != 0 && distance - allMovedCell > 0)
                {
                    (movedCell, origDirection) = Game.Move(this, distance - allMovedCell, origDirection);
                    allMovedCell += movedCell;
                }
            }
            /*foreach (var direction in targetDirections)
            {
                if (direction == Direction.up) if (Game.Retreat(this, Direction.down, distance)) return;
                if (direction == Direction.down) if (Game.Retreat(this, Direction.up, distance)) return;
                if (direction == Direction.left) if (Game.Retreat(this, Direction.right, distance)) return;
                if (direction == Direction.right) if (Game.Retreat(this, Direction.left, distance)) return;
            }*/
        }
    }
}
