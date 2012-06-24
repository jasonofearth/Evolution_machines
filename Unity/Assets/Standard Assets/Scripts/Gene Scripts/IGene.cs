using System;
using UnityEngine;
using System.Collections.Generic;


	public interface IGene
	{
	
		Structure Express(Vector3 location,Structure parent);
		double getChromosome(int chromosomeType);
	}


