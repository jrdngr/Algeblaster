using UnityEngine;
using System.Collections;

public class DisintegratorRocket : Projectile {

    private float blastRadius;
    public float BlastRadius {
        get { return blastRadius; }
        set { blastRadius = value; }
    }
    private float acceleration;
    public float Acceleration {
        get { return acceleration; }
        set { acceleration = value; }
    }
    private bool homing;
    public bool Homing {
        get { return homing; }
        set { homing = value; }
    }

    void Start() {
        rigidbody.velocity = myVelocity;
    }

    protected override void FixedUpdate() {
        if (!homing) {
            rigidbody.AddForce(new Vector3(0, acceleration, 0));
            if (transform.position.y > myBounds.yMax + 20)
                Destroy(this.gameObject);
        }
    }

}
