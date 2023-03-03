﻿using System;
using System.Collections.Generic;

namespace ZooManager
{
    //feature c
    public class Chick : Bird
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

        public override void Activate()
        {
            //base is Bird -> Animal, base on Activate in Animal (parent)
            base.Activate();
            Console.WriteLine("I am a chick.");
            //if found "cat" near it (distance 1) run away with distance 1
            Flee(new List<string>() { "cat" }, 1);
        }
    }
}

