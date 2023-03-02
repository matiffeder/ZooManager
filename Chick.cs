using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Chick : Bird
    {
        //deliever the arg from bird to chick
        public Chick(string name) : base(name)
        {
            emoji = "🐥";
            //species = "bird";
            //"this" means this class, to seperate the arg of name
            //this.name = name;
            //reaction time is 6 (<=6) to 10 (<11)
            reactionTime = new Random().Next(6, 11);
            //reactionTime = 1;
        }

        public override void Activate()
        {
            //base is Bird -> Animal
            base.Activate();
            Console.WriteLine("I am a chick.");
            Flee(new List<string>() { "cat" }, 1);
        }

        /*public void Flee(List<string> targets)
        {
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
            foreach (var direction in targetDirections)
            {
                if (direction == Direction.up) if (Game.Retreat(this, Direction.down)) return;
                if (direction == Direction.down) if (Game.Retreat(this, Direction.up)) return;
                if (direction == Direction.left) if (Game.Retreat(this, Direction.right)) return;
                if (direction == Direction.right) if (Game.Retreat(this, Direction.left)) return;
            }*/

            //checking all the directions, if found cat (ture), then run away from that direction (change the cell)
            /*if (Game.Seek(location.x, location.y, Direction.up, "cat"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "cat"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "cat"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "cat"))
            {
                if (Game.Retreat(this, Direction.left)) return;
            }*/
        //}
    }
}

