using UnityEngine;
using System.Collections;

#pragma warning disable 108

//This part contains several other parts that parent to the main mothership when this part is destroyed
public class MSPartFactor : MothershipPart {

    [SerializeField] GameObject myCore;
    [SerializeField] GameObject myNumber;
    [SerializeField] GameObject myOperator;
    [SerializeField] GameObject myShield;
    [SerializeField] GameObject leftParen;
    [SerializeField] GameObject rightParen;
    [SerializeField] GameObject shieldPop;
    [SerializeField] TextMesh numberLabel;

    private int myFactor;
    private bool factorActive = false;
    private CodeManager codeMgr;

    public int MyFactor {
        get {
            return myFactor;
        }
        set {
            myFactor = value;
        }
    }
    public bool FactorActive {
        get {
            return factorActive;
        }
        set {
            factorActive = value;
        }
    }

    protected override void Awake() {
        myFactor = Random.Range(2, 9);
        numberLabel.text = myFactor.ToString();
        myNumber.GetComponent<MSPartNumerator>().InFactor = true;
    }

    protected override void Start() {
        base.Start();
        codeMgr = GameObject.FindGameObjectWithTag("SecretCode").GetComponent<CodeManager>();  //THIS IS BAD.  IT SHOULD INHERIT AN MSCODE.  FIX IT
        myCore.GetComponent<MSPartDivide>().MyFactor *= myFactor;
        myNumber.GetComponent<MSPartNumerator>().MyValue *= myFactor;
        myNumber.GetComponent<MSPartNumerator>().PoweredObject = myOperator;
        myOperator.GetComponent<MSPartPlusMinus>().ShieldActive = false;
        if (Random.Range(0, 2) == 0) {
            myOperator.GetComponent<MothershipPart>().MyType = MothershipPart.Type.plus;
            myNumber.GetComponent<MSPartNumerator>().IsPositive = true;
        }
        else {
            myOperator.GetComponent<MothershipPart>().MyType = MothershipPart.Type.minus;
            myNumber.GetComponent<MSPartNumerator>().IsPositive = false;
        }
    }

    void Update() {
        if (factorActive && !numberLabel.renderer.enabled) {
            numberLabel.text = myFactor.ToString();
            numberLabel.renderer.enabled = true;
            myCore.GetComponent<MSPartDivide>().MyFactor /= myFactor;
            myCore.GetComponent<MSPartDivide>().ChangedValue();
            myNumber.GetComponent<MSPartNumerator>().MyValue /= myFactor;
            myNumber.GetComponent<MSPartNumerator>().ChangedValue();
        }

    }

    int GCD() {
        int gcd = 1;
        int max = GetCoreFactor();
        if (GetTermNumber() > GetCoreFactor())
            max = GetTermNumber();
        for (int i = 1; i < max / 2; i++) {
            if (GetCoreFactor() % i == 0 && GetTermNumber() % i == 0)
                gcd = i;
        }
        return gcd;
    }

    public int GetCoreFactor() {
        return myCore.GetComponent<MSPartDivide>().MyFactor;
    }

    public int GetTermNumber() {
        return myNumber.GetComponent<MSPartNumerator>().MyValue;
    }

    public void GotHit(WeaponHit weaponHit) {
        if (factorActive && weaponHit.type == WeaponHit.WeaponType.div && weaponHit.frequency == myFactor) {
            if (codeMgr.MyDenominator == 1)
                codeMgr.MyDenominator = myFactor;
            else
                codeMgr.MyDenominator *= myFactor;
            if (GCD() == 1) {
                myOperator.GetComponent<MSPartPlusMinus>().ShieldActive = true;
                myCore.transform.parent = transform.parent;
                myNumber.transform.parent = transform.parent;
                myOperator.transform.parent = transform.parent;
                Destroy(leftParen.gameObject);
                Destroy(rightParen.gameObject);
                GameObject pop = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(pop, 2f);
                Destroy(this.gameObject);
                GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
                Destroy(explosion, 2f);
                Destroy(this.gameObject);
            }
            else {
                factorActive = false;
                numberLabel.renderer.enabled = false;
                myShield.GetComponent<MSFactorShield>().Reset();
            }
        }

    }


}
