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
        private static int _numCellsX = 4;
        public static int numCellsX { get { return _numCellsX; } set { _numCellsX = value; } }

        private static int _numCellsY = 4;
        public static int numCellsY { get { return _numCellsY; } set { _numCellsY = value; } }

        static private int maxCellsX = 10;
        static private int maxCellsY = 10;

        //2d list
        static private List<List<Zone>> _animalZones = new List<List<Zone>>();
        static public List<List<Zone>> animalZones { get { return _animalZones; } set { _animalZones = value; } }

        static private Zone _holdingPen = new Zone(-1, -1, null);
        static public Zone holdingPen { get { return _holdingPen; } set { _holdingPen = value; } }

        static public void SetUpGame()
        {
            for (var y = 0; y < numCellsY; y++)
            {
                //create a new list to store a row with numCellsX cells
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                //add a row according the current x length
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
        }

        static public void AddZones(Direction d)
        {
            if (d == Direction.down || d == Direction.up)
            {
                if (numCellsY >= maxCellsY) return; // hit maximum height!
                List<Zone> rowList = new List<Zone>();
                //add a row according the current x length
                for (var x = 0; x < numCellsX; x++)
                {
                    //add a cell in the row
                    rowList.Add(new Zone(x, numCellsY, null));
                }
                //add y number
                numCellsY++;
                //add the row
                if (d == Direction.down) animalZones.Add(rowList);
                // if (d == Direction.up) animalZones.Insert(0, rowList);
            }
            else // must be left or right...
            {
                if (numCellsX >= maxCellsX) return; // hit maximum width!
                for (var y = 0; y < numCellsY; y++)
                {
                    //identify the row (each row)
                    var rowList = animalZones[y];
                    // if (d == Direction.left) rowList.Insert(0, new Zone(null));
                    //add a cell into the last of the row
                    if (d == Direction.right) rowList.Add(new Zone(numCellsX, y, null));
                }
                numCellsX++;
            }
        }

        static public void ZoneClick(Zone clickedZone)
        {
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                //change the animal loaction (not holdingPen location)
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddAnimalToHolding(string animalType)
        {
            if (holdingPen.occupant != null) return;
            //why not use Cat as the name, why use fluffy
            //if (animalType == "cat") holdingPen.occupant = new Cat("Fluffy");
            if (animalType == "cat") holdingPen.occupant = new Cat("cat");
            //if (animalType == "mouse") holdingPen.occupant = new Mouse("Squeaky");
            if (animalType == "mouse") holdingPen.occupant = new Mouse("mouse");
            if (animalType == "raptor") holdingPen.occupant = new Raptor("raptor");
            if (animalType == "chick") holdingPen.occupant = new Chick("chick");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            ActivateAnimals();
        }

        //if the actor has less index it will possible to move more than once
        static private void ActivateAnimals()
        {
            //activate animals by the order of reaction times
            for (var r = 1; r < 11; r++) // reaction times from 1 to 10
            {
                //from the first row
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

