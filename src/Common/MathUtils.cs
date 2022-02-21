namespace Yabft.Common;
public static class MathUtils
{
    public static int Wrap(this int value, int min, int max) => ((((value - min) % (max - min)) + (max - min)) % (max - min)) + min;
}
