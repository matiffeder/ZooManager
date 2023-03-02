using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZooManager
{
    public class Raptor : Bird
    {
        //deliever the arg from bird to raptor
        public Raptor(string name) : base(name)
        {
            emoji = "🦅";
            //species = "raptor";
            //"this" means this class, to seperate the arg of name
            //this.name = name; 
            //reaction time is 1
            reactionTime = 1; 
        }

        public override void Activate()
        {
            //base is Bird -> Animal
            base.Activate();
            Console.WriteLine("I am a raptor.");
            //Console.WriteLine("-------------------"+species);
            //Console.WriteLine("-------------------"+ name);
            Hunt(new List<string>() { "mouse", "cat" }, 1);
        }

        /*public void Hunt(List<string> targets)
        {
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
            if (targetDirections.Count > 0)
            {
                Game.Attack(this, targetDirections[new Random().Next(0, targetDirections.Count)]);
            }

            //checking all the directions, if found cat (ture), then attack that direction (reaplce the cell)
            /*if (Game.Seek(location.x, location.y, Direction.up, "cat") || Game.Seek(location.x, location.y, Direction.up, "mouse"))
            {
                Game.Attack(this, Direction.up);
            }
            else if (Game.Seek(location.x, location.y, Direction.down, "cat") || Game.Seek(location.x, location.y, Direction.down, "mouse"))
            {
                Game.Attack(this, Direction.down);
            }
            else if (Game.Seek(location.x, location.y, Direction.left, "cat") || Game.Seek(location.x, location.y, Direction.left, "mouse"))
            {
                Game.Attack(this, Direction.left);
            }
            else if (Game.Seek(location.x, location.y, Direction.right, "cat") || Game.Seek(location.x, location.y, Direction.right, "mouse"))
            {
                Game.Attack(this, Direction.right);
            }*/
        //}
    }
}
