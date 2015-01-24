using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Villager_Manager_Script : MonoBehaviour {
	public GameObject villager;
	public List<GameObject> population;
	public Vector3 spawnPoint;
	public int initialPopulation;
	public float spawnDelay;
	float lastSpawn;
	// Use this for initialization
	void Start () {
		lastSpawn = Time.time;
		for (int i = 0; i < initialPopulation; i++)
		{
			CreateNewVillage();
		}
	}
	
	void CreateNewVillage()
	{
		GameObject newVillager = (GameObject)GameObject.Instantiate (villager, spawnPoint, new Quaternion(0,0,0,0));
		population.Add (newVillager);
	}
	void Update () {
		if(Time.time - lastSpawn > spawnDelay)
		{
			lastSpawn = Time.time;
			CreateNewVillage();
		}
	}
}
