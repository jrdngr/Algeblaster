using UnityEngine;
using System.Collections;

//Creates the entire field of stars based on user-inputted settings
public class StarField : MonoBehaviour {

	[SerializeField] private float xMin;
	[SerializeField] private float xMax;
	[SerializeField] private float yMin;
	[SerializeField] private float yMax;
	[SerializeField] private int numberFarStars;
	[SerializeField] private float farSpeed;
	[SerializeField] private float farScale;
	[SerializeField] private int numberMidStars;
	[SerializeField] private float midSpeed;
	[SerializeField] private float midScale;
	[SerializeField] private int numberCloseStars;
	[SerializeField] private float closeSpeed;
	[SerializeField] private float closeScale;

	[SerializeField] private GameObject star;

	void Start(){
		//Create far range stars
		for (int i = 0; i <= numberFarStars; i++){
			Vector3 pos;
			Star.StarColor color;
			int newColor = Random.Range (0,5);
			pos.x = Random.Range (xMin, xMax);
			pos.y = Random.Range (yMin, yMax);
			pos.z = transform.position.z;
			if (newColor == 0)
				color = Star.StarColor.blue;
			else if (newColor == 1)
				color = Star.StarColor.red;
			else
				color = Star.StarColor.white;
			GameObject newStar = (GameObject)Instantiate(star, pos, Quaternion.identity);
			newStar.transform.parent = transform;
			newStar.GetComponent<Star>().Initialize(color, farScale, new Vector3 (0, farSpeed,0), xMin, xMax, yMin, yMax);
		}
		//Create mid range stars
		for (int i = 0; i <= numberMidStars; i++){
			Vector3 pos;
			Star.StarColor color;
			int newColor = Random.Range (0,10);
			pos.x = Random.Range (xMin, xMax);
			pos.y = Random.Range (yMin, yMax);
			pos.z = transform.position.z;
			if (newColor == 0)
				color = Star.StarColor.blue;
			else if (newColor == 1)
				color = Star.StarColor.red;
			else
				color = Star.StarColor.white;
			GameObject newStar = (GameObject)Instantiate(star, pos, Quaternion.identity);
			newStar.transform.parent = transform;
			newStar.GetComponent<Star>().Initialize(color, midScale, new Vector3 (0, midSpeed,0), xMin, xMax, yMin, yMax);
		}
		//Create close range stars
		for (int i = 0; i <= numberCloseStars; i++){
			Vector3 pos;
			Star.StarColor color;
			int newColor = Random.Range (0,20);
			pos.x = Random.Range (xMin, xMax);
			pos.y = Random.Range (yMin, yMax);
			pos.z = transform.position.z;
			if (newColor == 0)
				color = Star.StarColor.blue;
			else if (newColor == 1)
				color = Star.StarColor.red;
			else
				color = Star.StarColor.white;
			GameObject newStar = (GameObject)Instantiate(star, pos, Quaternion.identity);
			newStar.transform.parent = transform;
			newStar.GetComponent<Star>().Initialize(color, closeScale, new Vector3 (0, closeSpeed,0), xMin, xMax, yMin, yMax);
		}
	}
}
