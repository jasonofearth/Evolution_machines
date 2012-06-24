using System;
using UnityEngine;


	public class Core : Structure
	{
		double drivepowerMass;			// How much mass is available for the batteries and the motor.
    
		double powerPackAllocation;   	// Determines how much of the total mass available for driving is available to the power pack     

		double motorAllocation; 			// Determines how much of the total mass available for driving is available to the motor
    	double motorSpeedAllocation;		// Determines how much of the mass available for the motor should be used increasing top motor speed
    	double motorTorqueAllocation;	// Determines how much of the mass available for the motor should be used increasing top motor torque
    
    	double structureMass;			// Increasing structure mass increases the distance components can be placed from the cart structure CG. (the primary origin / datum ) 
    
    	Vector3 powerPackCG;			// position of the power pack
   		Vector3 motorCG;				// position of the motor
    	Vector3 structureMassCG;		// will initially just be (0,1,0), but may have a variable y parameter - how high off the ground the structure nominally sits.
		
		public Core ()
		{
		
		}
	
		public bool GenerateFromGene(CoreGene gene, Vector3 position, Structure parent)
		{
			GameObject motor = null;
			GameObject powerpack = null;
		
			ParseGenes(gene);
		
			// Determine how 'spread out' all the pieces of the cart are.
			//Debug.Log("index:"+index+" name"+name+" position"+position+" genome"+genome);
			//double structuralDistance = Mathf.Sqrt((float)structureMass);
			//Vector3 struturalScale = new Vector3((float)structuralDistance,(float)structuralDistance,(float)structuralDistance); 	
		
			// Determine the masses of the various structural components
			double basemass = gene.getChromosome((int)CoreGene.CoreChromosomeType.structureMass);
			motorAllocation = gene.getChromosome((int)CoreGene.CoreChromosomeType.motorAllocation);
			powerPackAllocation = gene.getChromosome((int)CoreGene.CoreChromosomeType.powerPackAllocation);
			double drivetotal = motorAllocation + powerPackAllocation;
			double motormass = (motorAllocation / drivetotal) * drivepowerMass;
			double powerpackmass = (powerPackAllocation / drivetotal) * drivepowerMass;
			
		
			powerPackCG = Utilities.CreateVector3(gene.getChromosome((int)CoreGene.CoreChromosomeType.powerPackCG),0.1f,0.1f,0.1f);
			motorCG = Utilities.CreateVector3(gene.getChromosome((int)CoreGene.CoreChromosomeType.motorCG),0.1f,0.1f,0.1f);
			
			
			// If our motor or power pack are too light, then the joints will make them go crazy.  Don't let that happen:
			//if ( motormass > (basemass / 10) && motormass < (basemass * 10))
			//{
				motor = Utilities.loadObject("sphere",(position + motorCG) ,false); // What was this doing there '* structuralDistance'
			//} 
			
			//if (powerpackmass > (basemass/10) && powerpackmass < (basemass*10)) 
			//{ 
				powerpack = Utilities.loadObject("sphere",(position + powerPackCG),false); //here too: '* structuralDistance'
			//} 
			
			
			// It's possible at this point that one of our components may be nonexistent.
			if (motor == null || powerpack == null){
				Debug.Log("unable to load some object - motor or powerpack.  Returning an empty cart.");
				return false;
			}
			else
			{
				GameObject cartbase = Utilities.loadObject("cartBase",position,false);
				// Our component masses are fine.  Lets create them.
				cartbase.rigidbody.mass = (float)basemass;
				motor.rigidbody.mass = (float)motormass;
				powerpack.rigidbody.mass = (float)powerpackmass;
				//var thiscart = Cart(genome.WheelGenomes.Length+3);
				
				// connect the joint to the motor.  It will start at the motor, go to the cart.  It has a center at the center of the motor(0,0,0), and a primary axis of (1,0,0) - the x axis.
				motor.transform.parent = cartbase.transform;
				//ConfigurableJoint motorjoint = Joint.createJoint(motor,cartbase,new Vector3(0,0,0),new Vector3(1,0,0));
				// connect the joint to the powerpack.  It will start at the pack, go to the cart.  It has a center at the center of the pack(0,0,0), and a primary axis of (1,0,0) - the x axis.
				powerpack.transform.parent = cartbase.transform;
				
				//ConfigurableJoint packjoint = Joint.createJoint(powerpack,cartbase,new Vector3(0,0,0),new Vector3(1,0,0));
				
				//Debug.Log("build wheels");
				//var wheels = Array(genome.numWheels);
				

				return true;
			}
			
		}
	
		private void ParseGenes(CoreGene gene)
		{
			drivepowerMass = gene.getChromosome((int)CoreGene.CoreChromosomeType.drivepowerMass) * 10;
			structureMass = gene.getChromosome((int)CoreGene.CoreChromosomeType.drivepowerMass) * 10 + 1;
			motorAllocation = gene.getChromosome((int)CoreGene.CoreChromosomeType.motorAllocation);
			
			motorSpeedAllocation = gene.getChromosome((int)CoreGene.CoreChromosomeType.motorSpeedAllocation);
			motorTorqueAllocation = gene.getChromosome((int)CoreGene.CoreChromosomeType.motorTorqueAllocation);
		
			powerPackCG = Utilities.CreateVector3(gene.getChromosome((int)CoreGene.CoreChromosomeType.powerPackCG));
			motorCG = Utilities.CreateVector3(gene.getChromosome((int)CoreGene.CoreChromosomeType.motorCG));
			structureMassCG = Utilities.CreateVector3(gene.getChromosome((int)CoreGene.CoreChromosomeType.structureMassCG));

		}
	}


