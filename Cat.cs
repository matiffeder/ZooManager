using System;
using System.Collections.Generic;

namespace ZooManager
{
    class Cat : Animal, IPredator, IPrey
    {
        public Cat(string name) : base(name)
        {
            //emoji for show the animal on the cells
            emoji = "🐱";
            //didn't use at the moment
            species = "cat";
            //"this" means this class, to seperate the arg of name
            //name will set in Interaction.AddAnimalToHolding
            //Animal (parent) has set the name
            //this.name = name;
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
            //base is Animal, base on Activate in Occupant (parent)
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            //get the attack directions of targets by distance
            (Dictionary<Direction, int> _, List<Direction> targetDirections) = Game.Seek(location.x, location.y, new string[] { "mouse", "chick" }, 1);
            //hunt can run away from a raptor, since it also move to the other cell
            //so it will run away first when hunt
            //hunt "mouse" or "chick" if distance is 1
            //the function only hunt when it can hunt, so we don't need to put it in if condition
            //feature e, k
            Hunt(new string[] { "mouse", "chick" }, 1);
            //if can't hunt then run away
            //if found "raptor" near it (distance 1) run away with distance 2
            //feature e, k
            if (targetDirections.Count < 1)
            {
                Flee(new string[] { "raptor" }, 2);
            }

            //-----if really want to flee first
            /*
            //get the flee directions of targets by distance
            (Dictionary<Direction, int> _directionInfo, List<Direction> fleeDirections) = Game.Seek(location.x, location.y, new string[] { "raptor" }, 1);
            //if can't find raptors. only hunt when no raptor
            //feature e, k
            if (fleeDirections.Count < 1)
            {
                //hunt "mouse" or "chick" if distance is 1
                Hunt(new string[] { "mouse", "chick" }, 1);
            }
            //if found "raptor" near it (distance 1) run away with distance 2
            //the function only flee when it can flee, so we don't need to put it in if condition
            //feature e, k
            Flee(new string[] { "raptor" }, 2);
            */
        }
        //there is Hunt method with paremeters that in Occupant class and it is better than using the Hunt from interface
        void IPredator._Hunt(string[] targets, int distance) { }
        //there is Flee method with paremeters that in Occupant class and it is better than using the Flee from interface
        void IPrey._Flee(string[] targets, int distance) { }
    }
}

