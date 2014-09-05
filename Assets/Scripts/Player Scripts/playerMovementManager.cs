using UnityEngine;
using System.Collections;

#pragma warning disable 0414

public class playerMovementManager : MonoBehaviour {

    public enum DashDirection { left = -1, right = 1};

    //Basic Movement
    private float xMovement;
    public float XMovement {
        get { return xMovement; }
        set { xMovement = value; }
    }
    private float yMovement;
    public float YMovement {
        get { return yMovement; }
        set { yMovement = value; }
    }
    private float thrustForce;
    private float maxSpeed;
    private Rect myBounds;
    
    //Dashing
    private float dashForce;
    private float dashTime;
    private float dashDelay;
    private int dashDirection;
    private bool dashReady = true;
    private bool dashing;
    private Timer dashTimer;
    private Timer nextDashTimer;
    private GameObject dashEffect;
    private GameObject leftDashEffect;
    private GameObject rightDashEffect;

    //Bumping
    private int bumpDamage;
    private float bumpForce;
    private float bumpTime;
    private bool bumping = false;
    private GameObject bumpEffect;
    private Timer bumpTimer;
    
    private PlayerManager playerManager;

    void Awake() {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        myBounds = playerManager.GetComponent<LevelManager>().playerBounds;
        //Initialize dash stuff
        leftDashEffect = transform.Find("DashEffectLeft").gameObject;
        rightDashEffect = transform.Find("DashEffectRight").gameObject;
        thrustForce = playerManager.ThrustForce;
        maxSpeed = playerManager.MaxSpeed;
        dashForce = playerManager.DashForce;
        dashTime = playerManager.DashTime;
        dashDelay = playerManager.DashDelay;
        dashEffect = playerManager.DashEffect;
        dashTimer = gameObject.AddComponent<Timer>();
        dashTimer.Trigger += DashOver;
        nextDashTimer = gameObject.AddComponent<Timer>();
        nextDashTimer.Trigger += DashReady;
        //Initialize bump stuff
        bumpDamage = playerManager.BumpDamage;
        bumpForce = playerManager.BumpForce;
        bumpTime = playerManager.BumpTime;
        bumpEffect = playerManager.BumpEffect;
        bumpTimer = gameObject.AddComponent<Timer>();
        bumpTimer.Trigger += BumpOver;
    }

    void FixedUpdate() {
        Vector3 velocityLimits = new Vector3();
        rigidbody.AddForce(new Vector3(xMovement * thrustForce, yMovement * thrustForce, 0));
        
        //Enforce velocity limit
        velocityLimits.x = Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed);
        velocityLimits.y = Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed);
        velocityLimits.z = 0;        
        if (!dashing && !bumping)
            rigidbody.velocity = velocityLimits;

        //Enforce player bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, myBounds.xMin, myBounds.xMax), Mathf.Clamp(transform.position.y, myBounds.yMin, myBounds.yMax), 0);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.contacts[0].thisCollider.name != "Shield") {
            if ((collision.gameObject.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("Fodder")) && !dashing) {
                GameObject bump = (GameObject)Instantiate(bumpEffect, transform.position, Quaternion.identity);
                rigidbody.AddForce(collision.contacts[0].normal * bumpForce, ForceMode.Impulse);
                bumping = true;
                bumpTimer.Go(bumpTime);
                Destroy(bump, 2f);
                GetComponent<playerHealthManager>().Hit(bumpDamage);
                if (collision.gameObject.CompareTag("Fodder"))
                    collision.gameObject.GetComponent<EnemyHealthManager>().SubtractHP(bumpDamage);
            }
        }
    }


    void DashReady() {
        dashReady = true;
    }

    void DashOver() {
        dashing = false;
    }

    void BumpOver() {
        bumping = false;
    }

    public void StartDash (DashDirection dir) {
        if (dashReady) {
            GameObject newDashEffect = null;
            if (dir == DashDirection.left)
                newDashEffect = (GameObject)Instantiate(dashEffect, leftDashEffect.transform.position, Quaternion.Euler(0,90,0));
            else if (dir == DashDirection.right)
                newDashEffect = (GameObject)Instantiate(dashEffect, rightDashEffect.transform.position, Quaternion.Euler(0,-90,0));
            Destroy(newDashEffect, 1f);
            rigidbody.AddForce(new Vector3(dashForce, 0, 0) * (int)dir, ForceMode.Impulse);
            dashing = true;
            dashDirection = (int)dir;
            dashReady = false;
            dashTimer.Go(dashTime);
            nextDashTimer.Go(dashDelay);
        }
    }
    
}
