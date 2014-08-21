using UnityEngine;
using System.Collections;

// Common variables and methods for all pickups
// Used by HealthOrb, ExpOrb
public class Pickup : MonoBehaviour {

	[SerializeField] protected float maxSpeed;
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float moveForce;
	[SerializeField] protected float xMax;
	[SerializeField] protected float xMin;
	[SerializeField] protected int value;
	[SerializeField] protected GameObject powerupGot;
	[SerializeField] protected GameObject player;

	void Start(){
		player = GameObject.FindGameObjectWithTag("Player");
		xMax = player.GetComponent<pMoveMgr>().XMax;
		xMin = player.GetComponent<pMoveMgr>().XMin;
	}
}
