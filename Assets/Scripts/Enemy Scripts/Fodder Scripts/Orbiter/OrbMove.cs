using UnityEngine;
using System.Collections;

public class OrbMove : MonoBehaviour, IStunnable {

    private const float stunTime = 0.1f;

    private float thrustForce;
    private float maxSpeed;
    private float bumpForce;
    private float bumpTime;
    private int bumpDamage;
    private bool bumping = false;
    private bool stunned = false;
    private Timer bumpTimer;
    private Timer stunTimer;
    private Vector3 forceVector;
    private GameObject playerShip;
    private GameObject bumpEffect;
    private PlayerManager gameManager;
    private Orbiter myVars;

    void Start() {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        myVars = GetComponent<Orbiter>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        bumpDamage = gameManager.GetComponent<WeaponManager>().FactorBeamTractorBumpDamage;
        bumpEffect = gameManager.BumpEffect;
        thrustForce = myVars.ThrustForce;
        maxSpeed = myVars.MaxSpeed;
        bumpForce = gameManager.BumpForce;
        bumpTime = gameManager.BumpTime;
        bumpTimer = gameObject.AddComponent<Timer>();
        bumpTimer.Trigger += BumpOver;
        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Trigger += StunOff;
    }

    void FixedUpdate() {
        if (!stunned) {
            if (!gameManager.GetComponent<EventManager>().playerDead) {
                forceVector = playerShip.transform.position - transform.position;
                transform.LookAt(playerShip.transform);
            }
            forceVector /= forceVector.magnitude;
            GetComponent<Rigidbody>().AddForce(forceVector * thrustForce);
            if (!bumping)
                GetComponent<Rigidbody>().velocity = new Vector3(Mathf.Clamp(GetComponent<Rigidbody>().velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(GetComponent<Rigidbody>().velocity.y, -maxSpeed, maxSpeed), 0);
        }
    }

    void BumpOver() {
        bumping = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (!stunned) {
            GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * bumpForce, ForceMode.Impulse);
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
