using UnityEngine;
using System.Collections;

// Manages Positron Bullet projectiles
/// Positron adds to the term it hits
public class oldPositronBullet : oldWeapon {

	void OnTriggerEnter (Collider collision){
        if (collision.gameObject.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("EnemyShield") || collision.gameObject.CompareTag("Fodder")) {
            if (!collision.gameObject.CompareTag("EnemyShield")) {
                WeaponHit weaponHit = new WeaponHit(damage, frequency, type);
                collision.GetComponent<EnemyHealthManager>().Hit(weaponHit);
            }
            Destroy(this.gameObject);
			GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
			Destroy (pop, 0.5f);
		}
	}
}
