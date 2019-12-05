namespace Yabft.Shared
{
    public static class MathUtils
    {
        public static int Wrap(this int value, int min, int max)
        {
            return ((((value - min) % (max - min)) + (max - min)) % (max - min)) + min;
        }
    }
}
