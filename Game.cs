using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;

namespace ZooManager
{
    public static class Game
    {
        static List<List<Zone>> animalZones = Interaction.animalZones;

        /* 
         * function to reduce the repeating codes
         * call: none
         * called by: Game
         * parameter: int - location, int - location, int - distance of the direction
         * return: Dictionary<Direction, int> - key is direction, value is coordinate (x, y)
         */
        static Dictionary<Direction, int[]> DirCoordinates(int x, int y, int distance)
        {
            //create the dictionary for the new coordinates of 4 the directions
            Dictionary<Direction, int[]> dirCoordinates = new Dictionary<Direction, int[]>()
            {
                //key: directions, value: coordinates after add distance in the direction
                { Direction.up, new int[] {x, y-distance} },
                { Direction.right, new int[] {x+distance, y} },
                { Direction.down, new int[] {x, y+distance} },
                { Direction.left, new int[] {x-distance, y} },
            };
            //returb the dictionary
            return dirCoordinates;
        }

    /* 
     * seek targets in 4 directions distance
     * call: Interaction - numCellsY, numCellsX
     * called by: Animal, Game
     * parameter: int - location, List<string> - targets, int - distance to seek, bool - true is not fly
     * return: Dictionary<Direction, int> - key is direction, value is target's distance, List<Direction> - closest/farest directions with target
     */
    //feature f, i
    static public (Dictionary<Direction, int>, List<Direction>) Seek(int x, int y, List<string> targets, int distance, bool nofly=true)
        {
            //store 4 directions and its distance to target, default distance is 0 (no target==0)
            //feature f, i
            Dictionary<Direction, int> directionInfo = new Dictionary<Direction, int>()
            {
                {Direction.up, 0},
                {Direction.right, 0},
                {Direction.down, 0},
                {Direction.left, 0},
            };
            //list to store closest/farest directions with target
            List<Direction> targetDirections = new List<Direction>();
            //int to save the closest/farest directions with target
            int targetDistance = 10;
            //seeking empty cells
            //feature i
            if (targets==null)
            {
                //check the farest empty cell, so the distance should be higher in each loop
                //it is needed this because the direction in distance might have no occupant
                for (int i = 1; i <= distance; i++)
                {
                    //get the coordinates in current distance from the current location
                    //key is direction, value is axis
                    Dictionary<Direction, int[]> dirCoordinates = DirCoordinates(x, y, i);
                    //save the farest empty coordinates of all the direction
                    foreach (var coordinate in dirCoordinates)
                    {
                        //if the direction is not edge
                        if (coordinate.Value[1] >= 0 && coordinate.Value[0] < Interaction.numCellsX && coordinate.Value[1] < Interaction.numCellsY && coordinate.Value[0] >= 0)
                        {
                            //the cell is empty
                            if (animalZones[coordinate.Value[1]][coordinate.Value[0]].occupant == null)
                            {
                                //save 'the distance to the empty' of the direction
                                //feature i (found empty, >0)
                                directionInfo[coordinate.Key] = i;
                            }
                        }
                    }
                }
                //if not flying
                if (nofly)
                {
                    //check the closest cell that is occupied, so the distance should be lower in each loop
                    //this will cover the previous data if matched the conditions
                    for (int i = distance; i > 0; i--)
                    {
                        //get the coordinates in current distance from the current location
                        //key is direction, value is axis
                        Dictionary<Direction, int[]> dirCoordinates = DirCoordinates(x, y, i);
                        //save the closest empty coordinates of all the direction
                        foreach (var coordinate in dirCoordinates)
                        {
                            //if the direction is not edge
                            if (coordinate.Value[1] >= 0 && coordinate.Value[0] < Interaction.numCellsX && coordinate.Value[1] < Interaction.numCellsY && coordinate.Value[0] >= 0)
                            {
                                //check if something on the cells in the direction by lower distance
                                if (animalZones[coordinate.Value[1]][coordinate.Value[0]].occupant != null)
                                {
                                    //the closer occupant is at current distance, the empty cell is at previous cell
                                    //so the distance-1 will be the closest empty cell with no occupant in between
                                    directionInfo[coordinate.Key] = i - 1;
                                }
                            }
                        }
                    }
                }
                //the function below DirCoordinates can't apply DirCoordinates, because break will break the wrong loop
                //up
                //if same direction is empty then keep looking next empty in distance
                //feature f (distance)
                /*for (int i=1; i<=distance; i++)
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
                }*/
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
                    //get the coordinates in current distance from the current location
                    //key is direction, value is axis
                    Dictionary<Direction, int[]> dirCoordinates = DirCoordinates(x, y, i);
                    //check different targets
                    for (int j = 0; j < targets.Count; j++)
                    {
                        //check all the directions in dirCoordinates
                        foreach (var coordinate in dirCoordinates)
                        {
                            //declare values to make the codes readable
                            int newY = coordinate.Value[1];
                            int newX = coordinate.Value[0];
                            Direction direction = coordinate.Key;
                            //if the direction is not edge and the direction is one of targets
                            //animalZones[newY][newX].occupant != null is needed to check animalZones[newY][newX].occupant.name
                            if (newY >= 0 && newX < Interaction.numCellsX && newY < Interaction.numCellsY && newX >= 0
                                && animalZones[newY][newX].occupant != null && animalZones[newY][newX].occupant.name == targets[j])
                            {
                                //save the current distance to the direction
                                //feature f (return nearest distance)
                                directionInfo[direction] = i;
                            }
                        }
                        //if up is not edge and up is one of targets
                        //animalZones[y - i][x].occupant != null is needed to check animalZones[y - i][x].occupant.name
                        /*if (y - i >= 0 && animalZones[y - i][x].occupant != null && animalZones[y - i][x].occupant.name == targets[j])
                        {
                            //save the current distance to up direction
                            //feature f (return nearest distance)
                            directionInfo[Direction.up] = i;
                        }
                        if (x + i <= Interaction.numCellsX - 1 && animalZones[y][x + i].occupant != null && animalZones[y][x + i].occupant.name == targets[j])
                        {
                            directionInfo[Direction.right] = i;
                        }
                        if (y + i <= Interaction.numCellsY - 1 && animalZones[y + i][x].occupant != null && animalZones[y + i][x].occupant.name == targets[j])
                        {
                            directionInfo[Direction.down] = i;
                        }
                        if (x - i >= 0 && animalZones[y][x - i].occupant != null && animalZones[y][x - i].occupant.name == targets[j])
                        {
                            directionInfo[Direction.left] = i;
                        }*/
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
            //check saved distance in diff direction by saved closest/farest distance to find the closest/farest directions
            foreach (var target in directionInfo)
            {
                //if the direction's distance is closest/farest distance, add to list
                if (target.Value == targetDistance) targetDirections.Add(target.Key);
            }
            //return target's direction and distance, closest/farest directions with target
            return (directionInfo, targetDirections);
        }

        /* 
         * attack animal and take their place
         * call: no
         * called by: Animal
         * parameter: Animal - attacker, Direction - location, int - distance to attack
         * return: no (void)
         */
        static public void Attack(Animal attacker, Direction d, int distance)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            //get the coordinates in distance from the current location
            //key is direction, value is axis
            Dictionary<Direction, int[]> dirCoordinates = DirCoordinates(attacker.location.x, attacker.location.y, distance);
            //declare values to make the codes readable
            int newY = dirCoordinates[d][1];
            int newX = dirCoordinates[d][0];
            //make it moved to avoid move more than once
            //feature o 
            animalZones[attacker.location.y][attacker.location.x].occupant.moved = true;
            //the attacker remove from the orig location, should remove first
            animalZones[attacker.location.y][attacker.location.x].occupant = null;
            //attacker take place of the animal that be attacked
            animalZones[newY][newX].occupant = attacker;

            //get loacation of attacker
            //int x = attacker.location.x;
            //int y = attacker.location.y;
            //the direction to attack
            /*switch (d)
            {
                case Direction.up:
                    //attacker take place of the animal that be attacked
                    animalZones[y - distance][x].occupant = attacker;
                    break;
                case Direction.right:
                    animalZones[y][x + distance].occupant = attacker;
                    break;
                case Direction.down:
                    animalZones[y + distance][x].occupant = attacker;
                    break;
                case Direction.left:
                    animalZones[y][x - distance].occupant = attacker;
                    break;
            }
            //the attacker remove from the orig location 
            animalZones[y][x].occupant = null;*/
        }


        /* 
         * move to a empty cell in distance and will not move back
         * call: Game - Seek
         * called by: Animal
         * parameter: Animal - mover, int - distance to move, Direction? - the direction to move back
         * return: int - remain distance, Direction? - the direction to move back
         */
        //feature g, h, i
        static public (int, Direction?) Move(Animal mover, int distance, Direction? origDirection, List<Direction>? predatorDirections, bool nofly=true)
        {
            //get loacation of mover
            int x = mover.location.x;
            int y = mover.location.y;
            //store the distance that moved in this time 
            int movedCell = 0;
            //get the list of farest directions that can move
            //feature g (move to a empty cell), i (target is empty)
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(x, y, null, distance, nofly);
            //avoid the direction that have predator occupied, so that the animanl will not go to the directions and be eaten
            if (predatorDirections != null)
            {
                //check the list in predatorDirections
                foreach (Direction direction in predatorDirections)
                {
                    //remove the direction from list in targetDirections
                    targetDirections.Remove(direction);
                }
            }
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
            //if have a cell to move
            if (targetDirections.Count > 0)
            {
                //choose a direction in the list to move radomly
                Direction newDir = targetDirections[new Random().Next(0, targetDirections.Count)];
                //save the cell that moved according to the distance that return from Seek method
                //  all the distance in targetDirections are the same
                movedCell = directionInfo[targetDirections[0]];
                Console.WriteLine($"{mover.name} is moving to {newDir.ToString()} {movedCell} cells");
                //get the coordinates in distance from the current location
                //key is direction, value is axis
                Dictionary<Direction, int[]> dirCoordinates = DirCoordinates(mover.location.x, mover.location.y, movedCell);
                //declare values to make the codes readable
                int newY = dirCoordinates[newDir][1];
                int newX = dirCoordinates[newDir][0];
                //make it moved to avoid move more than once
                //feature o 
                animalZones[mover.location.y][mover.location.x].occupant.moved = true;
                //mover remove from the orig cell, should remove first
                animalZones[mover.location.y][mover.location.x].occupant = null;
                //move to the new cell
                animalZones[newY][newX].occupant = mover;
                //https://stackoverflow.com/questions/4538894/get-index-of-a-key-value-pair-in-a-c-sharp-dictionary-based-on-the-value
                //get the index of newDir in dirCoordinates
                int dirIndex = dirCoordinates.Keys.ToList().IndexOf(newDir);
                //save the move back direction
                //the DirCoordinates dict is organized by top, right, down, left, so the opposite direction is in the next two or previous two index
                origDirection = dirCoordinates.ElementAt(dirIndex+2 > 3 ? dirIndex-2 : dirIndex+2).Key;

                /*switch (newDir)
                {
                    //if the radom direction is up
                    case Direction.up:
                        //animal move to the new cell
                        animalZones[y - movedCell][x].occupant = mover;
                        //save the move back direction
                        origDirection = Direction.down;
                        break;
                    case Direction.right:
                        animalZones[y][x + movedCell].occupant = mover;
                        origDirection = Direction.left;
                        break;
                    case Direction.down:
                        animalZones[y + movedCell][x].occupant = mover;
                        origDirection = Direction.up;
                        break;
                    case Direction.left:
                        animalZones[y][x - movedCell].occupant = mover;
                        origDirection = Direction.right;
                        break;
                }
                //remove from the orig cell
                animalZones[y][x].occupant = null;*/
            }
            //return distance that moved in this time
            //feature g 
            return (movedCell, origDirection);
        }
    }
}

