using UnityEngine;
using System.Collections;

//All mothership parts inherit from this class
public class MothershipPart : MonoBehaviour {

    public enum Type { core, plus, minus, times, divide, numerator, denominator, factor, blank}

    public GameObject deathEffect;

    protected int myIndex;
    protected bool onLeft;
    protected bool isCore;
    protected bool isQuitting;
    protected GameObject[] guns = new GameObject[3];
    protected MothershipPart.Type myType;
    protected EnemyHealthManager healthMgr;
    
    protected GameObject myMothership;
    protected GameObject msCode;

    public bool OnLeft {
        get {
            return onLeft;
        }
        set {
            onLeft = value;
        }
    }
    public bool IsCore {
        get {
            return isCore;
        }
        set {
            isCore = value;
        }
    }
    public int MyIndex {
        get {
            return myIndex;
        }
        set {
            myIndex = value;
        }
    }
    public MothershipPart.Type MyType {
        get {
            return myType;
        }
        set {
            myType = value;
        }
    }

    protected virtual void Awake() {
        msCode = GameObject.FindGameObjectWithTag("SecretCode");
        healthMgr = GetComponent<EnemyHealthManager>();
        guns[0] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunLine");
        guns[1] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunSpread");
        guns[2] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunSweep");
        GameObject myGun = (GameObject)Instantiate(guns[Random.Range(0, 3)], transform.position, Quaternion.identity);
        myGun.transform.parent = transform;
    }

    protected virtual void Start() {
        myMothership = transform.parent.gameObject;
    }
    
    protected void OnApplicationQuit() {
        isQuitting = true;
    }

    public virtual void CheckDead() {
        if (healthMgr.CurrentHP == 0) {
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(this.gameObject);
        }
    }

}
