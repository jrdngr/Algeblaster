using UnityEngine;
using System.Collections;

//Numerators function as the power generators for their attached operators
//Operators make shields
//onLeft determines whether the operator on the right or left is powered
public class MSPartNumerator : MothershipPart {

    [SerializeField] private int hpTrigger;  //How many hitpoints must be lost before the number changes
    [SerializeField] private int berserkThreshold;
    [SerializeField] private float berserkTickTime;
    [SerializeField] private float chainReactionDelay;
    [SerializeField] private TextMesh numberLabel;

    private int myValue = 0;
    private int initialValue;
    private bool hasDenominator;
    private bool isBerserk = false;
    private bool isPositive;
    private bool inFactor = false;
    private bool berserkTickReady = true;
    private Color startColor;
    private CodeMgr codeMgr;
    private GameObject poweredObject;
    private Timer berserkTimer;

    public int MyValue {
        get {
            return myValue;
        }
        set {
            myValue = value;
            initialValue = value;
        }
    }
    public bool HasDenominator {
        get {
            return hasDenominator;
        }
        set {
            hasDenominator = value;
        }
    }
    public bool InFactor {
        get {
            return inFactor;
        }
        set {
            inFactor = value;
        }
    }
    public bool IsPositive {
        get {
            return isPositive;
        }
        set {
            isPositive = value;
        }
    }
    public GameObject PoweredObject {
        get {
            return poweredObject;
        }
        set {
            poweredObject = value;
        }
    }

    new void Awake() {
        base.Awake();
        healthMgr = GetComponent<EnemyHealthManager>();
        myValue = Random.Range(2, 10);
        initialValue = myValue;
        healthMgr.MaxHP = hpTrigger;
        startColor = renderer.material.GetColor("_Color");
    }

    new void Start() {
        base.Start();
        codeMgr = msCode.GetComponent<CodeMgr>();
        numberLabel.text = myValue.ToString();
        CheckPositive();
        if (!inFactor) {
            SetPoweredObject();
        }
        berserkTimer = gameObject.AddComponent<Timer>();
        berserkTimer.Trigger += BerserkTick;
    }

    void Update() {
        CheckDead();
        ManageBerserk();
       }

    void OnDisable() {
        if (!isQuitting) {
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(poweredObject.gameObject, chainReactionDelay);
        }
    }

    void ManageBerserk() {
        if (myValue >= initialValue + berserkThreshold && !isBerserk) {
            isBerserk = true;
            healthMgr.MaxHP = hpTrigger;
            berserkTimer.Go(berserkTickTime);
        }
        if (isBerserk) {
            renderer.material.SetColor("_Color", Color.red);
            if (myValue == initialValue) {
                isBerserk = false;
                renderer.material.SetColor("_Color", startColor);
            }
            if (berserkTickReady) {
                myValue -= 1;
                if (isPositive)
                    codeMgr.MyValue -= 1;
                else
                    codeMgr.MyValue += 1;
                if (myIndex == 0) {         //Special code for the leftmost term
                    myValue += 2;
                    codeMgr.MyValue += 2;
                }
                numberLabel.GetComponent<TextMesh>().text = myValue.ToString();
                berserkTickReady = false;
                berserkTimer.Go(berserkTickTime);
            }
        }
        if (myIndex == 0 && myValue < -berserkThreshold - initialValue)
            isBerserk = true;
    }

    void BerserkTick() {
        berserkTickReady = true;    
    }

    void CheckPositive() {
        if (myValue < 0 && myIndex != 0 && !inFactor) {
            myValue = Mathf.Abs(myValue);
            isPositive = !isPositive;
            if (myMothership.GetComponent<Mothership>().PartList[myIndex - 1])
                myMothership.GetComponent<Mothership>().PartList[myIndex - 1].GetComponent<MSPartPlusMinus>().ChangeSign();
        }
    }

    public void ChangedValue() {
        numberLabel.GetComponent<TextMesh>().text = myValue.ToString();
    }

    public void SetPoweredObject() {
        if (OnLeft)
            poweredObject = myMothership.GetComponent<Mothership>().PartList[MyIndex + 1];
        else
            poweredObject = myMothership.GetComponent<Mothership>().PartList[MyIndex - 1];
        if (myIndex == 0 || myMothership.GetComponent<Mothership>().Parts[myIndex - 1] == MothershipPart.Type.plus)
            isPositive = true;
        else if (myMothership.GetComponent<Mothership>().Parts[myIndex - 1] == MothershipPart.Type.minus)
            isPositive = false;
    }

    public void GotHit(WeaponHit weaponHit) {
        if (!isBerserk) {
            healthMgr.SubtractHP(weaponHit.damage);
            if (healthMgr.CurrentHP <= 0) {
                if (weaponHit.type == Weapon.WeaponType.pos) {
                    if (isPositive)
                        myValue = myValue + weaponHit.frequency;
                    else
                        myValue = myValue - weaponHit.frequency;
                    codeMgr.AddValue (weaponHit.frequency);
                }
                else if (weaponHit.type == Weapon.WeaponType.neg) {
                    if (isPositive)
                        myValue = myValue - weaponHit.frequency;
                    else
                        myValue = myValue + weaponHit.frequency;
                    codeMgr.AddValue(-weaponHit.frequency);
                }

                if (myValue == 0)
                    Destroy(this.gameObject);
                CheckPositive();
                healthMgr.CurrentHP = hpTrigger;
                numberLabel.GetComponent<TextMesh>().text = myValue.ToString();
            }
        }
    
    }
}
