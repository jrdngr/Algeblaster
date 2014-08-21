using UnityEngine;
using System.Collections;

//Operators function as shield generators.  They must be powered by an attached numerator
//onLeft determines whether the shield is generated to the left or right of the operator
public class MSPartPlusMinus : MothershipPart {

    [SerializeField] private GameObject textLabel;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject shieldAnchorLeft;
    [SerializeField] private GameObject shieldAnchorRight;
    [SerializeField] private GameObject shieldLostEffect;

    private bool shieldActive = true;

    public bool ShieldActive {
        get {
            return shieldActive;
        }
        set {
            shieldActive = value;
        }
    }


    new void Start() {
        base.Start();
        if (myType == MothershipPart.Type.plus)
            textLabel.GetComponent<TextMesh>().text = "+";
        else if (myType == MothershipPart.Type.minus)
            textLabel.GetComponent<TextMesh>().text = "-";
        if (OnLeft)
           shield.transform.position = shieldAnchorRight.transform.position;
        else
           shield.transform.position = shieldAnchorLeft.transform.position;
    }

    void Update() {
        if (!shieldActive)
            shield.SetActive(false);
        else
            shield.SetActive(true);
    }

    void OnDisable() {
        if (!isQuitting) {
            GameObject shieldLost = (GameObject)Instantiate(shieldLostEffect, shield.transform.position, Quaternion.identity);
            Destroy(shieldLost, 2f);
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
        }
    }

    public void ChangeSign() {
        if (myType == MothershipPart.Type.plus) {
            myType = MothershipPart.Type.minus;
            textLabel.GetComponent<TextMesh>().text = "-";
        }
        else if (myType == MothershipPart.Type.minus) {
            myType = MothershipPart.Type.plus;
            textLabel.GetComponent<TextMesh>().text = "+";
        }
    }

    public void GotHit(WeaponHit weaponHit) {

    }

}
