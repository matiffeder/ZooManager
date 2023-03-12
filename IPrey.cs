namespace ZooManager
{
    interface IPrey
    {

        /* 
         * run away from an animal to a random loacation
         * call: Move - Game, Seek - Game
         * called by: Cat, Raptor, Chick
         * parameter: string[] - targets animal, int - the distance of cells to run
         * return: no (void)
         */
        //feature g (mouse flee), h, i, j (cat flee) 
        void _Flee(string[] targets, int distance);
    }
}
