using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Alien : Occupant, IPredator
    {
        public Alien(string name)
        {
            //emoji for show on the cells
            emoji = "👽";
            //didn't use at the moment
            species = "alien";
            //"this" means this class, to seperate the arg of name
            //name will set in Interaction.AddAnimalToHolding
            this.name = name; 
            //reaction time is 1
            reactionTime = 10;
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
            //base is Occupant
            base.Activate();
            Console.WriteLine("I am an alien.");
            //make alien attack all things, includes alien
            //a list for name of occupant from occupants, occupantsName length is the same as Game.occupants
            string[] occupantsName = new string[Game.occupants.Length];
            //add the name string to occupantsName
            for (int i=0; i<Game.occupants.Length; i++)
            {
                occupantsName[i] = Game.occupants[i].name;
            }
            //hunt all things in the Game.occupants list if distance is 1
            //the function only hunt when it can hunt
            Hunt(occupantsName, 1);
        }
    }
}
