using UnityEngine;
using System.Collections;

public class MultizapperBall : Projectile {

    private const float ballRadius = 1f;
    private const float tetherTime = 10f;

    private bool hasTether = false;
    private bool wasTethered = false;
    private bool hasBallOfSteel;
    private float zapDelay;
    private float zapRange;
    private float zapSpeed;
    private int zapDamage;
    private float chainRange;
    private int numberOfChains;
    private bool canZap = true;
    private Timer zapDelayTimer;
    private Timer tetherTimer;
    private LineRenderer myLine;
    private GameObject zap;
    private GameObject myLauncher;
    public GameObject MyLauncher {
        get { return myLauncher; }
        set { myLauncher = value; }
    }

    private bool isQuitting = false;

    void Start(){
        myLine = GetComponent<LineRenderer>();
        zapDelayTimer = gameObject.AddComponent<Timer>();
        zapDelayTimer.Trigger += ResetZap;
        tetherTimer = gameObject.AddComponent<Timer>();
        tetherTimer.Trigger += KillTether;
        if (hasTether)
            tetherTimer.Go(tetherTime);
        if (!hasBallOfSteel)
            GetComponent<SphereCollider>().radius = zapRange;
    }

    protected override void FixedUpdate() {
        if (!wasTethered) 
            GetComponent<Rigidbody>().velocity = myVelocity;
        if (!hasTether && 
            (transform.position.y > myBounds.yMax || transform.position.y < myBounds.yMin ||
            transform.position.x > myBounds.xMax || transform.position.x < myBounds.xMin))
            Destroy(this.gameObject);
    }

    void Update() {
        if (hasTether && Input.GetButtonDown("Fire1"))
            KillTether();
        if (hasTether) {
            myLine.SetPosition(0, transform.position);
            myLine.SetPosition(1, myLauncher.transform.position);
        }
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

    void KillTether() {
        GetComponent<SpringJoint>().breakForce = 0;
        myLine.enabled = false;
        hasTether = false;
        tetherTimer.Cancel();
    }

    void OnTriggerStay(Collider other) {
        if (!hasBallOfSteel && other.gameObject.GetComponent<EnemyHealthManager>() != null && Vector3.Distance(transform.position, other.transform.position) <= ballRadius) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(myEffect, 5f);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.GetComponent<EnemyHealthManager>() != null && canZap && !hasBallOfSteel) {
            Zap(other.transform);
            canZap = false;
            zapDelayTimer.Go(zapDelay);
        }
    }


    protected override void OnTriggerEnter(Collider other) {
        if (hasBallOfSteel && other.gameObject.GetComponent<EnemyHealthManager>() != null) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(myEffect, 5f);
        }
    }

    void Zap(Transform zapToObject) {
        GameObject myZap = (GameObject)Instantiate(zap, transform.position, Quaternion.identity);
        myZap.GetComponent<MultizapperZap>().SetProperties(zapToObject, zapSpeed, zapDamage, chainRange, numberOfChains, hit);
    }

    public void SetBallProperties(bool tether, bool steel, GameObject zapPrefab, float zDelay, float zRange, float zSpeed, int zDamage, float cRange, int numChains) {
        hasTether = tether;
        wasTethered = tether;
        hasBallOfSteel = steel;
        zap = zapPrefab;
        zapDelay = zDelay;
        zapRange = zRange;
        zapSpeed = zSpeed;
        zapDamage = zDamage;
        chainRange = cRange;
        numberOfChains = numChains;
    }

}
