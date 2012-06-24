using System;
using UnityEngine;
using System.Collections.Generic;

public class BaseGene : IGene
{
	protected Dictionary<int,double> chromosomes;
	public BaseGene ()
	{
		chromosomes = new Dictionary<int, double>();
	}
	
	public double getChromosome(int chromosome)
	{
		double retVal = 0;
		chromosomes.TryGetValue(chromosome,out retVal);
		return retVal;
	}
	
	public Structure Express(Vector3 location,Structure parent)
	{
		return null;
	}
}


