using System;
using System.Collections.Generic;
using UnityEngine;


	public class CoreGene : BaseGene
	{

		public enum CoreChromosomeType{
		drivepowerMass,
		powerPackAllocation,
		motorAllocation,
		motorSpeedAllocation,
		motorTorqueAllocation,
		structureMass,
		powerPackCG,
		motorCG,
		structureMassCG
		};
		
    	List<IGene> children;
			
		public CoreGene ()
		{
			children = new List<IGene>();
			System.Random random = new System.Random();
			//generate random chromosome start values
			foreach(CoreChromosomeType type in Enum.GetValues(typeof(CoreChromosomeType)))
			{
				chromosomes.Add((int)type,random.NextDouble());
			}
			//add wheel manually just to get started
			int numwheels = 4;
			for(int i = 0; i< numwheels; i++) {
				children.Add(new WheelGene());
			}
			
		}
		
		public CoreGene (float childProbability)
		{
			
		}
		
		public Structure Express(Vector3 location,Structure parent)
		{
			Core creatureStructure = new Core();
			if(!creatureStructure.GenerateFromGene(this,location,null)) // if we weren't able to instantiate the core
			{
				return null; 											// then we're done.  But if we were...
			}
			Debug.Log("Generating children of the core");
			foreach(IGene child in children) 							// then go through all the children
			{
				//Debug.Log(location);
				child.Express(location, creatureStructure); 			// and express them.
			}
			return creatureStructure;
		}
	
	}

