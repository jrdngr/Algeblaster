using UnityEngine;
using System.Collections;

//Moves the whole mothership around
public class MSMove : MonoBehaviour {

    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    [SerializeField] private float xForce;
    [SerializeField] private float yForce;
    [SerializeField] private float maxSpeedX;
    [SerializeField] private float maxSpeedY;

    private int xDir;
    private int yDir;

    void Start() {
        xDir = Random.Range(0, 1);
        yDir = Random.Range(0, 1);
        if (xDir == 0)
            xDir = -1;
        if (yDir == 0)
            yDir = -1;
    }

    void OnDrawGizmosSelected() {
        Vector3 center = new Vector3((xMax + xMin) / 2, (yMax + yMin) / 2, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(xMax - xMin, yMax - yMin, 0));
    }

    void FixedUpdate() {
        Move();
    }

    void Move() {
        if (transform.position.x < xMin)
            xDir = 1;
        if (transform.position.x > xMax)
            xDir = -1;
        if (transform.position.y < yMin)
            yDir = 1;
        if (transform.position.y > yMax)
            yDir = -1;
        rigidbody.AddForce(new Vector3(xForce * xDir, yForce * yDir, 0));
        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeedX, maxSpeedX), Mathf.Clamp(rigidbody.velocity.y, -maxSpeedY, maxSpeedY), 0);
    }

    public void Center(float offset) {
        xMin += offset;
        xMax -= offset;
    }

}
