using UnityEngine;
using System.Collections.Generic;

public class Builder : MonoBehaviour
{
	private List<Creature> creatures;
	
	World world;
	// Use this for initialization
	void Start ()
	{
		//set initial child gene likelihoods
		//set initial mutation likelihoods
		//set creature lifespan
		world = GetComponent<World>();
		genesis ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	private void genesis ()
	{
		Creature newCreature;
		creatures = new List<Creature>();
		int creatureCount = 0;
		while (creatures.Count < world.numCreatures)
		{
			newCreature = new Creature(new Vector3(5*(creatureCount + 1),5,5));
			creatures.Add(newCreature);
			creatureCount++;
		}
	}
}
