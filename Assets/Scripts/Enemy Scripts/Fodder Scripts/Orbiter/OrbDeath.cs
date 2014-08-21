using UnityEngine;
using System.Collections;

public class OrbDeath : MonoBehaviour {

    private Orbiter myVars;
    private EnemyHealthManager healthMgr;
    private FodderPowerupMgr powerupMgr;
    private GameObject deathEffect;

    void Start() {
        myVars = GetComponent<Orbiter>();
        healthMgr = GetComponent<EnemyHealthManager>();
        powerupMgr = GetComponent<FodderPowerupMgr>();
        deathEffect = myVars.DeathEffect;
    }

    void Update() {
        if (healthMgr.CurrentHP <= 0)
            Kill();
    }

    void Kill() {
        powerupMgr.SpawnOrbs(transform.position, 1);
        GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(explosion, 2f);
        Destroy(this.gameObject);        
    }

}
