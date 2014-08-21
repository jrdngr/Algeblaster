using UnityEngine;
using System.Collections;

#pragma warning disable 108

//Single denominator that goes below the entire equation
public class MSPartDenominator : MothershipPart {

    [SerializeField] private GameObject myShield;
    [SerializeField] private GameObject shieldPopEffect;
    [SerializeField] private TextMesh numberLabel;

    public GameObject MyShield {
        get {
            return myShield;
        }
    }

    private int myValue;
    private CodeMgr codeMgr;

    void Awake() {
        base.Awake();
        healthMgr = GetComponent<EnemyHealthManager>();
        codeMgr = msCode.GetComponent<CodeMgr>();
        myValue = Random.Range(2, 9);
        numberLabel.text = myValue.ToString();
    }

    void Start() {
        
    }

    public void GotHit(WeaponHit hit) {
        if (hit.type == Weapon.WeaponType.mult && hit.frequency == myValue) {
            healthMgr.SubtractHP(hit.damage);
            if (healthMgr.CurrentHP <= 0) {
                codeMgr.MultiplyValue(myValue);
                GameObject shieldPop = (GameObject)Instantiate(shieldPopEffect, myShield.transform.position, Quaternion.identity);
                shieldPop.transform.localScale = new Vector3(transform.parent.GetComponent<Mothership>().CenterX + 1, 1, 2);
                shieldPop.particleSystem.enableEmission = true;
                GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
                Destroy(shieldPop, 2f);
                Destroy(this.gameObject);                
            }
        }
    }

}
