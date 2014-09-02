using UnityEngine;
using System.Collections;

public class OrbMove : MonoBehaviour {

    private float thrustForce;
    private float maxSpeed;
    private float bumpForce;
    private float bumpTime;
    private bool bumping = false;
    private Timer bumpTimer;
    private Vector3 forceVector;
    private GameObject playerShip;
    private PlayerManager gameManager;
    private Orbiter myVars;

    void Start() {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        myVars = GetComponent<Orbiter>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        thrustForce = myVars.ThrustForce;
        maxSpeed = myVars.MaxSpeed;
        bumpForce = gameManager.BumpForce;
        bumpTime = gameManager.BumpTime;
        bumpTimer = gameObject.AddComponent<Timer>();
        bumpTimer.Trigger += BumpOver;
    }

    void FixedUpdate() {
        if (!gameManager.GetComponent<EventManager>().playerDead) {
            forceVector = playerShip.transform.position - transform.position;
            transform.LookAt(playerShip.transform);
        }
        forceVector /= forceVector.magnitude;
        rigidbody.AddForce(forceVector * thrustForce);
        if (!bumping)
            rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed), 0);
    }

    void BumpOver() {
        bumping = false;
    }

    void OnCollisionEnter(Collision collision) {
        rigidbody.AddForce(collision.contacts[0].normal * bumpForce, ForceMode.Impulse);
        bumping = true;
        bumpTimer.Go(bumpTime);
    }

}
