using System;
using System.Collections.Generic;
using UnityEngine;

	public class World : MonoBehaviour
	{
		public static int MAX_CREATURES = 50;
		//creature lifespan
		//child gene likelihoods
		//available child gene types
		Dictionary<string,double> geneProbabilities;
		public enum geneTypes {CORE,WHEEL,JOINT,BONE};
		public int numCreatures;
		public World ()
		{
			
			System.Random rand = new System.Random();
			geneProbabilities = new Dictionary<string, double>();
			numCreatures = rand.Next (MAX_CREATURES);
			
			foreach(geneTypes type in Enum.GetValues(typeof(geneTypes)))
			{
				geneProbabilities.Add(type.ToString(),rand.NextDouble());
			}
		}
	}


