using UnityEngine;
using System.Collections;

// Controls the bullets fired from the Prime Factor Minion
public class MinPFBulletController : Minion {

	[SerializeField] private int bulletDamage = 10;
	[SerializeField] private GameObject hitEffect;

	void OnTriggerEnter (Collider collision){
		if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Shield")){
			if (collision.gameObject.CompareTag("Player"))
				collision.GetComponent<playerHealthManager>().Hit(bulletDamage);
			Destroy(this.gameObject);
			GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity);
			Destroy (pop, 0.5f);
		}
	}

}
