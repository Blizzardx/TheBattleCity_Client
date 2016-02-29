using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Core.Utils
{
    public static class SysUtils
    {
		public static bool IsEqual(float f1,float f2)
		{
			return System.Math.Abs(f1 - f2) < 0.00001f;
		}
		
		public static bool IsEqual(Vector3 v1,Vector3 v2)
		{
			return IsEqual(v1.x, v2.x) && IsEqual(v1.y, v2.y) && IsEqual(v1.z, v2.z);
		}

       
    }
}



