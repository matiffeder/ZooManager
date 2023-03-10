using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Mouse : Animal, IPrey
    {
        public Mouse(string name) : base(name)
        {
            //emoji for show the animal on the cells
            emoji = "🐭";
            //didn't use at the moment
            species = "mouse";
            //"this" means this class, to seperate the arg of name
            //name will set in Interaction.AddAnimalToHolding
            //Animal (parent) has set the name
            //this.name = name;
            //reaction time is 1 (>=1) to 3 (<4)
            reactionTime = new Random().Next(1, 4);
        }

        /* 
         * active an animal, flee from raptor, cat (output name and location was written on parent)
         * call: no
         * called by: no 
         * parameter: no
         * return: no (void)
         */
        //override the Activate() in Animal
        public override void Activate()
        {
            //base is Animal, base on Activate in Occupant (parent)
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            //if found "raptor" or "cat" near it (distance 1) run away with distance 2
            //feature g
            Flee(new string[] { "raptor", "cat" }, 2);
        }
    }
}

