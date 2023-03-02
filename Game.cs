using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

//if holding have an animal and user pick up the other animal on the zone the holding animal will disappear

namespace ZooManager
{
    public static class Game
    {
        //static int numCellsX = Interaction.numCellsX;
        //static int numCellsY = Interaction.numCellsY;

        static List<List<Zone>> animalZones = Interaction.animalZones;

        /*static public bool Seek(int x, int y, Direction d, string target)
        {
            switch (d)
            {
                case Direction.up:
                    y--;
                    //break the function and do not run below until a "}"
                    break;
                case Direction.down:
                    y++;
                    break;
                case Direction.left:
                    x--;
                    break;
                case Direction.right:
                    x++;
                    break;
            }
            //-1 is holdingPen
            //if the direction out of range of the zone return false
            if (y < 0 || x < 0 || y > Interaction.numCellsY - 1 || x > Interaction.numCellsX - 1) return false;
            //if no animal in the cell of the direction return false
            if (animalZones[y][x].occupant == null) return false;
            //if found prey or predator or other target in the direction
            if (animalZones[y][x].occupant.name == target)
            {
                return true;
            }
            return false;
        }*/
        static public (Dictionary<Direction, int>, List<Direction>) Seek(int x, int y, List<string> targets, int distance)
        {
            Dictionary<Direction, int> directionInfo = new Dictionary<Direction, int>() {
                {Direction.up, 0},
                {Direction.down, 0},
                {Direction.left, 0},
                {Direction.right, 0},
            };
            List<Direction> targetDirections = new List<Direction>();
            int targetDistance = 10;
            if (targets==null)
            {
                for (int i=1; i<=distance; i++)
                {
                    if (y - i >= 0 && animalZones[y - i][x].occupant != null)
                    {
                        break;
                    }
                    else if (y - i >= 0 && animalZones[y - i][x].occupant == null)
                    {
                        directionInfo[Direction.up]++;
                    }
                }
                for (int i = 1; i <= distance; i++)
                {
                    if (y + i <= Interaction.numCellsY - 1 && animalZones[y + i][x].occupant != null)
                    {
                        break;
                    }
                    else if (y + i <= Interaction.numCellsY - 1 && animalZones[y + i][x].occupant == null)
                    {
                        directionInfo[Direction.down]++;
                    }
                }
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
                for (int i = 1; i <= distance; i++)
                {
                    if (x + i <= Interaction.numCellsX - 1 && animalZones[y][x + i].occupant != null)
                    {
                        break;
                    }
                    else if (x + i <= Interaction.numCellsX - 1 && animalZones[y][x + i].occupant == null)
                    {
                        directionInfo[Direction.right]++;
                    }
                }
                targetDistance = 0;
                foreach (var target in directionInfo)
                {
                    if (target.Value > 0 && target.Value > targetDistance) targetDistance = target.Value;
                }
            }
            else if (targets != null)
            {
                for (int i = distance; i >= 1; i--)
                {
                    for (int j = 0; j < targets.Count; j++)
                    {
                        if (y - i >= 0 && animalZones[y - i][x].occupant != null && animalZones[y - i][x].occupant.name == targets[j])
                        {
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
                targetDistance = 10;
                foreach (var target in directionInfo)
                {
                    if (target.Value > 0 && target.Value < targetDistance) targetDistance = target.Value;
                }
            }
            foreach (var target in directionInfo)
            {
                if (target.Value == targetDistance) targetDirections.Add(target.Key);
            }
            //I return directionInfo because the guide said we need return an int that reflects the number of squares to the nearest target
            return (directionInfo, targetDirections);
        }

        /* This method currently assumes that the attacker has determined there is prey
           in the target direction. In addition to bug-proofing our program, can you think
           of creative ways that NOT just assuming the attack is on the correct target (or
           successful for that matter) could be used?
           这种方法目前假定攻击者已经确定在目标方向有猎物。除了对我们的程序进行防错之外，
           你能想出一些创造性的方法，不只是假设攻击是在正确的目标上（或者说是成功的），还可以使用吗？
         */

        static public void Attack(Animal attacker, Direction d, int distance)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

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
            animalZones[y][x].occupant = null;
        }

        /* We can't make the same assumptions with this method that we do with Attack, since
           the animal here runs AWAY from where they spotted their target (using the Seek method
           to find a predator in this case). So, we need to figure out if the direction that the
           retreating animal wants to move is valid. Is movement in that direction still on the board?
           Is it just going to send them into another animal? With our cat & mouse setup, one is the
           predator and the other is prey, but what happens when we have an animal who is both? The animal
           would want to run away from their predators but towards their prey, right? Perhaps we can generalize
           this code (and the Attack and Seek code) to help our animals strategize more...
           我们不能用这种方法做与攻击相同的假设，因为这里的动物是向远离它们发现目标的地方跑去的
           （在这种情况下用寻找的方法来寻找捕食者）。因此，我们需要弄清楚，撤退的动物想要移动的方向是否有效。
           朝这个方向移动是否还在棋盘上？是不是会把它们送到另一个动物身上？在我们的猫和老鼠的设置中，
           一个是捕食者，另一个是猎物，但是当我们有一个同时是捕食者和猎物的动物时会发生什么？
           动物会想逃离它们的捕食者，而向它们的猎物跑去，对吗？
           也许我们可以把这个代码（以及攻击和寻找代码）加以概括，以帮助我们的动物制定更多的战略......
         */

        static public bool Retreat(Animal runner, Direction d, int distance)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    /* The logic below uses the "short circuit" property of Boolean &&.
                       If we were to check our list using an out-of-range index, we would
                       get an error, but since we first check if the direction that we're modifying is
                       within the ranges of our lists, if that check is false, then the second half of
                       the && is not evaluated, thus saving us from any exceptions being thrown.
                       下面的逻辑使用了 Boolean && 的 "short circuit" 属性。如果我们使用一个超出范围的索引来检查我们的列表，
                       我们会得到一个错误，但是由于我们首先检查我们要修改的方向是否在我们列表的范围内，
                       如果这个检查是假的，那么&&的后半部分就不会被评估，从而使我们免于任何异常的抛出
                     */
                    //if no animal in the cell of the direction (up)
                    //if y == 0 there is no cell to run
                    if (y > 0 && animalZones[y - distance][x].occupant == null)
                    {
                        //the runner change the place to the direction (up)
                        animalZones[y - distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                /* Note that in these four cases, in our conditional logic we check
                   for the animal having one square between itself and the edge that it is
                   trying to run to. For example,in the above case, we check that y is greater
                   than 0, even though 0 is a valid spot on the list. This is because when moving
                   up, the animal would need to go from row 1 to row 0. Attempting to go from row 0
                   to row -1 would cause a runtime error. This is a slightly different way of testing
                   if 
                   请注意，在这四种情况下，在我们的条件逻辑中，我们检查动物在自己和它试图跑到的边缘之间是否有一个方格。
                   例如，在上述情况下，我们检查y是否大于0，尽管0是列表上的一个有效位置。这是因为当向上移动时，
                   动物需要从第1行到第0行。试图从第0行到第-1行会导致运行时错误。这是一种稍微不同的测试方式，如果
                 */
                case Direction.down:
                    //the bottom row is Interaction.numCellsY-1, so y should be smaller than Interaction.numCellsY-1 to have a place to run
                    if (y < Interaction.numCellsY - 1 && animalZones[y + distance][x].occupant == null)
                    {
                        animalZones[y + distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - distance].occupant == null)
                    {
                        animalZones[y][x - distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
                case Direction.right:
                    if (x < Interaction.numCellsX - 1 && animalZones[y][x + distance].occupant == null)
                    {
                        animalZones[y][x + distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true;
                    }
                    return false;
            }
            return false; // fallback
        }
        static public (int, Direction?) Move(Animal mover, int distance, Direction? origDirection)
        {
            int x = mover.location.x;
            int y = mover.location.y;
            int movedCell = 0;
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(x, y, null, distance);
            for (int i = 0; i<targetDirections.Count; i++)
            {
                if (origDirection!=null && targetDirections[i]==origDirection)
                {
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
            return (movedCell, origDirection);
        }
    }
}

