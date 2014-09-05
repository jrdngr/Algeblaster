using UnityEngine;
using System.Collections;

public class DisintegratorRocket : Projectile {

    private const float keepMyTrail = 20f;
    private const float checkForTargetDelay = 0.1f;
    private const int numberOfAcceptableMisses = 2;
    private const float angerDampingBoost = 4f;
    private const int homingRocketBoostRange = 10;
    private const float homingBrakeDistance = 5f;
    private const float minimumBrakeSpeed = 5;
    private const float driftAcceleration = 20;
    private const float lateralThrustCorrection = 4f;
    private const float angerTime = 3f;

    private float blastRadius;
    private float acceleration;
    private bool homing;
    private float homingRadius;
    private float homingAcceleration;
    private float rotationDamping;

    private bool angry = false;
    private bool canCheckForTarget = true;
    private Timer newTargetTimer;
    private Timer angerTimer;
    private Transform myTarget = null;
    private GameObject myBooster;
    private GameObject myAnger;

    void Start() {
        myBooster = transform.Find("booster").gameObject;
        myBooster.SetActive(false);
        myAnger = transform.Find("Rocket Anger!").gameObject;
        myAnger.SetActive(false);
        rigidbody.velocity = myVelocity;
        newTargetTimer = gameObject.AddComponent<Timer>();
        newTargetTimer.Trigger += ResetTargetCheck;
        angerTimer = gameObject.AddComponent<Timer>();
        angerTimer.Trigger += HulkSmash;
        angerTimer.Go(angerTime);
    }

    protected override void FixedUpdate() {

        Vector3 annoying = transform.position;
        annoying.z = 0;
        transform.position = annoying;
        
        //Find a target
        if (homing && canCheckForTarget && myTarget == null)
            FindTarget();
        //Apply thrust if the rocket is dumb or has no target
        if (!homing || (homing && myTarget == null)) {
            rigidbody.AddForce(new Vector3(0, acceleration, 0));
            if (transform.position.y > myBounds.yMax + keepMyTrail)
                Destroy(this.gameObject);
            //Rotate to face forward
            var rotation = Quaternion.LookRotation(new Vector3(0, 1, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
        }

        //Apply thrust to a homing rocket
        if (homing && myTarget != null) {
            
            //Find unit vector pointing to target
            Vector3 targetVector = Vector3.Normalize(myTarget.transform.position - transform.position);

            //If the rocket is pointed in the direction of the target within a certain range, apply thrust
            //If enough time has elapsed, add corrections to make it more accurate
            if (Mathf.Abs(Vector3.Angle(transform.forward, targetVector)) <= homingRocketBoostRange) {
                rigidbody.AddForce(targetVector * homingAcceleration);
                myBooster.SetActive(true);
                if (angry)
                    rigidbody.AddForce(myTarget.rigidbody.velocity * lateralThrustCorrection);
            }
            //If the rocket is out of range of the target, quickly slow it down to its minimum speed until it's facing the target
            else if (Vector3.Distance(transform.position, myTarget.position) >= homingBrakeDistance && rigidbody.velocity.magnitude >= minimumBrakeSpeed && !angry) {
                rigidbody.AddForce(-Vector3.Normalize(rigidbody.velocity) * homingAcceleration);
                rigidbody.AddForce(transform.forward * driftAcceleration);
            }
            //If the rocket is out of range and at or below its minimum speed, apply forward thrust for driftu
            else if (Vector3.Distance(transform.position, myTarget.position) >= homingBrakeDistance && rigidbody.velocity.magnitude <= minimumBrakeSpeed && !angry) {
                rigidbody.AddForce(transform.forward * driftAcceleration);
            }
            else
                myBooster.SetActive(false);

            //If angry, come to a complete stop to reorient
            if (angry && (Mathf.Abs(Vector3.Angle(transform.forward, targetVector)) >= homingRocketBoostRange || rigidbody.velocity.magnitude >= minimumBrakeSpeed)) {
                rigidbody.AddForce(-Vector3.Normalize(rigidbody.velocity) * homingAcceleration);
            }

            //Rotate to face the target smoothly
            var rotation = Quaternion.LookRotation(myTarget.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationDamping);
            
            //Enforce bounds
            if (transform.position.y > myBounds.yMax + keepMyTrail || transform.position.y < myBounds.yMin - keepMyTrail ||
                transform.position.x > myBounds.xMax + keepMyTrail || transform.position.x < myBounds.xMin - keepMyTrail)
                Destroy(this.gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<EnemyHealthManager>() != null) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, transform.position, Quaternion.identity);
            Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
            foreach (Collider c in colliders) {
                if (c.rigidbody && c.rigidbody.CompareTag("Fodder")) {
                    WeaponHit blastHit = hit;
                    if ((int)Vector3.Distance(transform.position, c.transform.position) > 0)
                        blastHit.damage /= (int)Vector3.Distance(transform.position, c.transform.position);
                    if (blastHit.damage > hit.damage)
                        blastHit.damage = hit.damage;
                    c.gameObject.GetComponent<EnemyHealthManager>().Hit(blastHit);
                }
            }
            Destroy(myEffect, 5f);
            Destroy(this.gameObject);
        }
    }

    void FindTarget() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, homingRadius);
        foreach (Collider c in colliders) {
            if (c.rigidbody && (c.rigidbody.CompareTag("Fodder") || c.rigidbody.CompareTag("Minion"))) {
                myTarget = c.transform;
            }
        }
        canCheckForTarget = false;
        newTargetTimer.Go(checkForTargetDelay);
    }

    void ResetTargetCheck() {
        canCheckForTarget = true;
    }

    void HulkSmash() {
        angry = true;
        rotationDamping += angerDampingBoost;
        myAnger.SetActive(true);
        GetComponent<AudioSource>().Play();
    }

    public void SetRocketProperties(float accel, float blastRad, bool isHoming, float homingRad, float homingAccel, float rotDamping) {
        acceleration = accel;
        blastRadius = blastRad;
        homing = isHoming;
        homingRadius = homingRad;
        homingAcceleration = homingAccel;
        rotationDamping = rotDamping;
    }
}
