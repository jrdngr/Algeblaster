using UnityEngine;
using System.Collections;

public class OrbMove : MonoBehaviour {

    private float thrustForce;
    private float maxSpeed;
    private float bumpForce;
    private Vector3 forceVector;
    private GameObject playerShip;
    private GameObject god;
    private Orbiter myVars;

    void Start() {
        playerShip = GameObject.FindGameObjectWithTag("Player");
        myVars = GetComponent<Orbiter>();
        god = GameObject.FindGameObjectWithTag("God");
        thrustForce = myVars.ThrustForce;
        maxSpeed = myVars.MaxSpeed;
        bumpForce = thrustForce * 50;
    }

    void FixedUpdate() {
        if (!god.GetComponent<EventManager>().playerDead) {
            forceVector = playerShip.transform.position - transform.position;
            transform.LookAt(playerShip.transform);
        }
        forceVector /= forceVector.magnitude;
        rigidbody.AddForce(forceVector * thrustForce);
        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed), 0);
    }

    void OnCollisionEnter(Collision collision) {
        rigidbody.AddForce(collision.contacts[0].normal * bumpForce);
    }

}
