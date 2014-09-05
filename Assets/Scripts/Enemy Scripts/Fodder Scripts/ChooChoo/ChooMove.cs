using UnityEngine;
using System.Collections;

public class ChooMove : MonoBehaviour, IStunnable {
    
    private const float pi = Mathf.PI;
    private const float stunTime = 0.1f;

    private float thrustForce;
    private float maxSpeed;
    private float moveAngle;
    private bool stunned = false;
    private Vector3 myDirection;
    private ChooChoo myVars;
    private LevelManager levelManager;
    private Timer stunTimer;


    public Rect MyBounds { get; set; }

    void Start() {
        levelManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<LevelManager>();
        myVars = GetComponent<ChooChoo>();
        MyBounds = levelManager.bounds;
        thrustForce = myVars.ThrustForce;
        maxSpeed = myVars.MaxSpeed;
        moveAngle = myVars.MoveAngle * pi / 180f;
        stunTimer = gameObject.AddComponent<Timer>();
        stunTimer.Trigger += StunOff;
        Reorient();
    }

    void FixedUpdate() {
        if (!stunned) {
            if (rigidbody.velocity != myDirection * maxSpeed) {
                rigidbody.AddForce(myDirection * thrustForce);
            }
            rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed), 0);
            CheckBounds();
        }
    }

    void CheckBounds() {
        float xVelocity = rigidbody.velocity.x;
        float yVelocity = rigidbody.velocity.y;
        bool changed = false;
        if (transform.position.x < MyBounds.xMin && xVelocity < 0) {
            xVelocity *= -1;
            moveAngle += pi;
            if (moveAngle > pi / 2 && moveAngle < pi)
                moveAngle += pi / 2;
            else if (moveAngle > pi && moveAngle < 3 * pi / 2)
                moveAngle -= pi / 2;             
            changed = true;
        }
        if (transform.position.x > MyBounds.xMax && xVelocity > 0) {
            xVelocity *= -1;
            moveAngle += pi;
            if (moveAngle > 0 && moveAngle < pi / 2)
                moveAngle -= pi / 2;
            else if (moveAngle > 3 * pi / 2 && moveAngle < 2*pi)
                moveAngle += pi / 2;             
            changed = true;
        }
        if (transform.position.y < MyBounds.yMin && yVelocity < 0) {
            yVelocity *= -1;
            moveAngle += pi;
            if (moveAngle > 3 * pi / 2 && moveAngle < 2 * pi)
                moveAngle -= pi / 2;
            else if (moveAngle > pi && moveAngle < 3 * pi / 2)
                moveAngle += pi / 2;
            changed = true;
        }
        if (transform.position.y > MyBounds.yMax && yVelocity > 0) {
            yVelocity *= -1;
            moveAngle += pi;
            if (moveAngle > 0 && moveAngle < pi / 2)
                moveAngle += pi / 2;
            else if (moveAngle > pi / 2 && moveAngle < pi)
                moveAngle -= pi / 2;
            changed = true;
        }
        if (changed) {
            rigidbody.velocity = new Vector3(xVelocity, yVelocity, 0);
            Reorient();
        }
    }

    void Reorient() {
        while (moveAngle >= 2 * pi) {
            moveAngle -= 2 * pi;
        }
        while (moveAngle < 0) {
            moveAngle += 2 * pi;
        }
        myDirection = new Vector3(Mathf.Cos(moveAngle), Mathf.Sin(moveAngle), 0);
        transform.rotation = Quaternion.LookRotation(myDirection);
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
