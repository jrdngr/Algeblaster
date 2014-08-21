using UnityEngine;
using System.Collections;

// Controls the minion's moving eye
public class MinPFEyeMgr : Minion {

	[SerializeField] private float eyeSpeed = 2f;
	[SerializeField] private GameObject eyeLeft;
	[SerializeField] private GameObject eyeRight;
	[SerializeField] private GameObject eyeStopped;

    private bool moving = true;
    private bool movingRight = true;
	private float eyeDelay;
	private float startTime;
	private float length;
    private Timer switchTimer;

    public bool Moving {
        get {
            return moving;
        }
        set {
            moving = value;
        }
    }

	void Start () {
		startTime = Time.time;
		transform.position = eyeLeft.transform.position;
		length = Vector3.Distance (eyeLeft.transform.position, eyeRight.transform.position);
		eyeDelay = length/eyeSpeed;
        switchTimer = gameObject.AddComponent<Timer>();
        switchTimer.Trigger += Switch;
        switchTimer.Go(eyeDelay);
	}
	
	void Update () {
		if (moving)
			eyeStopped.SetActive(false);
			MoveEye ();
		if (!moving){
			eyeStopped.SetActive(true);
		}
	}

    void Switch() {
        movingRight = !movingRight;
        startTime = Time.time;
        switchTimer.Go(eyeDelay);
    }

	void MoveEye(){
		float distanceMoved = (Time.time - startTime) * eyeSpeed;
		float fracLength = distanceMoved / length;
		
		if (movingRight)
			transform.position = Vector3.Slerp(eyeLeft.transform.position, eyeRight.transform.position, fracLength);
		if (!movingRight)
			transform.position = Vector3.Slerp(eyeRight.transform.position, eyeLeft.transform.position, fracLength);
	}
}
