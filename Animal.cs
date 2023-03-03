﻿using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Animal
    {
        //emoji for show the animal on the cells
        public string emoji;
        //changed these lines in Game
        //if (animalZones[y][x].occupant.species == target)
        //to
        //if (animalZones[y][x].occupant.name == target)
        public string species;
        //used to identify animal and what it can do
        public string name;
        //defult reaction time
        public int reactionTime = 5;

        //the location of an animal
        public Point location;

        public void ReportLocation()
        {
            Console.WriteLine($"I am at {location.x},{location.y}");
        }

        //virtual is used in parent class, so that the child can base on it and override it, it should be public
        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }

        /* 
         * hunt an animal in a random loacation
         * call: Attack - Game
         * called by: Cat, Raptor
         * parameter: List<string> - targets animal, int - the distance that it can hunt
         * return: no (void)
         */
        virtual public void Hunt(List<string> targets, int distance)
        {
            //I don't know how to not define the directionInfo 
            //get the directions of targets by distance
            //todo : need to make sure the cells bwtween target and attacker are empty
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, distance);
            //if found targets in some directions
            if (targetDirections.Count > 0)
            {
                //attack the target in the distance with a radom direction
                Game.Attack(this, targetDirections[new Random().Next(0, targetDirections.Count)], distance);
            }
        }

        /* 
         * run away from an animal to a random loacation
         * call: Move - Game, Seek - Game
         * called by: Cat, Raptor, Chick
         * parameter: List<string> - targets animal, int - the distance of cells to run
         * return: no (void)
         */
        //feature g (mouse flee), h, i, j (cat flee) 
        virtual public void Flee(List<string> targets, int distance)
        {
            //I don't know how to not define the directionInfo 
            //get the directions of targets by distance 1
            //feature i
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
            //if found targets in some directions
            if (targetDirections.Count > 0)
            {
                //move then get num of moved cells and orig direction of self (this)
                (int movedCell, Direction? origDirection) = Game.Move(this, distance, null);
                //save the total moved cells num
                int allMovedCell = movedCell;
                //if movedCell==0 (can't move) or distance - allMovedCell <= 0 (no more move) then stop move again
                while (movedCell != 0 && distance - allMovedCell > 0)
                {
                    //move also reomve the previous origDirection then get num of moved cells and orig direction of self (this)
                    //distance-allMovedCell is remain cells that can move
                    //feature h
                    (movedCell, origDirection) = Game.Move(this, distance - allMovedCell, origDirection);
                    //save the total moved cells num
                    allMovedCell += movedCell;
                }
            }
        }
        //if the cat's index is lower it can attack and flee
    }
}
