using System;
using System.Collections.Generic;
using UnityEngine;


	public class GenomeTree
	{
		CoreGene core;
		public GenomeTree ()
		{
			createGenome();
		}
		
		private void createGenome()
		{
			core = new CoreGene();
		
		}
		
		public CoreGene GetRoot()
		{
			return core;
		}
	
		public Structure Generate(Vector3 startPos)
		{
			return core.Express(startPos,null);
		}
		
	}


