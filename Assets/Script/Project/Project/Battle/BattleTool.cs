using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class BattleTool
{
    public static int ConvertFloatToInt(float value)
    {
        return (int)(value * 10000);
    }

    public static float ConvertIntToFloat(int value)
    {
        return value * 0.0001f;
    }
}