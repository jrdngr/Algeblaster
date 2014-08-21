using UnityEngine;
using System.Collections;

public class ChooChoo : MonoBehaviour {

    [SerializeField] private float moveAngle;
    [SerializeField] private float thrustForce;
    [SerializeField] private float maxSpeed;
    [SerializeField] private GameObject deathEffect;

    public float MoveAngle {
        get {
            return moveAngle;
        }
        set {
            moveAngle = value;
        }
    }
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

    void Awake() {
    }

}
