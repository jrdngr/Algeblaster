using UnityEngine;
using System.Collections;

// Handles minion movement
public class MinPFMoveMgr : Minion {

	[SerializeField] private float xMin;
	[SerializeField] private float xMax;
	[SerializeField] private float yMin;
	[SerializeField] private float yMax;
	[SerializeField] private float newXDirectionTime;
    [SerializeField] private float newYDirectionTime;
	[SerializeField] private float thrustForce;

    private float maxSpeed = 5f;
	private int currentDirectionX = 1;
	private int currentDirectionY = 1;
    private bool seesPlayer = false;
    private bool seesBullet = false;
    private bool seesOtherShip = false;
    private Timer newXDirTimer;
    private Timer newYDirTimer;

    public float XMin {
        get {
            return xMin;
        }
    }
    public float XMax {
        get {
            return xMax;
        }
    }
    public float YMin {
        get {
            return yMin;
        }
    }
    public float YMax {
        get {
            return yMax;
        }
    }
    public float MaxSpeed {
        set {
            maxSpeed = value;
        }
    }
    public bool SeesPlayer {
        get {
            return seesPlayer;
        }
        set {
            seesPlayer = value;
        }
    }
    public bool SeesBullet {
        get {
            return seesBullet;
        }
        set {
            seesBullet = value;
        }
    }
    public bool SeesOtherShip {
        get {
            return seesOtherShip;
        }
        set {
            seesOtherShip = value;
        }
    }

    void Start() {
        newXDirTimer = gameObject.AddComponent<Timer>();
        newXDirTimer.Trigger += NewXDirection;
        newXDirTimer.Go(newXDirectionTime);
        newYDirTimer = gameObject.AddComponent<Timer>();
        newYDirTimer.Trigger += NewYDirection;
        newYDirTimer.Go(newYDirectionTime);
    }

	// Draws a box around the minion's movement range in the editor window
	void OnDrawGizmosSelected(){
		Vector3 center = new Vector3((xMax + xMin)/2, (yMax + yMin)/2, 0);
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(center, new Vector3(xMax-xMin, yMax-yMin, 0));
	}

	void FixedUpdate () {
		if (!seesPlayer && !seesOtherShip)
			Move ();
		transform.position = new Vector3(Mathf.Clamp (transform.position.x, xMin-1, xMax+1), Mathf.Clamp (transform.position.y, yMin-1, yMax+1),0);
		rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rigidbody.velocity.y, -maxSpeed/2, maxSpeed/2), 0);
	}

    void NewXDirection() {
        currentDirectionX = Random.Range(0, 2);
        if (currentDirectionX == 0)
            currentDirectionX = -1;
        newXDirTimer.Go(newXDirectionTime);
    }

    void NewYDirection() {
        currentDirectionY = Random.Range(0, 2);
        if (currentDirectionY == 0)
            currentDirectionY = -1;
        newYDirTimer.Go(newYDirectionTime);
    }

	// Move this minion
	void Move(){
		if (transform.position.x < xMin){
			currentDirectionX = 1;
            newXDirTimer.Reset();
		}
		if (transform.position.x > xMax){
			currentDirectionX = -1;
            newXDirTimer.Reset();
		}
		if (transform.position.y < yMin){
			currentDirectionY = 1;
            newYDirTimer.Reset();
		}
		if (transform.position.y > yMax){
			currentDirectionY = -1;
            newYDirTimer.Reset();
		}
		rigidbody.AddForce(new Vector3(currentDirectionX * thrustForce, currentDirectionY * thrustForce, 0));
	}

	// Attempts to move away from a given object.  Used for bullets and other ships.
	public void MoveAway(float bulletX, float bulletY){
		int moveX;
		if (bulletX > transform.position.x)
			moveX = -1;
		else
			moveX = 1;
		if (bulletX < transform.position.x + .1f && bulletX > transform.position.x - .1f)
			moveX = 0;
		rigidbody.AddForce(new Vector3(moveX * thrustForce, currentDirectionY * thrustForce,0));
	}
	
	// Attempts to follow the player.  Called from player sensor
	public void FollowPlayer(float playerX, float playerY){
		int moveX;
		if (playerX > transform.position.x)
			moveX = 1;
		else
			moveX = -1;
		if (playerX < transform.position.x + .1f && playerX > transform.position.x - .1f)
			moveX = 0;
		rigidbody.AddForce(new Vector3(moveX * thrustForce, currentDirectionY * thrustForce,0));
	}

}
