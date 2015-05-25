namespace matchPuzzle.MVCS.model.level
{
    public class RandomProxy
    {
        readonly System.Random random;

        public RandomProxy(int seed)
        {
            random = new System.Random(seed);
        }

        public int Get(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}