using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZooManager
{
    //feature a, b
    class Raptor : Bird, IPredator
    {
        //deliever the arg from bird to raptor
        public Raptor(string name) : base(name)
        {
            //emoji for show the animal on the cells
            emoji = "🦅";
            //species = "bird";
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
            //check if there is prey can hunt
            (Dictionary<Direction, int> _, List<Direction> targetDirections) = Game.Seek(location.x, location.y, new string[] { "mouse", "cat" }, 1);
            //hunt "mouse" or "cat" if distance is 1
            //the function only hunt when it can hunt, so we don't need to put it in if condition
            //feature d, i
            Hunt(new string[] { "mouse", "cat" }, 1);
            //if no prey, then fly (false)
            if (targetDirections.Count < 1)
            {
                Game.Move(this, 2, null, null, false);
            }
        }
        //there is Hunt method with paremeters that in Occupant class and it is better than using the Hunt from interface
        void IPredator._Hunt(string[] targets, int distance) { }
    }
}
