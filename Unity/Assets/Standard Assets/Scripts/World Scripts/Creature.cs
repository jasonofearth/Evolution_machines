using System;
using UnityEngine;


	public class Creature
	{
		private GenomeTree genome;
		private long lifespan;
		private int healthEaten;
		private Vector3 startPosition;
		private Structure creatureStructure;
		
		public Creature ()
		{
			genome = new GenomeTree();
			startPosition = new Vector3();
		
			creatureStructure = genome.Generate(startPosition);
		}
		
		public Creature(Vector3 startPos)
		{
			startPosition = startPos;
			genome = new GenomeTree();
			creatureStructure = genome.Generate(startPosition);
			
		}
	}


