using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Cat : Animal
    {
        public Cat(string name)
        {
            emoji = "🐱";
            species = "cat";
            this.name = name;
            reactionTime = new Random().Next(1, 6); // reaction time 1 (fast) to 5 (medium)
            //reactionTime = 1;
        }

        //override the Activate() in Animal
        public override void Activate()
        {
            //base is Animal
            base.Activate();
            Console.WriteLine("I am a cat. Meow.");
            //hunt can runaway from a raptor, since it also move to the other cell
            Hunt(new List<string>() { "mouse", "chick" }, 1);
            Flee(new List<string>() { "raptor" }, 1);
        }

        /*public void Flee(List<string> targets)
        {
            (Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
            foreach (var direction in targetDirections)
            {
                if (direction == Direction.up) if (Game.Retreat(this, Direction.down)) return;
                if (direction == Direction.down) if (Game.Retreat(this, Direction.up)) return;
                if (direction == Direction.left) if (Game.Retreat(this, Direction.right)) return;
                if (direction == Direction.right) if (Game.Retreat(this, Direction.left)) return;
            }*/

        //checking all the directions, if found cat (ture), then run away from that direction (change the cell)
        /*if (Game.Seek(location.x, location.y, Direction.up, "raptor"))
        {
            if (Game.Retreat(this, Direction.down)) return;
        }
        if (Game.Seek(location.x, location.y, Direction.down, "raptor"))
        {
            if (Game.Retreat(this, Direction.up)) return;
        }
        if (Game.Seek(location.x, location.y, Direction.left, "raptor"))
        {
            if (Game.Retreat(this, Direction.right)) return;
        }
        if (Game.Seek(location.x, location.y, Direction.right, "raptor"))
        {
            if (Game.Retreat(this, Direction.left)) return;
        }*/
        //}

        /* Note that our cat is currently not very clever about its hunting.
           It will always try to attack "up" and will only seek "down" if there
           is no mouse above it. This does not affect the cat's effectiveness
           very much, since the overall logic here is "look around for a mouse and
           attack the first one you see." This logic might be less sound once the
           cat also has a predator to avoid, since the cat may not want to run in
           to a square that sets it up to be attacked!
           请注意，我们的猫目前在捕猎方面不是很聪明。它总是试图攻击 "上面"，
           只有在上面没有老鼠的情况下才会寻找 "下面"。这对猫的效率影响不大，
           因为这里的总体逻辑是 "四处寻找老鼠并攻击你看到的第一只老鼠"。
           一旦猫有了要避开的捕食者，这个逻辑可能就不那么靠谱了，
           因为猫可能不想跑到一个为它设置了被攻击的方格中去！"。
         */

        //if the cat's index is lower it can attack and flee
        /////public override void Hunt(List<string> targets)
        /////{
        //base.Hunt(targets);
        /////(Dictionary<Direction, int> directionInfo, List<Direction> targetDirections) = Game.Seek(location.x, location.y, targets, 1);
        /*foreach (var direction in targetDirections)
        {
            if (direction == Direction.up) Game.Attack(this, Direction.up);
            if (direction == Direction.down) Game.Attack(this, Direction.down);
            if (direction == Direction.left) Game.Attack(this, Direction.left);
            if (direction == Direction.right) Game.Attack(this, Direction.right);
        }*/
        //Console.WriteLine("-----------------"+ new Random().Next(0, 0));
        /////if (targetDirections.Count>0)
        /////{
        /////Game.Attack(this, targetDirections[new Random().Next(0, targetDirections.Count)]);
        /////}
        //Game.Attack(this, targetDirections[0]);

        //this is concept for aovid eating an animal and then be eaten, but the way to seek is now different
        /*if (
            !Game.Seek(location.x, location.y + 1, Direction.up, "raptor") && !Game.Seek(location.x, location.y + 1, Direction.left, "raptor") && !Game.Seek(location.x, location.y + 1, Direction.right, "raptor") &&
            !Game.Seek(location.x, location.y - 1, Direction.down, "raptor") && !Game.Seek(location.x, location.y - 1, Direction.left, "raptor") && !Game.Seek(location.x, location.y - 1, Direction.right, "raptor") &&
            !Game.Seek(location.x + 1, location.y, Direction.up, "raptor") && !Game.Seek(location.x + 1, location.y, Direction.down, "raptor") && !Game.Seek(location.x + 1, location.y, Direction.right, "raptor") &&
            !Game.Seek(location.x - 1, location.y, Direction.up, "raptor") && !Game.Seek(location.x - 1, location.y, Direction.down, "raptor") && !Game.Seek(location.x - 1, location.y, Direction.left, "raptor")
           )
        {*/

        //checking all the directions, if found cat (ture), then attack that direction (reaplce the cell)
        /*if (Game.Seek(location.x, location.y, Direction.up, "mouse") || Game.Seek(location.x, location.y, Direction.up, "chick"))
        {
            Game.Attack(this, Direction.up);
        }
        else if (Game.Seek(location.x, location.y, Direction.down, "mouse") || Game.Seek(location.x, location.y, Direction.down, "chick"))
        {
            Game.Attack(this, Direction.down);
        }
        else if (Game.Seek(location.x, location.y, Direction.left, "mouse") || Game.Seek(location.x, location.y, Direction.left, "chick"))
        {
            Game.Attack(this, Direction.left);
        }
        else if (Game.Seek(location.x, location.y, Direction.right, "mouse") || Game.Seek(location.x, location.y, Direction.right, "chick"))
        {
            Game.Attack(this, Direction.right);
        }*/
        //}
        /////}
    }
}

