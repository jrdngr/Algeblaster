using UnityEngine;
using System.Collections;

// Controls factor beam projectiles
// Factor beam factors the term that it hits
public class oldFactorBeam : oldWeapon {

	void OnTriggerEnter (Collider collision){
        if (collision.gameObject.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("EnemyShield") || collision.gameObject.CompareTag("Fodder")) {
            if (!collision.gameObject.CompareTag("EnemyShield")) {
//                WeaponHit weaponHit = new WeaponHit(damage, frequency, type);
//                collision.GetComponent<EnemyHealthManager>().Hit(weaponHit);
            }
            GetComponent<WeaponLinearMovement>().Speed = 0;
            particleSystem.enableEmission = false;
            Destroy(this.gameObject, 1f);            
		    GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		    Destroy (pop, 0.5f);
		}
	}
}
