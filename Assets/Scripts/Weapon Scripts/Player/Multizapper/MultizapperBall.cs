using UnityEngine;
using System.Collections;

public class MultizapperBall : Projectile {

    private const float ballRadius = 1f;

    private float zapDelay;
    private float zapRange;
    private float zapSpeed;
    private int zapDamage;
    private float chainRange;
    private int numberOfChains;
    private bool canZap = true;
    private Timer zapDelayTimer;
    private GameObject zap;
    private GameObject myLauncher;
    public GameObject MyLauncher {
        get { return myLauncher; }
        set { myLauncher = value; }
    }

    private bool isQuitting = false;

    void Start(){
        zapDelayTimer = gameObject.AddComponent<Timer>();
        zapDelayTimer.Trigger += ResetZap;
        GetComponent<SphereCollider>().radius = zapRange;
    }

    void Update() {

    }

    void OnDisable() {
        if (!isQuitting)
            myLauncher.GetComponent<MultizapperLauncher>().BallDead();
    }

    void OnApplicationQuit() {
        isQuitting = true;
    }

    void ResetZap(){
        canZap = true;
    }

    protected override void OnTriggerEnter(Collider other) {
        
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.GetComponent<EnemyHealthManager>() != null && Vector3.Distance(transform.position, other.transform.position) <= ballRadius) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(myEffect, 5f);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.GetComponent<EnemyHealthManager>() != null && canZap) {
            Zap(other.transform);
            canZap = false;
            zapDelayTimer.Go(zapDelay);
        }
    }

    void Zap(Transform zapToObject) {
        GameObject myZap = (GameObject)Instantiate(zap, transform.position, Quaternion.identity);
        myZap.GetComponent<MultizapperZap>().SetProperties(zapToObject, zapSpeed, zapDamage, chainRange, numberOfChains, hit);
    }

    public void SetBallProperties(GameObject zapPrefab, float zDelay, float zRange, float zSpeed, int zDamage, float cRange, int numChains) {
        zap = zapPrefab;
        zapDelay = zDelay;
        zapRange = zRange;
        zapSpeed = zSpeed;
        zapDamage = zDamage;
        chainRange = cRange;
        numberOfChains = numChains;
    }

}
