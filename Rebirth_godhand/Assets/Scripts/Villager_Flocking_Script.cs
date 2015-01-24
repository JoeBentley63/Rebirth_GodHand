using UnityEngine;
using System.Collections;

public class Villager_Flocking_Script : MonoBehaviour {
	
	public float neighbourRadius;
	public float alignmentRadius;
	public float cohesionRadius;
	public float seperationRadius;
	
	public float alignmentStrenght;
	public float cohesionStrenght;
	public float seperationStrenght;
	
	public LayerMask layerMask;
	
	void Update () {
		this.rigidbody.velocity += (AlignmentBehaviour() *alignmentStrenght) + (CohesionBehaviour()*cohesionStrenght) + (SeperationBehaviour()*seperationStrenght);
	}
	
	Vector3 AlignmentBehaviour(){
		Vector3 velocityModifier = new Vector3(0,0,0);
		var neighborCount = 0;
		Collider[] neighbours = Physics.OverlapSphere (this.transform.position, neighbourRadius, layerMask);
		foreach(Collider neighbour in neighbours)
		{
			if (neighbour != this.collider)
			{
				if (Vector3.Distance(this.transform.position,neighbour.transform.position) < alignmentRadius)
				{
					velocityModifier.x += neighbour.rigidbody.velocity.x;
					velocityModifier.z += neighbour.rigidbody.velocity.z;
					neighborCount++;
				}
			}
		}
		if (neighborCount != 0) 
		{
			velocityModifier.x /= neighborCount;
			velocityModifier.y /= neighborCount;
		}
		velocityModifier.Normalize ();
			
		return velocityModifier;
	}
	
	Vector3 CohesionBehaviour(){
		Vector3 velocityModifier = new Vector3(0,0,0);
		var neighborCount = 0;
		Collider[] neighbours = Physics.OverlapSphere (this.transform.position, neighbourRadius, layerMask);
		foreach(Collider neighbour in neighbours)
		{
			if (neighbour != this.collider)
			{
				if (Vector3.Distance(this.transform.position,neighbour.transform.position) < alignmentRadius)
				{
					velocityModifier.x += neighbour.transform.position.x;
					velocityModifier.z += neighbour.transform.position.z;
					neighborCount++;
				}
			}
		}
		if (neighborCount != 0) 
		{
			velocityModifier.x /= neighborCount;
			velocityModifier.z /= neighborCount;
			Vector3 returnedVelocity = new Vector3(velocityModifier.x - this.transform.position.x,0, velocityModifier.z - this.transform.position.z);
			returnedVelocity.Normalize();
		}
		velocityModifier.Normalize();
		
		return velocityModifier;
	}
	
	Vector3 SeperationBehaviour(){
		Vector3 velocityModifier = new Vector3(0,0,0);
		var neighborCount = 0;
		Collider[] neighbours = Physics.OverlapSphere (this.transform.position, neighbourRadius, layerMask);
		foreach(Collider neighbour in neighbours)
		{
			if (neighbour != this.collider)
			{
				if (Vector3.Distance(this.transform.position,neighbour.transform.position) < alignmentRadius)
				{
					velocityModifier.x += neighbour.transform.position.x - this.transform.position.x;
					velocityModifier.z += neighbour.transform.position.z - this.transform.position.z;
					neighborCount++;
				}
			}
		}
		if (neighborCount != 0) 
		{
			velocityModifier.x *= -1;
			velocityModifier.z *= -1;
		}
		velocityModifier.Normalize();
		
		return velocityModifier;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position,cohesionRadius);
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere (transform.position,neighbourRadius);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position,alignmentRadius);
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (transform.position,seperationRadius);

	}
}
