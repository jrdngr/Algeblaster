using UnityEngine;
using System.Collections;

//Controls an individual star.  Initialization is triggered by the StarField class
public class Star : MonoBehaviour {

	public enum StarColor {white, red, blue};

	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;
	private float scale;
	private Vector3 speed;
	private StarColor color;

	void Start(){
		transform.localScale = new Vector3(scale,scale,scale);
		switch (color){
		case StarColor.white:
			renderer.material.color = Color.white;
			break;
		case StarColor.blue:
			renderer.material.color = new Color(0.7f, 0.9f, 1f, 1f);
			break;
		case StarColor.red:
			renderer.material.color = new Color(1f, 0.7f, 0.7f, 1f);
			break;
		default:
			renderer.material.color = Color.white;
			break;
		}
	}

	void FixedUpdate(){
		transform.position = transform.position - speed;
		if (transform.position.y <= yMin){
			float newX = Random.Range (xMin, xMax);
			transform.position = new Vector3(newX, yMax, transform.position.z);
		}
	}

	public void Initialize(StarColor myColor, float myScale, Vector3 mySpeed, float minX, float maxX, float minY, float maxY){
		color = myColor;
		scale = myScale;
		speed = mySpeed;
		xMin = minX;
		xMax = maxX;
		yMin = minY;
		yMax = maxY;
	}

}
