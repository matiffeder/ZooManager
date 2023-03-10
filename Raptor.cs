using System;
using System.Xml.Linq;
using System.Collections.Generic;
namespace ZooManager
{
    //feature a, b
    public class Raptor : Bird
    {
        //deliever the arg from bird to raptor
        public Raptor(string name) : base(name)
        {
            emoji = "🦅";
            //species = "raptor";
            //"this" means this class, to seperate the arg of name
            //Bird (parent) has set the name
            //this.name = name; 
            //reaction time is 1
            reactionTime = 1; 
        }

        /* 
         * active an animal, hunt mouse or cat (output name and location was written on parent)
         * call: no
         * called by: no 
         * parameter: no
         * return: no (void)
         */
        //override the Activate() in Animal
        public override void Activate()
        {
            //base is Bird -> Animal
            base.Activate();
            Console.WriteLine("I am a raptor.");
            (Dictionary<Direction, int> _directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, new List<string>() { "mouse", "cat" }, 1);
            //hunt "mouse" or "cat" if distance is 1
            //the function only hunt when it can hunt, so we don't need to put it in if condition
            //feature d, i
            Hunt(new List<string>() { "mouse", "cat" }, 1);
            if (targetDirections.Count < 1)
            {
                Game.Move(this, 2, null, null, false);
            }
        }
    }
}
