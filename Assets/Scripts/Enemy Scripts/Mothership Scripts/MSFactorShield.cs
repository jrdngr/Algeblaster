using UnityEngine;
using System.Collections;

//Attached directly to the shield of the MSFactor part
public class MSFactorShield : MonoBehaviour {

    [SerializeField] private int maxHP;

    private int coreFactor;
    private int termNumber;
    private MSPartFactor myFactorObject;
    private EnemyHealthManager healthMgr;

    void Awake() {
        healthMgr = GetComponent<EnemyHealthManager>();
        myFactorObject = transform.parent.GetComponent<MSPartFactor>();
    }

    void Start() {
        Reset();
    }

    public void Reset() {
        healthMgr.MaxHP = maxHP;
        coreFactor = myFactorObject.GetCoreFactor();
        termNumber = myFactorObject.GetTermNumber();
    }

    public void GotHit(WeaponHit weaponHit) {
        if (weaponHit.type == WeaponHit.WeaponType.fac) {
            if ((float)coreFactor % (float)weaponHit.frequency == 0f && (float)termNumber % (float)weaponHit.frequency == 0f) {
                healthMgr.CurrentHP -= weaponHit.damage;
                if (healthMgr.CurrentHP <= 0){
                    myFactorObject.FactorActive = true;
                    myFactorObject.MyFactor = weaponHit.frequency;
                }
            }
        }
    }

}
