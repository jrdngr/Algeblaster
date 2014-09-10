using UnityEngine;
using System.Collections;

public class ColorMSPartNumerator : MothershipPart {

    public int hpTrigger;
    public int berserkThreshold;
    public float berserkTickTime;
    public float chainReactionDelay;
    public WeaponHit.WeaponColor myColor;
    public bool inFactor = false;
    public GameObject poweredObject;

    
    private Renderer myRenderer;
    private bool isPositive;
    private bool isPrimary = false;
    private bool isBerserk = false;
    private bool berserkTickReady = true;
    private Timer berserkTimer;

    private Color purple = new Color(1, 0, 1, 1);
    private Color orange = new Color(1, 0.5f, 0, 1);

    protected override void Awake() {
        base.Awake();
        myRenderer = GetComponent<Renderer>();
        SetColor();        
    }

    protected override void Start() {
        base.Start();
        berserkTimer = gameObject.AddComponent<Timer>();
        berserkTimer.Trigger += BerserkTick;
        if (!inFactor) {
            SetPoweredObject();
        }

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
        if (isBerserk) {
            Debug.Log("I'm berserk!");
            isBerserk = false;
        }
    }

    void BerserkTick() {
        berserkTickReady = true;
    }

    public void SetPoweredObject() {
        if (OnLeft)
            poweredObject = myMothership.GetComponent<Mothership>().PartList[MyIndex + 1];
        else
            poweredObject = myMothership.GetComponent<Mothership>().PartList[MyIndex - 1];
        if (MyIndex == 0 || myMothership.GetComponent<Mothership>().Parts[MyIndex - 1] == MothershipPart.Type.plus)
            isPositive = true;
        else if (myMothership.GetComponent<Mothership>().Parts[MyIndex - 1] == MothershipPart.Type.minus)
            isPositive = false;
    }

    public void GotHit(WeaponHit weaponHit) {
        if (!isBerserk) {
            healthMgr.SubtractHP(weaponHit.damage);
            if (healthMgr.CurrentHP <= 0) {
                if (weaponHit.type == WeaponHit.WeaponType.pos) {
                    if (!isPositive && isPrimary && myColor == weaponHit.color)
                        Destroy(this.gameObject);
                    else if (isPrimary)
                        SetColor(AddColors(myColor, weaponHit.color));
                    else
                        isBerserk = true;
                }
                else if (weaponHit.type == WeaponHit.WeaponType.neg) {
                    if (isPositive && isPrimary && myColor == weaponHit.color)
                        Destroy(this.gameObject);
                    else if (!isPrimary)
                        SetColor(SubtractColors(myColor, weaponHit.color));
                    else
                        isBerserk = true;
                }
                healthMgr.CurrentHP = hpTrigger;
            }

        }
    }

    WeaponHit.WeaponColor AddColors(WeaponHit.WeaponColor firstColor, WeaponHit.WeaponColor secondColor) {
        WeaponHit.WeaponColor returnColor = WeaponHit.WeaponColor.blue;
        if (firstColor == WeaponHit.WeaponColor.blue) {
            if (secondColor == WeaponHit.WeaponColor.red)
                returnColor = WeaponHit.WeaponColor.purple;
            else
                returnColor = WeaponHit.WeaponColor.green;
        }
        else if (firstColor == WeaponHit.WeaponColor.red) {
            if (secondColor == WeaponHit.WeaponColor.blue)
                returnColor = WeaponHit.WeaponColor.purple;
            else
                returnColor = WeaponHit.WeaponColor.orange;
        }
        else if (firstColor == WeaponHit.WeaponColor.yellow) {
            if (secondColor == WeaponHit.WeaponColor.blue)
                returnColor = WeaponHit.WeaponColor.green;
            else
                returnColor = WeaponHit.WeaponColor.orange;
        }

        return returnColor;
    }

    //secondColor is "subtracted" from firstColor
    WeaponHit.WeaponColor SubtractColors(WeaponHit.WeaponColor firstColor, WeaponHit.WeaponColor secondColor) {
        WeaponHit.WeaponColor returnColor = WeaponHit.WeaponColor.blue;
        if (firstColor == WeaponHit.WeaponColor.purple) {
            if (secondColor == WeaponHit.WeaponColor.blue)
                returnColor = WeaponHit.WeaponColor.red;
            else if (secondColor == WeaponHit.WeaponColor.red)
                returnColor = WeaponHit.WeaponColor.blue;
            else
                returnColor = WeaponHit.WeaponColor.purple;                
        }
        else if (firstColor == WeaponHit.WeaponColor.green) {
            if (secondColor == WeaponHit.WeaponColor.blue)
                returnColor = WeaponHit.WeaponColor.yellow;
            else if (secondColor == WeaponHit.WeaponColor.yellow)
                returnColor = WeaponHit.WeaponColor.blue;
            else
                returnColor = WeaponHit.WeaponColor.green;
        }
        else if (firstColor == WeaponHit.WeaponColor.orange) {
            if (secondColor == WeaponHit.WeaponColor.yellow)
                returnColor = WeaponHit.WeaponColor.red;
            else if (secondColor == WeaponHit.WeaponColor.red)
                returnColor = WeaponHit.WeaponColor.yellow;
            else
                returnColor = WeaponHit.WeaponColor.orange;
        }

        return returnColor;
    }


    void SetColor(WeaponHit.WeaponColor newColor) {
        switch (newColor) {
            case WeaponHit.WeaponColor.blue:
                myColor = WeaponHit.WeaponColor.blue;
                myRenderer.material.color = Color.blue;
                isPrimary = true;
                break;
            case WeaponHit.WeaponColor.red:
                myColor = WeaponHit.WeaponColor.red;
                myRenderer.material.color = Color.red;
                isPrimary = true;
                break;
            case WeaponHit.WeaponColor.yellow:
                myColor = WeaponHit.WeaponColor.yellow;
                myRenderer.material.color = Color.yellow;
                isPrimary = true;
                break;
            case WeaponHit.WeaponColor.purple:
                myColor = WeaponHit.WeaponColor.purple;
                myRenderer.material.color = purple;
                isPrimary = false;
                break;
            case WeaponHit.WeaponColor.green:
                myColor = WeaponHit.WeaponColor.green;
                myRenderer.material.color = Color.green;
                isPrimary = false;
                break;
            case WeaponHit.WeaponColor.orange:
                myColor = WeaponHit.WeaponColor.orange;
                myRenderer.material.color = orange;
                isPrimary = false;
                break;
        }
    }

    void SetColor() {
        switch (Random.Range(0, 6)) {
            case 0:
                myColor = WeaponHit.WeaponColor.blue;
                myRenderer.material.color = Color.blue;
                isPrimary = true;
                break;
            case 1:
                myColor = WeaponHit.WeaponColor.red;
                myRenderer.material.color = Color.red;
                isPrimary = true;
                break;
            case 2:
                myColor = WeaponHit.WeaponColor.yellow;
                myRenderer.material.color = Color.yellow;
                isPrimary = true;
                break;
            case 3:
                myColor = WeaponHit.WeaponColor.purple;
                myRenderer.material.color = purple;
                isPrimary = false;
                break;
            case 4:
                myColor = WeaponHit.WeaponColor.green;
                myRenderer.material.color = Color.green;
                isPrimary = false;
                break;
            case 5:
                myColor = WeaponHit.WeaponColor.orange;
                myRenderer.material.color = orange;
                isPrimary = false;
                break;
        }
    }


}
