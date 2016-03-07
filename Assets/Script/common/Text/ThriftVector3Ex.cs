using UnityEngine;
using System.Collections;

namespace NetWork.Auto
{
	public partial class ThriftVector3
	{
	    public float fX
	    {
	        get{ return _x * 0.01f; }
	        set{ X = (int)(value * 100); }
	    }

	    public float fY
	    {
	        get{ return _y * 0.01f; }
	        set{ Y = (int)(value * 100); }
	    }

	    public float fZ
	    {
	        get{ return _z * 0.01f; }
	        set{ Z = (int)(value * 100); }
	    }

		public Vector3 GetVector3()
		{
			return new Vector3( fX, fY, fZ );
		}
		
		public void SetVector3( Vector3 v )
		{
			fX = v.x;
			fY = v.y;
			fZ = v.z;
		}

		public Quaternion getQuaternaion()
		{
			return new Quaternion( fX ,fY,fZ,1.0f) ;
		}
	}
}