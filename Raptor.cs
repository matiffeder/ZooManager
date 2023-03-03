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

        public override void Activate()
        {
            //base is Bird -> Animal
            base.Activate();
            Console.WriteLine("I am a raptor.");
            //hunt "mouse" or "cat" if distance is 1
            //feature d
            Hunt(new List<string>() { "mouse", "cat" }, 1);
        }
    }
}
