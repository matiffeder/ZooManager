using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ZooManager
{
    public static class Game
    {
        static List<List<Zone>> animalZones = Interaction.animalZones;

        /* 
         * seek targets in 4 directions distance
         * call: Interaction - numCellsY, numCellsX
         * called by: Animal, Game
         * parameter: int - location, List<string> - targets, int - distance to seek
         * return: Dictionary<Direction, int> - key is direction value is target's distance, List<Direction> - closest/farest directions with target
         */
        //feature f, i
        static public (Dictionary<Direction, int>, List<Direction>) Seek(int x, int y, List<string> targets, int distance)
        {
            //store 4 directions and its distance to target, default distance is 0 (no target==0)
            //feature f, i
            Dictionary<Direction, int> directionInfo = new Dictionary<Direction, int>() {
                {Direction.up, 0},
                {Direction.down, 0},
                {Direction.left, 0},
                {Direction.right, 0},
            };
            //list to store closest/farest directions with target
            List<Direction> targetDirections = new List<Direction>();
            //int to save the closest/farest directions with target
            int targetDistance = 10;
            //seeking empty cells
            //feature i
            if (targets==null)
            {
                //up
                //if same direction is empty then keep looking next empty in distance
                //feature f (distance)
                for (int i=1; i<=distance; i++)
                {
                    //if up is not edge and up have animal
                    if (y - i >= 0 && animalZones[y - i][x].occupant != null)
                    {
                        //this direction is stop here, so break and seek other directions
                        break;
                    }
                    //if up is not edge and up is empty
                    else if (y - i >= 0 && animalZones[y - i][x].occupant == null)
                    {
                        //add 1 to the distance to the empty of direction up
                        //feature i (found empty, >0)
                        directionInfo[Direction.up]++;
                    }
                }
                //down
                for (int i = 1; i <= distance; i++)
                {
                    if (y + i < Interaction.numCellsY && animalZones[y + i][x].occupant != null)
                    {
                        break;
                    }
                    else if (y + i < Interaction.numCellsY && animalZones[y + i][x].occupant == null)
                    {
                        directionInfo[Direction.down]++;
                    }
                }
                //left
                for (int i = 1; i <= distance; i++)
                {
                    if (x - i >= 0 && animalZones[y][x - i].occupant != null)
                    {
                        break;
                    }
                    else if (x - i >= 0 && animalZones[y][x - i].occupant == null)
                    {
                        directionInfo[Direction.left]++;
                    }
                }
                //right
                for (int i = 1; i <= distance; i++)
                {
                    if (x + i < Interaction.numCellsX && animalZones[y][x + i].occupant != null)
                    {
                        break;
                    }
                    else if (x + i < Interaction.numCellsX && animalZones[y][x + i].occupant == null)
                    {
                        directionInfo[Direction.right]++;
                    }
                }
                //use for finding farest direction in distance
                targetDistance = 0;
                //check saved distance in diff direction to find the farest distance in direction
                foreach (var target in directionInfo)
                {
                    //if the direction have empty cell and is the distance bigger than current targetDistance, save the new bigger value
                    if (target.Value > 0 && target.Value > targetDistance) targetDistance = target.Value;
                }
            }
            //if taget is not empty cell
            else if (targets != null)
            {
                //check from farest to closest to find the closest target
                for (int i = distance; i >= 1; i--)
                {
                    //check different targets
                    for (int j = 0; j < targets.Count; j++)
                    {
                        //if up is not edge and up is one of targets
                        //animalZones[y - i][x].occupant != null is needed to check animalZones[y - i][x].occupant.name
                        if (y - i >= 0 && animalZones[y - i][x].occupant != null && animalZones[y - i][x].occupant.name == targets[j])
                        {
                            //save the current distance to up direction
                            //feature f (return nearest distance)
                            directionInfo[Direction.up] = i;
                        }
                        if (y + i <= Interaction.numCellsY - 1 && animalZones[y + i][x].occupant != null && animalZones[y + i][x].occupant.name == targets[j])
                        {
                            directionInfo[Direction.down] = i;
                        }
                        if (x - i >= 0 && animalZones[y][x - i].occupant != null && animalZones[y][x - i].occupant.name == targets[j])
                        {
                            directionInfo[Direction.left] = i;
                        }
                        if (x + i <= Interaction.numCellsX - 1 && animalZones[y][x + i].occupant != null && animalZones[y][x + i].occupant.name == targets[j])
                        {
                            directionInfo[Direction.right] = i;
                        }
                    }
                }
                //use for finding closest direction in distance
                targetDistance = 10;
                //check saved distance in diff direction to find the closest distance in direction
                foreach (var target in directionInfo)
                {
                    //if the direction have empty cell and is the distance smaller than current targetDistance, save the new smaller value
                    if (target.Value > 0 && target.Value < targetDistance) targetDistance = target.Value;
                }
            }
            //check saved distance in diff direction by saved closest distance to find the closest directions
            foreach (var target in directionInfo)
            {
                //if the direction's distance is closest distance, add to list
                if (target.Value == targetDistance) targetDirections.Add(target.Key);
            }
            //return target's direction and distance, closest/farest directions with target
            return (directionInfo, targetDirections);
        }

        static public void Attack(Animal attacker, Direction d, int distance)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            //get loacation of attacker
            int x = attacker.location.x;
            int y = attacker.location.y;

            //the direction to attack
            switch (d)
            {
                case Direction.up:
                    //attacker take place of the animal that be attacked
                    animalZones[y - distance][x].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + distance][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - distance].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + distance].occupant = attacker;
                    break;
            }
            //the attacker remove from the orig location 
            animalZones[y][x].occupant = null;
        }


        /* 
         * move to a empty cell in distance and will not move back
         * call: Game - Seek
         * called by: Animal
         * parameter: Animal - mover, int - distance to move, Direction? - the direction to move back
         * return: int - remain distance, Direction? - the direction to move back
         */
        //feature g, h, i
        static public (int, Direction?) Move(Animal mover, int distance, Direction? origDirection)
        {
            //get loacation of mover
            int x = mover.location.x;
            int y = mover.location.y;
            //store the distance that moved in this time 
            int movedCell = 0;
            //get the list of farest directions that can move
            //feature g (move to a empty cell), i (target is empty)
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(x, y, null, distance);
            //check the list of directions to avoid move back
            //feature h
            for (int i = 0; i<targetDirections.Count; i++)
            {
                //if have moved (origDirection!=null) and origDirection in the list
                if (origDirection!=null && targetDirections[i]==origDirection)
                {
                    //remove the direction to move back from the list
                    targetDirections.RemoveAt(i);
                }
            }
            if (targetDirections.Count > 0)
            {
                Direction newDir = targetDirections[new Random().Next(0, targetDirections.Count)];
                movedCell = directionInfo[targetDirections[0]];
                Console.WriteLine($"{mover.name} is moving to {newDir.ToString()} {movedCell} cells");

                switch (newDir)
                {
                    case Direction.up:
                        animalZones[y - movedCell][x].occupant = mover;
                        origDirection = Direction.down;
                        break;
                    case Direction.down:
                        animalZones[y + movedCell][x].occupant = mover;
                        origDirection = Direction.up;
                        break;
                    case Direction.left:
                        animalZones[y][x - movedCell].occupant = mover;
                        origDirection = Direction.right;
                        break;
                    case Direction.right:
                        animalZones[y][x + movedCell].occupant = mover;
                        origDirection = Direction.left;
                        break;
                }
                animalZones[y][x].occupant = null;
            }
            //return distance that moved in this time
            //feature g 
            return (movedCell, origDirection);
        }
    }
}

