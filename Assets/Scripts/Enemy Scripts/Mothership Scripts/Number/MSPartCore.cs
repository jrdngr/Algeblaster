using UnityEngine;
using System.Collections;

#pragma warning disable 108

//The core part.  This is the 'x' in solve for x
//The core part can also contain the MSDivide script which adds a factor before the x
public class MSPartCore : MothershipPart {

    private bool hasFactor = true;

    public bool HasFactor {
        get {
            return hasFactor;
        }
        set {
            hasFactor = value;
        }
    }

    protected override void Start() {
        base.Start();
        if (!hasFactor)
            GetComponent<MSPartDivide>().enabled = false;
    }

    void Update() {
        CheckDead();
    }

    public override void CheckDead() {
        if (healthMgr.CurrentHP == 0) {
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(this.gameObject);
            GameObject.Find("Game Manager").GetComponent<EventManager>().enemiesDead = true;
        }
    }

    public void GotHit(WeaponHit weaponHit) {
       if (!hasFactor)
           healthMgr.SubtractHP(weaponHit.damage);
    }
}
