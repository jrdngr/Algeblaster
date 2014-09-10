using UnityEngine;
using System.Collections;

public class SquadronMove : MonoBehaviour, IStunnable {

    private const float stunTime = 0.1f;
    private const float keepMyTrail = 5f;
    private const float curveThrust = 1f;

    private int myDirection;
    private Squadron.SquadronPattern myPattern;

    private float thrustForce;
    private float maxSpeed;
    private float bumpForce;
    private float bumpTime;
    private int bumpDamage;
    private bool bumping = false;
    private bool stunned = false;
    private bool switchedDirection = false;
    private Timer bumpTimer;
    private Timer stunTimer;
    private GameObject playerShip;
    private GameObject bumpEffect;
    private PlayerManager playerManager;
    private LevelManager levelManager;
    private Squadron myVars;

    public Rect MyBounds { get; set; }

    void Start() {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        levelManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<LevelManager>();
        MyBounds = levelManager.bounds;
        myVars = GetComponent<Squadron>();
        myDirection = myVars.SquadronDirection;
        if (myDirection == 0)
            myDirection = -1;
        myPattern = myVars.MyPattern;
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        bumpDamage = playerManager.GetComponent<WeaponManager>().FactorBeamTractorBumpDamage;
        bumpEffect = playerManager.BumpEffect;
        //thrustForce = myVars.ThrustForce;
        maxSpeed = myVars.MaxSpeed;
        bumpForce = playerManager.BumpForce;
        bumpTime = playerManager.BumpTime;
        bumpTimer = gameObject.AddComponent<Timer>();
        bumpTimer.Trigger += BumpOver;
        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Trigger += StunOff;
    }

    void FixedUpdate() {
        if (!stunned) {
            float midPoint = MyBounds.yMin + ((MyBounds.yMax - MyBounds.yMin) / 2);
            switch (myPattern) {
                case Squadron.SquadronPattern.straight:
                    rigidbody.velocity = new Vector3(0, -maxSpeed, 0);
                    break;
                case Squadron.SquadronPattern.curve:
                    rigidbody.velocity = new Vector3(rigidbody.velocity.x, -maxSpeed, 0);
                    rigidbody.AddForce(new Vector3(myDirection, 0, 0) * curveThrust);
                    break;
                case Squadron.SquadronPattern.zigzag:
                    rigidbody.velocity = new Vector3((maxSpeed / 2) * myDirection, -maxSpeed, 0);
                    if (!switchedDirection & transform.position.y <= midPoint){
                        myDirection *= -1;
                        switchedDirection = true;
                    }
                    break;
                default:
                    break;
            }
        }
        if (transform.position.y <= MyBounds.yMin - keepMyTrail)
            Destroy(this.gameObject);
    }

    
    void BumpOver() {
        bumping = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (!stunned) {
            rigidbody.AddForce(collision.contacts[0].normal * bumpForce, ForceMode.Impulse);
            bumping = true;
            bumpTimer.Go(bumpTime);
            GameObject myBumpEffect = (GameObject)Instantiate(bumpEffect, transform.position, Quaternion.identity);
            Destroy(myBumpEffect, 1f);
        }
        if (stunned && collision.gameObject.CompareTag("Fodder")) {
            if (collision.gameObject.CompareTag("Fodder"))
                collision.gameObject.GetComponent<EnemyHealthManager>().SubtractHP(bumpDamage);
            GameObject myBumpEffect = (GameObject)Instantiate(bumpEffect, transform.position, Quaternion.identity);
            Destroy(myBumpEffect, 1f);
        }
    }

    //IStunnable
    public void Stun() {
        stunned = true;
        if (stunTimer.Running)
            stunTimer.Reset();
        else
            stunTimer.Go(stunTime);
    }

    void StunOff() {
        stunned = false;
    }


}
