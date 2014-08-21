using UnityEngine;
using System.Collections;

// Handles Multizapper orb projectile
// Orb projectile multiplies the term that it hits
public class Multizapper : Weapon {

    public int Damage {
        get { return damage; }
    }
    public int Frequency {
        get { return frequency; }
    }
    public pWeaponMgr WeaponManager { get; set; }

    	void Start(){
		gui = GameObject.Find ("Gui");
		frequency = gui.GetComponent<FrequencyMgr>().WeaponFrequency;
	}

    void OnDisable() {
        WeaponManager.ZapperOut = false;
    }

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
