using System;
using System.Collections.Generic;
using UnityEngine;


	public class WheelGene : BaseGene
	{
		public enum WheelChromosomeType{
		primitive,		// The primitive shape that'll be used to represent the wheel
		mass, 			// mass of the wheel in kg
		radius, 		// wheel radius in meters
		thickness, 		// tirewall thickness in meters
		position, 		// x,y,z coordinates of the wheel relative to the gene parent's position
		spring, 		// suspension spring coefficient
		damping,		// suspension damping coefficient
		camber,			// how far over the wheel leans from vertical
		toe				// how far to one side the wheel is angled from straight forward/backward
		};			
		
		new Dictionary<WheelChromosomeType, double> chromosomes;
		List<IGene> children;
	
		public WheelGene ()
		{
			chromosomes = new Dictionary<WheelChromosomeType, double>();
			children = new List<IGene>();
			System.Random random = new System.Random();
			//generate random chromosome start values
			foreach(WheelChromosomeType type in Enum.GetValues(typeof(WheelChromosomeType)))
			{
				chromosomes.Add(type,random.NextDouble());
			}
		}
	
		public Structure Express(Vector3 location,Structure parent)
		{
			Wheel wheelStructure = new Wheel();
			if(!wheelStructure.GenerateFromGene(this,location,parent)) // if we weren't able to instantiate the wheel
			{
				return null; 											// then we're done.  But if we were...
			}
			foreach(IGene child in children) 							// then go through all the children
			{
				child.Express(location, wheelStructure); 			// and express them.
			}
			return wheelStructure;
			//return null;
		}
	
		public double getChromosome(WheelChromosomeType chromosome)
		{
			double retVal = 0;
			chromosomes.TryGetValue(chromosome,out retVal);
			return retVal;
		}
	
	}


