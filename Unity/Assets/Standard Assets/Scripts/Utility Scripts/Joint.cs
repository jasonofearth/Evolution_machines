using System;
using UnityEngine;
using System.Text.RegularExpressions;

	public class Joint
	{
		private static String[] myParams= new String[]{"xmove","ymove","zmove","xangmove","yangmove","zangmove",
			"xdrivemode","ydrivemode","zdrivemode","xdrivespring","ydrivespring","zdrivespring",
			"xdrivedamper","ydrivedamper","zdrivedamper",
			"xdrivemaxforce","ydrivemaxforce","zdrivemaxforce",
			"xangdrivemode","yorzangdrivemode","slerpdrivemode",
			"xangdrivespring","yorzangdrivespring","slerpdrivespring",
			"xangdrivedamper","yorzangdrivedamper","slerpdrivedamper",
			"xangdrivemaxforce","yorzangdrivemaxforce","slerpdrivemaxforce",
			"linearlimitlimit","linearlimitspring","linearlimitdamper","linearlimitbouncyness",
			"lowangularlimitlimit","lowangularlimitspring","lowangularlimitdamper","lowangularlimitbouncyness",
			"highangularlimitlimit","highangularlimitspring","highangularlimitdamper","highangularlimitbouncyness",
			"angularylimitlimit","angularylimitspring","angularylimitdamper","angularylimitbouncyness",
			"angularzlimitlimit","angularzlimitspring","angularzlimitdamper","angularzlimitbouncyness",
			"targetposition","targetvelocity","targetrotation","targetangularvelocity",
			"rotationdrivemode","anchor",
			"breakforce","breaktorque","motionaxis"};

//Debug.Log(myParams.length);

		private static String [] unityParams=new String[]{"thejoint.xMotion","thejoint.yMotion","thejoint.zMotion",
			"thejoint.angularXMotion","thejoint.angularYMotion","thejoint.angularZMotion",
			"thejoint.xDrive.mode","thejoint.yDrive.mode","thejoint.zDrive.mode",
			"thejoint.xDrive.positionSpring","thejoint.yDrive.positionSpring","thejoint.zDrive.positionSpring",
			"thejoint.xDrive.positionDamper","thejoint.yDrive.positionDamper","thejoint.zDrive.positionDamper",
			"thejoint.xDrive.maximumForce","thejoint.yDrive.maximumForce","thejoint.zDrive.maximumForce",
			"thejoint.angularXDrive.mode","thejoint.angularYZDrive.mode","thejoint.slerpDrive.mode",
			"thejoint.angularXDrive.positionSpring","thejoint.angularYZDrive.positionSpring","thejoint.slerpDrive.positionSpring",
			"thejoint.angularXDrive.positionDamper","thejoint.angularYZDrive.positionDamper","thejoint.slerpDrive.positionDamper",
			"thejoint.angularXDrive.maximumForce","thejoint.angularYZDrive.maximumForce","thejoint.slerpDrive.maximumForce",
			"thejoint.linearLimit.limit","thejoint.linearLimit.spring","thejoint.linearLimit.damper","thejoint.linearLimit.bouncyness",
			"thejoint.lowAngularXLimit.limit","thejoint.lowAngularXLimit.spring","thejoint.lowAngularXLimit.damper","thejoint.lowAngularXLimit.bouncyness",
			"thejoint.highAngularXLimit.limit","thejoint.highAngularXLimit.spring","thejoint.highAngularXLimit.damper","thejoint.highAngularXLimit.bouncyness",
			"thejoint.angularYLimit.limit","thejoint.angularYLimit.spring","thejoint.angularYLimit.damper","thejoint.angularYLimit.bouncyness",
			"thejoint.angularZLimit.limit","thejoint.angularZLimit.spring","thejoint.angularZLimit.damper","thejoint.angularZLimit.bouncyness",
			"thejoint.targetPosition","thejoint.targetVelocity","thejoint.targetRotation","thejoint.targetAngularVelocity","thejoint.rotationDriveMode",
			"thejoint.anchor","thejoint.breakForce","thejoint.breakTorque","thejoint.axis"};

		private static String[] myValues= new String[]{"lock","limited","free",
			"none","position","velocity","positionandvelocity",
			"xyz","slerp","perp"};

		private static String [] unityValues=new String[]{"ConfigurableJointMotion.Locked","ConfigurableJointMotion.Limited","ConfigurableJointMotion.Free",
			"JointDriveMode.None","JointDriveMode.Position","JointDriveMode.Velocity","JointDriveMode.PositionAndVelocity",
			"RotationDriveMode.XYAndZ","RotationDriveMode.Slerp","theJoint.axis"};
		public Joint ()
		{
		}
		
		public static ConfigurableJoint createJoint(GameObject startobject,GameObject endobject)
		{
			var axis=(startobject.transform.position-endobject.transform.position)/2;
			var anchor=axis;
			return createJoint(startobject,endobject,anchor,axis);
		}

		public static ConfigurableJoint createJoint(GameObject startobject,GameObject endobject,Vector3 jointAxis)
		{
			var anchor=jointAxis;
			return createJoint(startobject,endobject,anchor,jointAxis);
		}

		public static ConfigurableJoint createJoint(GameObject startobject,GameObject endobject,Vector3 jointAnchor, Vector3 jointAxis)
		{
			ConfigurableJoint thejoint = (ConfigurableJoint)startobject.AddComponent("ConfigurableJoint");
			thejoint.axis=jointAxis;
			//Debug.Log(thejoint.axis);
			thejoint.anchor=jointAnchor;
			lockAxes(startobject);
			//endobject.transform.position;
			//thejoint.angularZMotion=ConfigurableJointMotion.Free;
			thejoint.connectedBody=endobject.rigidbody;//has to be done last
			return thejoint;
		}

		public static void lockAxes(GameObject startobject)
		{
			//Debug.Log("locking joints");
			ConfigurableJoint thejoint  = (ConfigurableJoint) startobject.GetComponent("ConfigurableJoint");
			//Debug.Log("locking joint"+thejoint);
			thejoint.xMotion=ConfigurableJointMotion.Locked;
			thejoint.yMotion=ConfigurableJointMotion.Locked;
			thejoint.zMotion=ConfigurableJointMotion.Locked;
			thejoint.angularXMotion=ConfigurableJointMotion.Locked;
			thejoint.angularYMotion=ConfigurableJointMotion.Locked;
			thejoint.angularZMotion=ConfigurableJointMotion.Locked; 
			Rigidbody dude;
			dude=thejoint.connectedBody;
			thejoint.connectedBody=dude;//has to be done last	
		}   


//example joint1 breakforce=10;breakitdown=breakmine;etc)
		public static void changeJoint(ConfigurableJoint thejoint,String jointchanges)
		{
			//Debug.Log("changing joints");
			//Debug.Log(jointchanges);
			var l=jointchanges.Length;
			if (jointchanges[l-1]==";"[0]){
				//Debug.Log("there is a semicolon at the end");
				jointchanges=jointchanges.Substring(0,l-1);
				//Debug.Log(jointchanges);
				//Debug.Log("is semicolon removed?");
			}
			String []jointchangesA=Regex.Split(jointchanges,";");
			for  (var i=0;i<jointchangesA.Length;i++){
				String [] jointparamA=Regex.Split(jointchangesA[i],"=");
				var ptype=jointparamA[0];
				System.Object pvalue=jointparamA[1];
				Debug.Log("p "+ptype+" "+pvalue);
				
				//Debug.Log(pvalue+"after");
				var utype=myParamToUnityParam(ptype);
				if (ptype=="targetrotation"){
					pvalue=toQuaternion((Vector3)pvalue);
				}
				//Debug.Log("pvalue="+pvalue);
				var uvalue=myValueToUnityValue((String)pvalue);
				//Debug.Log("u "+utype+" "+uvalue);
				
				if(utype != "" && uvalue != ""){
					//Debug.Log(utype + "=" + uvalue);	
		
					// TODO: REPLACE THIS EVAL			eval(utype + "=" + uvalue);		
					var cb=thejoint.connectedBody;
					thejoint.connectedBody=cb;	
				}
				else{
					Debug.LogWarning("changeJoint reporting param or value error");
				}
			}
		}


		public static Quaternion toQuaternion(Vector3 vec)
		{
			Debug.Log("vec="+vec);
//			if (Regex.IsMatch(vec,"vec"))
//			{
//			var re="vec";
//			vec=Regex.Replace(vec,re,"Vector3");
//			}
			
			// TODO: REPLACE THIS EVAL vece=eval(vec);
			//Debug.Log(vece);
			var Qt = Quaternion.Euler(vec);
			//Debug.Log(Qt);
			return Qt;//"Quaternion"+Qt.ToString();
		}


		public static string myParamToUnityParam(string theparam){
			Debug.Log(theparam+" converting param to unity param");
			var wordnum=-1;
			for(var i=0;i<myParams.Length;i++){
				//Debug.Log(theparam + "," + myParams[i]);
				if (theparam==myParams[i]){
					wordnum=i;
					break;
				}
			}
			//Debug.Log(wordnum + "wordnum");
			if(wordnum!=-1){
				return unityParams[wordnum];
			}
			else{
				Debug.LogWarning("warning: myParamToUnityParam cannot find parameter" + theparam);
				return "";
			}
		}
	
public static string myValueToUnityValue(string thevalue){
	//Debug.Log(thevalue+" converting value to unityvalue");
	var wordnum=-1;
	for(var i=0;i<myValues.Length;i++){
		if (thevalue==myValues[i]){
			wordnum=i;
			break;
		}
	}
	if(wordnum!=-1){
		return unityValues[wordnum];
	}
	else{
		
		if (Regex.IsMatch(thevalue,"vec")){
			var re="vec";
			var myvalue=Regex.Replace(thevalue,re,"Vector3");
			return myvalue;
		}	
		//Debug.Log("error cannot find it");
		return thevalue;	
	}
}
	}


