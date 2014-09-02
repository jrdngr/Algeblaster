using UnityEngine;
using System.Collections;

// Handles player movement, dodging, juice, and juice status bar
public class oldpMoveMgr : MonoBehaviour {

	[SerializeField] private float maxSpeedX = 8f;
	[SerializeField] private float maxSpeedY = 5f;
	[SerializeField] private float thrustForce = 5f;
    [SerializeField] private float bumpForce = 500f;
	[SerializeField] private int maxJuice = 100;
    [SerializeField] private int bumpDamage;
	
	[SerializeField] private float xMin = -8f;
	[SerializeField] private float xMax = 7f;
	[SerializeField] private float yMin = 0;
	[SerializeField] private float yMax = 2.5f;
	[SerializeField] private float dashSpeed = 500f;
	[SerializeField] private float nextDashDelay = 1f;
	[SerializeField] private float defaultDashTime = 0.2f;
	[SerializeField] private float juiceRefillSpeed = 0.2f;

	[SerializeField] private GameObject leftDash;
	[SerializeField] private GameObject rightDash;
	[SerializeField] private GameObject dashEffect;
    [SerializeField] private GameObject bumpEffect;
    [SerializeField] private StatusBar juiceBar;


    private int currentJuice;
    private float dashTime;
	private float lastDashTime = 0f;
	private float lastDashFrame = 0f;
	private float lastJuiceTick = 0f;
	private int dashDirection = 0;
	private bool dashing = false;

    public int CurrentJuice {
        get {
            return currentJuice;
        }
        set {
            currentJuice = value;
        }
    }
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

	void OnDrawGizmosSelected(){
		Vector3 center = new Vector3((xMax + xMin)/2, (yMax + yMin)/2, 0);
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(center, new Vector3(xMax-xMin, yMax-yMin, 0));
	}

	void Start(){
		dashTime = defaultDashTime;
		currentJuice = maxJuice;
	}

	void Update(){
		CheckInputs();
	}

	void FixedUpdate(){
		Move ();
		ManageJuice();
		rigidbody.velocity = new Vector3(Mathf.Clamp (rigidbody.velocity.x, -maxSpeedX, maxSpeedX), Mathf.Clamp (rigidbody.velocity.y, -maxSpeedY, maxSpeedY),0);
		transform.position = new Vector3(Mathf.Clamp (transform.position.x, xMin, xMax), Mathf.Clamp (transform.position.y, yMin, yMax),0);
	}

    void OnCollisionEnter(Collision collision) {
        if (collision.contacts[0].thisCollider.name != "Shield") {
            if ((collision.gameObject.CompareTag("Minion") || collision.gameObject.CompareTag("Mothership") || collision.gameObject.CompareTag("Fodder")) && !dashing) {
                GameObject bump = (GameObject)Instantiate(bumpEffect, transform.position, Quaternion.identity);
                rigidbody.AddForce(collision.contacts[0].normal * bumpForce);
                Destroy(bump, 2f);
                GetComponent<oldpHealthMgr>().Hit(bumpDamage);
                if (collision.gameObject.CompareTag("Fodder"))
                    collision.gameObject.GetComponent<EnemyHealthManager>().SubtractHP(bumpDamage);
            }
        }
    }

	// Moves the ship and locks it to the screen area
	void Move(){
		float moveX = Input.GetAxis ("Horizontal");
		float moveY = Input.GetAxis("Vertical");
		rigidbody.AddForce (new Vector3(moveX * thrustForce, moveY * thrustForce,0));

	}


	// Checks for any player inputs and responds accordingly
	void CheckInputs(){
		if (Input.GetButtonDown ("DashLeft") && !dashing && (Time.timeSinceLevelLoad - lastDashTime > nextDashDelay)) {
			dashing = true;
			lastDashTime = Time.timeSinceLevelLoad;
			lastDashFrame = Time.timeSinceLevelLoad;
			dashDirection = 0;
			GameObject dash = (GameObject)Instantiate(dashEffect, leftDash.transform.position, Quaternion.Euler (new Vector3(0,90,90)));
			Destroy (dash, 0.5f);
		}
		if (Input.GetButtonDown ("DashRight") && !dashing && (Time.timeSinceLevelLoad - lastDashTime > nextDashDelay)) {
			dashing = true;
			lastDashTime = Time.timeSinceLevelLoad;
			lastDashFrame = Time.timeSinceLevelLoad;
			dashDirection = 1;
			GameObject dash = (GameObject)Instantiate(dashEffect, rightDash.transform.position, Quaternion.Euler (new Vector3(0,270,90)));
			Destroy (dash, 0.5f);
		}
		if (dashing){
			if (Time.timeSinceLevelLoad - lastDashFrame > dashTime)
				dashing = false;
			Dash(dashDirection);
		}
	}

	// Dash in the specified direction.
	void Dash(int direction){
		if (direction == 0){
			rigidbody.AddForce (new Vector3(-dashSpeed, 0, 0));
		}
		if (direction == 1){
			rigidbody.AddForce (new Vector3(dashSpeed, 0, 0));
		}
	}

	void ManageJuice(){
		if (currentJuice < maxJuice && Time.timeSinceLevelLoad - lastJuiceTick > juiceRefillSpeed 
		    && !GetComponent<oldpShieldMgr>().ShieldOn){
			currentJuice++;
			lastJuiceTick = Time.timeSinceLevelLoad;
		}
		if (GetComponent<oldpShieldMgr>().ShieldOn && Time.timeSinceLevelLoad - lastJuiceTick > juiceRefillSpeed/GetComponent<oldpShieldMgr>().ShieldDrainSpeed){
			currentJuice--;
			lastJuiceTick = Time.timeSinceLevelLoad;
		}
		currentJuice = Mathf.Clamp (currentJuice, 0, maxJuice);
        juiceBar.SetFillPercentage((float)currentJuice / maxJuice);
	}
}
