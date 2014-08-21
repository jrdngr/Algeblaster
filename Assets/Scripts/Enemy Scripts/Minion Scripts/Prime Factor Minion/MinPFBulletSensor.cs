using UnityEngine;
using System.Collections;

// Senses bullets near the PFM and tells the minion to dodge them
public class MinPFBulletSensor : Minion {

	private bool triggered = false;
	private GameObject myParent;
	private Collider sensedObject;

	void Start () {
		myParent = (GameObject)transform.parent.gameObject;
	}
	
	void Update () {
		// removes sensor flag if the bullet is destroyed
		if (triggered && !sensedObject)
			myParent.GetComponent<MinPFMoveMgr>().SeesBullet = false;
	}
	
	void OnTriggerStay(Collider collision){
		if (collision.CompareTag("WeaponFire")){
			triggered = true;
			sensedObject = collision;
			myParent.GetComponent<MinPFMoveMgr>().SeesBullet = true;
			myParent.GetComponent<MinPFMoveMgr>().MoveAway(collision.transform.position.x, collision.transform.position.y);
		}
	}
	
	void OnTriggerExit(Collider collision){
		if (collision.CompareTag("WeaponFire")){
			myParent.GetComponent<MinPFMoveMgr>().SeesBullet = false;
		}
	}
}
