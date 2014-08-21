using UnityEngine;
using System.Collections;

#pragma warning disable 414

// Handles the zap spawned by the Multizapper Sensor when it detects and enemy
public class Zap : MonoBehaviour {

	[SerializeField] private int damage = 50;
	[SerializeField] private float speed = 30f;

	private float startTime;
	private float distance;
    private Vector3 startPos;
    private Vector3 target;

    public Vector3 StartPos {
        set {
            startPos = value;
        }
    }
    public Vector3 Target {
        set {
            target = value;
        }
    }

	void Start(){
		startTime = Time.time;
		distance = Vector3.Distance (startPos, target);
        Destroy(this.gameObject, 2);
	}

	void Update(){
		float distanceCovered = (Time.time - startTime) * speed;
		float lerpFactor = distanceCovered/distance;
		transform.position = Vector3.Lerp (startPos, target, lerpFactor);
	}

}
