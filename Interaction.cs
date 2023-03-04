using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//if holding have an animal and user pick up the other animal on the zone the holding animal will disappear

namespace ZooManager
{
    public static class Interaction
    {
        //the x number of the zone
        private static int _numCellsX = 4;
        public static int numCellsX { get { return _numCellsX; } set { _numCellsX = value; } }

        //the y number of the zone
        private static int _numCellsY = 4;
        public static int numCellsY { get { return _numCellsY; } set { _numCellsY = value; } }

        //the max x number of the zone
        static private int maxCellsX = 10;
        //the max y number of the zone
        static private int maxCellsY = 10;

        //2d list for create a zone, will start with y
        static private List<List<Zone>> _animalZones = new List<List<Zone>>();
        static public List<List<Zone>> animalZones { get { return _animalZones; } set { _animalZones = value; } }

        //the place to pick up an animal
        static private Zone _holdingPen = new Zone(-1, -1, null);
        static public Zone holdingPen { get { return _holdingPen; } set { _holdingPen = value; } }

        /* 
        * create the game zone
        * call: no
        * called by: index.razor
        * parameter: no
        * return: no (void)
        */
        static public void SetUpGame()
        {
            //create the zone by row, the first row y==0
            for (var y = 0; y < numCellsY; y++)
            {
                //create a new list to store a row with numCellsX cells
                List<Zone> rowList = new List<Zone>();
                //add a row by x according the current x length
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                //add the row
                animalZones.Add(rowList);
            }
        }

        /* 
        * add row or col on zone
        * call: no
        * called by: index.razor
        * parameter: Direction - click on right(left) or down(up) button
        * return: no (void)
        */
        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                //numCellsY can't bigger than max 
                if (numCellsY >= maxCellsY) return;
                //create a new list to store a row with numCellsX cells
                List<Zone> rowList = new List<Zone>();
                //add a row by x according the current x length
                for (var x = 0; x < numCellsX; x++)
                {
                    //add a cell in the row
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                //add 1 to y number
                numCellsY++;
                //add the row
                if (d == Direction.down) animalZones.Add(rowList);
            }
            else //if left or right
            {
                //numCellsX can't bigger than max 
                if (numCellsX >= maxCellsX) return;
                //add a col by y according the current x length
                for (var y = 0; y < numCellsY; y++)
                {
                    //identify the current row (each row)
                    var rowList = animalZones[y];
                    //add a cell into the last of the row
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                //add 1 to x number
                numCellsX++;
            }
        }

        /* 
        * active the diff actions when click on a cell, add an animal or pick up an animal
        * call: no
        * called by: index.razor
        * parameter: Zone - the cell that clicked
        * return: no (void)
        */
        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            //in console line write if emoji=="" then emoji="none" else clickedZone.emoji
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            //if click on a cell not empty, then console line write the location
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            //if pickup zone is empty and click on a cell not empty
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                //take an animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                //change the animal loaction (not holdingPen location) to pickup zone
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                //remove animal from the clicked cell
                clickedZone.occupant = null;
                //run animal movements
                ActivateAnimals();
            }
            //if pickup zone is not empty and click on a cell is empty
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                //set animal from holding pen and put it on the clicked cell
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                //change the animal loaction to clicked zone
                clickedZone.occupant.location = clickedZone.location;
                //make pickup zone empty
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                //run animal movements
                ActivateAnimals();
            }
            //if pickup zone is not empty and click on a cell is not empty
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
            }
        }

        /* 
        * add a new animal to pickup zone
        * call: no
        * called by: index.razor
        * parameter: string - the add animal button user clicked
        * return: no (void)
        */
        static public void AddAnimalToHolding(string animalType)
        {
            //if pickup zone is not empty
            if (holdingPen.occupant != null) return;
            //why not use Cat as the name, why use fluffy
            //add a new animal to pickup zone with name
            //if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "cat") holdingPen.occupant = new Cat("cat");
            //if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("mouse");
            if (animalType == "raptor") holdingPen.occupant = new Raptor("raptor");
            if (animalType == "chick") holdingPen.occupant = new Chick("chick");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            //run animal movements
            ActivateAnimals();
        }

        /* 
        * active animals by reaction time and from zone coordinate (0,0)
        * call: no
        * called by: Interaction
        * parameter: no
        * return: no (void)
        */
        //if the actor has less index it will possible to move more than once
        static private void ActivateAnimals()
        {
            //activate animals by the order of reaction time
            for (var r = 1; r <= 10; r++)
            {
                //from the first row (y==0)
                for (var y = 0; y < numCellsY; y++)
                {
                    //from the left cell
                    for (var x = 0; x < numCellsX; x++)
                    {
                        //if the cell has an animal and it is its reactionTime in order
                        var zone = animalZones[y][x];
                        if (zone.occupant != null && zone.occupant.reactionTime == r)
                        {
                            zone.occupant.Activate();
                        }
                    }
                }
            }
        }
    }
}

