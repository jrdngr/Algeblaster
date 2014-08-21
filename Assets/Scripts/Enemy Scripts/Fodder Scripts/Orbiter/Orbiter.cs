using UnityEngine;
using System.Collections;

public class Orbiter : MonoBehaviour {

    [SerializeField] private float thrustForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] GameObject deathEffect;

    public float ThrustForce {
        get {
            return thrustForce;
        }
        set {
            thrustForce = value;
        }
    }
    public float MaxSpeed {
        get {
            return maxSpeed;
        }
        set {
            maxSpeed = value;
        }
    }
    public GameObject DeathEffect {
        get {
            return deathEffect;
        }
    }

}
