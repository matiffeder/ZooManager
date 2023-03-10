using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Animal : Occupant
    {
        public Animal(string name)
        {
            //didn't use at the moment
            species = "animal";
            //"this" means this class, to seperate the arg of name
            //name will set in Interaction.AddAnimalToHolding
            this.name = name;
        }
    }
}
