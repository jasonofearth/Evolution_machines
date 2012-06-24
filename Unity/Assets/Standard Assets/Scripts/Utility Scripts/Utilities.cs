using System;
using UnityEngine;


	public class Utilities : MonoBehaviour
	{
		public Utilities ()
		{
			
		}
		
		public static Vector3 RandomVector3()
		{
			return RandomVector3((float)1.0,(float)1.0,(float)1.0); 
		}
		
		public static Vector3 RandomVector3(float max_x,float max_y, float max_z)
		{
			System.Random rand = new System.Random();
			Vector3 newVect = new Vector3();
			newVect.Set ((float)rand.NextDouble()*max_x,(float)rand.NextDouble()*max_y,(float)rand.NextDouble()*max_z );
			return newVect; 
		}
		
		public static Vector3 CreateVector3(double chromosomeVal)
		{
			return CreateVector3(chromosomeVal, 1,1,1);
		}
		
		public static Vector3 CreateVector3(double chromosomeVal,float max_x,float max_y, float max_z)
		{
			// each chromosome is a double - 8 bytes - 64 bits.  This means it has a max value of ~ 1.84467441 × 10^19
			// We want to use as much of that as possible, in principle.  So each axis of the 3 vector will have a max of about .6*10^6
			// or 600000.  Alternatively we could bit-shift it?  If we wanted it to come out nicely we could just ignore two bytes
			// and use 2 bytes for each of the axes, but that means a max value about 65000, so we're losing an order of 
			// magnitude of possible variation / detail.  Anyway, this should get cleaned up a bit somehow.
			
			Vector3 vect = new Vector3();
			// a double has 8 bytes, or 64 bits.  We're going to bitshift the shit out of this to produce longs(16 bits).
			// one of the nice things is that with 4 longs we could represent a generic quaternion pretty effectively.  
			long maxValue = 0xff;
			
			long x_component = ((long)chromosomeVal >> 48) & 0x000000ff;
			long y_component = ((long)chromosomeVal >> 32) & 0x000000ff;
			long z_component = ((long)chromosomeVal >> 16) & 0x000000ff;
		
			float xval = (float)chromosomeVal * max_x;
			chromosomeVal = (chromosomeVal * 10000) % 500;
			float yval = (float)chromosomeVal * max_y;
			chromosomeVal = (chromosomeVal * 10000) % 500;
			float zval = (float)chromosomeVal * max_z;
			vect.Set (xval,yval,zval);
			return vect;
		}
	
	public static GameObject loadObject(String objType,Vector3 objpos,bool addscript)
{
	//Debug.Log("instantiating " + objType + "At position " + objpos);	
	GameObject obj = (GameObject)Instantiate (Resources.Load(objType),objpos,Quaternion.identity);
	//Debug.Log(obj);
	obj.transform.position = objpos;
	obj.AddComponent("collisionKiller");
	if(addscript) {
		Debug.Log("locating " + objType + " main script");	
		var scriptName = objType+"MainScript";
		Debug.Log("adding "+scriptName);	
		//Debug.Log("adding collision killer script component");	
		//obj.AddComponent("collisionKiller");
		var thisScript = obj.GetComponent(scriptName);	
		Debug.Log("passing in mainscript of ");
		//thisScript.intialize(this);
	}
	//Debug.Log("returning cart base of type:" + obj.GetType());
	return obj;
}
	}


