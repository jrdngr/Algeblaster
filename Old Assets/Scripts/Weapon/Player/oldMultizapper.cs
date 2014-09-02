using UnityEngine;
using System.Collections;

// Handles Multizapper orb projectile
// Orb projectile multiplies the term that it hits
public class oldMultizapper : oldWeapon {

    public int Damage {
        get { return damage; }
    }
    public int Frequency {
        get { return frequency; }
    }
    public oldpWeaponMgr WeaponManager { get; set; }

    void OnDisable() {
        WeaponManager.ZapperOut = false;
    }

	void OnTriggerEnter (Collider collision){
        if (collision.gameObject.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("EnemyShield") || collision.gameObject.CompareTag("Fodder")) {
            if (!collision.gameObject.CompareTag("EnemyShield")) {
//                oldWeaponHit weaponHit = new oldWeaponHit(damage, frequency, type);
  //              collision.GetComponent<EnemyHealthManager>().Hit(weaponHit);
            }
            Destroy(this.gameObject);
		    GameObject pop = (GameObject)Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		    Destroy (pop, 0.5f);
		}
	}
}
