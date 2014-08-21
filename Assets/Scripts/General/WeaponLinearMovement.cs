using UnityEngine;
using System.Collections;

//Makes bullets move up or down, then kills them after a distance or y coordinate.
//y coordinate is disabled if the projectile moves down.  Yeah that's lame.  I'll fix it
public class WeaponLinearMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private bool movesUp = true;
    [SerializeField] private float distanceBeforeDestroyed;
    [SerializeField] private float yMax;

    private float startPos;
    private float distance;

    public float Speed {
        get {
            return speed;
        }
        set {
            speed = value;
        }
    }


    void Awake() {
        if (!movesUp)
            speed *= -1;
        startPos = transform.position.y;
        rigidbody.velocity = new Vector3(0, speed, 0);
    }

    void FixedUpdate() {
        if (rigidbody.velocity.y != speed)
            rigidbody.velocity = new Vector3(0, speed, 0);
        if (distance >= distanceBeforeDestroyed)
            Destroy(this.gameObject);
        if  (transform.position.y >= yMax && movesUp)
            Destroy(this.gameObject);
    }

    void Update() {
        distance = Mathf.Abs(transform.position.y - startPos);
    }

}
