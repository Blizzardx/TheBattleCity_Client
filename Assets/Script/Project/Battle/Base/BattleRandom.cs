using System;

public class BattleRandom
{
    private static int index = 0;
    public static int GetRandomValue()
    {
        Random r = new Random(BattleManager.GetClientFrame());
        return r.Next();
    }
    public static int GetRandomValue(int min,int max)
    {
        Random r = new Random(BattleManager.GetClientFrame());
        return r.Next(min,max);
    }
    public static int GetRandomValueUnsafe()
    {
        Random r = new Random(++index);
        return r.Next();
    }
    public static int GetRandomValueUnsafe(int min, int max)
    {
        Random r = new Random(++index);
        return r.Next(min, max);
    }
}