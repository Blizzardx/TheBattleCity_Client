using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Utils
{
    /// <summary>
    /// 數學轉換工具
    /// </summary>
    public class MathUtil
    {
        /// <summary>
        /// c#版：四舍五入
        /// </summary>
        /// <param name='value'>
        /// 要进行处理的数据
        /// </param>
        /// <param name='digit'>
        /// 保留的小数位数
        /// </param>
        public static double Round (double value, int digit)
        {
            double vt = Math.Pow (10, digit);
            //1.乘以倍数 + 0.5
            double vx = value * vt + 0.5;
            //2.向下取整
            double temp = Math.Floor (vx);
            //3.再除以倍数
            return (temp / vt);
        }

        /// <summary>
        /// 向上取整。如Ceiling(-123.55)=-123， Ceiling(123.55)=124
        /// </summary>
        public static double Ceiling (double value)
        {
            return Math.Ceiling (value);
        }

        /// <summary>
        /// 向下取整。如 Floor(-123.55)=-124，Floor(123.55)=123
        /// </summary>
        public static double Floor (double value)
        {
            return Math.Floor (value);
        }

        /// <summary>
        /// 直接截取。如 Truncate(-123.55)=-123, Truncate(123.55)=123
        /// </summary>
        public static double Truncate (double value)
        {
            return Math.Truncate (value);
        }
	
        /// <summary>
        /// 随机指定数目的随机数
        /// </summary>
        /// <returns>
        /// 随机好的无重复的数字集合。
        /// </returns>
        /// <param name='length'>
        /// Length.
        /// </param>
        public static List<int> RandomInt (int length)
        {
            List<int> result = new List<int>();
            for (int i = 0; result.Count < length; i++) {
                int index = UnityEngine.Random.Range(0, length);
			
                if(!result.Contains(index)){
                    result.Add(index);
                }
            }
		
            return result;
        }

    }
}
