using UnityEngine;
using System.Collections;

// Senses the player and tells the minion to follow him
public class MinPFPlayerSensor : Minion {

	[SerializeField] private GameObject eye;
	private GameObject myParent;


	void Start () {
		myParent = (GameObject)transform.parent.gameObject;
	}
	
	void FixedUpdate () {
	
	}

	void OnTriggerStay(Collider collision){
		if (collision.CompareTag("Player")){
			myParent.GetComponent<MinPFMoveMgr>().SeesPlayer = true;
			myParent.GetComponent<MinPFMoveMgr>().FollowPlayer(collision.transform.position.x, collision.transform.position.y);
			eye.GetComponent<MinPFEyeMgr>().Moving = false;
		}
	}

	void OnTriggerExit(Collider collision){
		if (collision.CompareTag("Player")){
			myParent.GetComponent<MinPFMoveMgr>().SeesPlayer = false;
			eye.GetComponent<MinPFEyeMgr>().Moving = true;
		}
	}
}
