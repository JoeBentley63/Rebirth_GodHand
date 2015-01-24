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
	public Vector3 destination;
	public float destinationArrivalDistance = 1;
	public int numArrivedVillagers = 0;
	// Use this for initialization
	void Start () {
		lastSpawn = Time.time;
		for (int i = 0; i < initialPopulation; i++)
		{
			CreateNewVillage();
		}
	}

	public void VillagerArrived()
	{
		numArrivedVillagers ++;
	}
	
	void CreateNewVillage()
	{
		GameObject newVillager = (GameObject)GameObject.Instantiate (villager, spawnPoint, new Quaternion(0,0,0,0));
		population.Add (newVillager);
	}
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition) ; 
			RaycastHit hit ;
			
			Debug.Log ("Click");
			
			if (Physics.Raycast (ray, out hit, 999f)) 
			{
				Debug.Log ("Hit");
				Debug.Log(hit.point);
				destination = hit.point;
				numArrivedVillagers = 0;
			}
		}

		if(Time.time - lastSpawn > spawnDelay)
		{
			lastSpawn = Time.time;
			CreateNewVillage();
		}
	}
}
