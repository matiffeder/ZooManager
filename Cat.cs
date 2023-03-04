using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            //emoji for show the animal on the cells
            emoji = "🐱";
            species = "cat";
            //"this" means this class, to seperate the arg of name
            //name will set in Interaction.AddAnimalToHolding
            this.name = name;
            //reaction time is 1 (>=1) to 5 (<4)
            reactionTime = new Random().Next(1, 6); 
        }

        /* 
         * active an animal, hunt mouse or chick, flee from raptor (output name and location was written on parent)
         * call: Game - Seek
         * called by: no 
         * parameter: no
         * return: no (void)
         */
        //override the Activate() in Animal
        //feature e, k
        public override void Activate()
        {
            //base is Animal, base on Activate in Animal (parent)
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            //get the attack directions of targets by distance
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, new List<string>() { "mouse", "chick" }, 1);
            //hunt can run away from a raptor, since it also move to the other cell
            //so it will run away first when hunt
            //hunt "mouse" or "chick" if distance is 1
            //feature e, k
            Hunt(new List<string>() { "mouse", "chick" }, 1);
            //if can't hunt then run away
            //if found "raptor" near it (distance 1) run away with distance 1
            //feature e, k
            if (targetDirections.Count < 1)
            {
                Flee(new List<string>() { "raptor" }, 2);
            }
        }
    }
}

