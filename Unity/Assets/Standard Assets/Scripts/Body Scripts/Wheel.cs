using System;
using UnityEngine;


	public class Wheel : Structure
	{
		double wheelMass;
		double wheelRadius;
		double wheelThickness;
		
		Vector3 wheelCG;
		
	
	
		public Wheel ()
		{
		}
		
		public bool GenerateFromGene(WheelGene gene,Vector3 parentLocation, Structure parent)
		{
			ParseGenes(gene);
			
			wheelCG = wheelCG+parentLocation;
			Debug.Log("putting a wheel at: " + (wheelCG+parentLocation));
			//wheelCG = parentLocation+new Vector3(1,2,3);
			GameObject wheel = Utilities.loadObject("sphere",wheelCG,false);
			wheel.rigidbody.mass = 1; //(float)wheelMass;
			Debug.Log("wheel location" + wheel.transform.position);	
		
			//var wg:WheelGenome;
			//if( genome.WheelGenomes.Length > genome.wheelPositions.Length) {
//				var newWheelPositions = new Vector3[genome.WheelGenomes.Length];
//				for (var k = 0; k<genome.wheelPositions.Length; k++) {
//					newWheelPositions[k] = genome.wheelPositions[k];
//				}
//				for (var j = k; j<newWheelPositions.Length; j++) {
//					newWheelPositions[j] = genome.randomVector3();
//				}
//				genome.wheelPositions = newWheelPositions;
//			//}
//			
//			for(var i=0; i<genome.WheelGenomes.Length; i++) {
//				wg = genome.WheelGenomes[i];
//				GameObject wheel = Utilities.loadObject(wg.wheelType,position + genome.wheelPositions[i]*structuralDistance,false);
//				wheel.transform.Rotate(wg.initialAngle);
//				ConfigurableJoint joint = gaController.j.createJoint(wheel,cartbase,Vector3(0,0,0),Vector3(1,0,0));
//				joint.angularYMotion=ConfigurableJointMotion.Free;
//				joint.targetAngularVelocity=Vector3(0,5,0);
//				joint.angularYZDrive.mode=JointDriveMode.Velocity;
//				thiscart.components[i] = wheel;
//			}
			return true;
		}
		private void ParseGenes(WheelGene gene) {
			wheelMass 		= gene.getChromosome(WheelGene.WheelChromosomeType.mass);
			wheelRadius		= gene.getChromosome(WheelGene.WheelChromosomeType.radius);
			wheelThickness	= gene.getChromosome(WheelGene.WheelChromosomeType.thickness);
			double wheelCGDouble	= gene.getChromosome(WheelGene.WheelChromosomeType.position);
			wheelCG = Utilities.CreateVector3(wheelCGDouble);
		}
	}


