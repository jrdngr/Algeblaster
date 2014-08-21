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

    void Start() {
        base.Start();
        if (!hasFactor)
            GetComponent<MSPartDivide>().enabled = false;
    }

    void Update() {
        CheckDead();
    }

    public void GotHit(WeaponHit weaponHit) {
       if (!hasFactor)
           healthMgr.SubtractHP(weaponHit.damage);
    }
}
