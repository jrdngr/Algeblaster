using UnityEngine;
using System.Collections;

// Script for a health orb
public class HealthOrb : Pickup {

    //DEBUG or maybe just fun
	void Update(){
		if (Input.GetMouseButtonDown(0)){
			float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			float mouseY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
			Vector3 location = new Vector3(mouseX, mouseY, 0);
			GetComponent<Rigidbody>().AddExplosionForce(1000f, location, 2f, 0f, ForceMode.Force);
		}

	}

	void FixedUpdate(){
	//	rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed),0);
		if (GetComponent<Rigidbody>().velocity.y > -maxSpeed)
			GetComponent<Rigidbody>().AddForce(new Vector3(0, -moveForce, 0));
		if (transform.position.x >= xMax || transform.position.x <= xMin)
			GetComponent<Rigidbody>().velocity = new Vector3(-GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 0);
	}

	void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
			other.transform.parent.GetComponent<playerHealthManager>().Heal (value);
			GameObject got = (GameObject)Instantiate(powerupGot, transform.position, Quaternion.identity);
			Destroy(got, 2);
			Destroy (this.gameObject);
		}
	}
}
