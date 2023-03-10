using System.Xml.Linq;

namespace ZooManager
{
    public class Bird : Animal
    {
        public Bird(string animalName) : base(animalName)
        {
            //didn't use at the moment
            species = "bird";
            //"this" means this class, to seperate the arg of name
            //this.name = name; 
            //we can also replace arg with "animalName" to do the same thing
            //name will set in Interaction.AddAnimalToHolding
            //Animal (parent) has set the name
            //name = animalName;
        }
    }
}
