using System;
using System.Collections.Generic;

namespace ZooManager
{
    public class Mouse : Animal
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
               so mice are more likely to react to their surroundings faster than cats!
             */
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            Flee(new List<string>() { "raptor", "cat" }, 2);
        }

        /* Note that our mouse is (so far) a teeny bit more strategic than our cat.
           The mouse looks for cats and tries to run in the opposite direction to
           an empty spot, but if it finds that it can't go that way, it looks around
           some more. However, the mouse currently still has a major weakness! He
           will ONLY run in the OPPOSITE direction from a cat! The mouse won't (yet)
           consider running to the side to escape! However, we have laid out a better
           foundation here for intelligence, since we actually check whether our escape
           was succcesful -- unlike our cats, who just assume they'll get their prey!
           请注意，我们的老鼠（到目前为止）比我们的猫多了一丁点策略。
           老鼠寻找猫，并试图向相反的方向跑到一个空的地方，但如果它发现它不能走那条路，
           它就会再四处寻找。然而，这只老鼠目前仍有一个重大的弱点！它只会向与猫相反的方向跑！
           老鼠（还）不会考虑跑到旁边去逃跑！然而，我们在这里为智力打下了更好的基础，
           因为我们实际上是在检查我们的逃跑是否成功 -- 不像我们的猫，他们只是假设他们会得到他们的猎物!
         */
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
            /*if (Game.Seek(location.x, location.y, Direction.up, "cat"))
            {
                if (Game.Retreat(this, Direction.down)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.down, "cat"))
            {
                if (Game.Retreat(this, Direction.up)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.left, "cat"))
            {
                if (Game.Retreat(this, Direction.right)) return;
            }
            if (Game.Seek(location.x, location.y, Direction.right, "cat"))
            {
                if (Game.Retreat(this, Direction.left)) return;
                //if left is not no cell it should go up or down
            }*/
        //}
    }
}

