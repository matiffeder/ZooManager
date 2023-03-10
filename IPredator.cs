namespace ZooManager
{
    interface IPredator
    {
        //Point location { get; }

        /* 
         * hunt an animal in a random loacation
         * call: Attack - Game
         * called by: Cat, Raptor
         * parameter: string[] - targets animal, int - the distance that it can hunt
         * return: no (void)
         */
        void Hunt(string[] targets, int distance);
    }
}
