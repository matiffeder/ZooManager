using System;
using System.Collections.Generic;

namespace ZooManager
{
    //feature c
    class Chick : Bird, IPrey
    {
        //deliever the arg from bird to chick
        public Chick(string name) : base(name)
        {
            //emoji for show the animal on the cells
            emoji = "🐥";
            //species = "bird";
            //"this" means this class, to seperate the arg of name
            //Bird (parent) has set the name
            //this.name = name;
            //reaction time is 6 (>=6) to 10 (<11)
            reactionTime = new Random().Next(6, 11);
        }

        /* 
         * active an animal, flee from cat (output name and location was written on parent)
         * call: no
         * called by: no 
         * parameter: no
         * return: no (void)
         */
        //override the Activate() in Animal
        public override void Activate()
        {
            //base is Bird -> Animal, base on Activate in Occupant (parent)
            base.Activate();
            Console.WriteLine("I am a chick.");
            //if found "cat" near it (distance 1) run away with distance 1
            Flee(new string[] { "cat" }, 1);
        }
        //there is Flee method with paremeters that in Occupant class and it is better than using the Flee from interface
        void IPrey._Flee(string[] targets, int distance) { }
    }
}

