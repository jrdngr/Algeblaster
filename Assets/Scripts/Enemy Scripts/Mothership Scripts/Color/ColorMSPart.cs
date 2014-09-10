using UnityEngine;
using System.Collections;

public class ColorMSPart : MonoBehaviour {

    public GameObject deathEffect;

    public int MyIndex { get; set; }
    public bool OnLeft { get; set; }
    public bool IsCore { get; set; }
    public MothershipPart.Type MyType { get; set; }

    protected bool isQuitting = false;
    protected GameObject[] guns = new GameObject[3];
    protected EnemyHealthManager healthMgr;
    protected GameObject myMothership;

    protected virtual void Awake() {
        healthMgr = GetComponent<EnemyHealthManager>();
        guns[0] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunLine");
        guns[1] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunSpread");
        guns[2] = (GameObject)Resources.Load("Weapons/Enemy/EnemyGunSweep");
        GameObject myGun = (GameObject)Instantiate(guns[Random.Range(0, 3)], transform.position, Quaternion.identity);
        myGun.transform.parent = transform;
    }

    protected virtual void Start() {
//        myMothership = transform.parent.gameObject;
    }

    protected void OnApplicationQuit() {
        isQuitting = true;
    }

    public void CheckDead() {
        if (healthMgr.CurrentHP == 0) {
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            Destroy(this.gameObject);
        }
    }

}
