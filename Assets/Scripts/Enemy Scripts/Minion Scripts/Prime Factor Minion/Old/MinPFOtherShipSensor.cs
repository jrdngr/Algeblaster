using UnityEngine;
using System.Collections;

// Senses other minions and tells the minion to avoid them.
// Uses DodgeBullet method from the Move Manager
public class MinPFOtherShipSensor : Minion {

	private bool triggered = false;
	private GameObject myParent;
	private Collider sensedObject;
	
	void Start () {
		myParent = (GameObject)transform.parent.gameObject;
	}
	
	void Update () {
		// removes sensor flag if the bullet is destroyed
		if (triggered && !sensedObject)
			myParent.GetComponent<MinPFMoveMgr>().SeesOtherShip = false;
	}
	
	void OnTriggerStay(Collider collision){
        if (collision.CompareTag("Minion") || collision.CompareTag("Mothership") || collision.CompareTag("Player")){
			triggered = true;
			sensedObject = collision;
			myParent.GetComponent<MinPFMoveMgr>().SeesOtherShip = true;
			myParent.GetComponent<MinPFMoveMgr>().MoveAway(collision.transform.position.x, collision.transform.position.y);
		}
	}
	
	void OnTriggerExit(Collider collision){
        if (collision.CompareTag("Minion") || collision.CompareTag("Mothership") || collision.CompareTag("Player")) {
            myParent.GetComponent<MinPFMoveMgr>().SeesOtherShip = false;
		}
	}

}
