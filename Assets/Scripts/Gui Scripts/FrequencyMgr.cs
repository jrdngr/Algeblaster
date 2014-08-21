using UnityEngine;
using System.Collections;

// Manages the frequency bar on the right side of the GUI
// Tells projectiles what their frequency is when they are created
public class FrequencyMgr: MonoBehaviour {

	private int weaponFrequency = 2;

	[SerializeField] private GameObject outlineSprite;
	[SerializeField] private GameObject two;
	[SerializeField] private GameObject three;
	[SerializeField] private GameObject four;
	[SerializeField] private GameObject five;
	[SerializeField] private GameObject six;
	[SerializeField] private GameObject seven;
	[SerializeField] private GameObject eight;
	[SerializeField] private GameObject nine;

    public int WeaponFrequency {
        get {
            return weaponFrequency;
        }
    }

	void Start(){
		MoveOutline();
	}

	void Update () {
		CheckInputs();
	}

	// Checks for player inputs regarding the frequency panel and moves the outline according to the
	// currently selected frequency
	void CheckInputs(){
		if (Input.GetButtonDown("ChangeFreqUp")){
			weaponFrequency++;
			MoveOutline();
		}

		if (Input.GetButtonDown("ChangeFreqDown")){
			weaponFrequency--;
			MoveOutline();
		}
		weaponFrequency = Mathf.Clamp(weaponFrequency, 2, 9);
	}

	// Moves the outline around the currently selected frequency
	void MoveOutline(){
		if (weaponFrequency == 9)
			outlineSprite.transform.position = nine.transform.position;
		else if (weaponFrequency == 8)
			outlineSprite.transform.position = eight.transform.position;
		else if (weaponFrequency == 7)
			outlineSprite.transform.position = seven.transform.position;
		else if (weaponFrequency == 6)
			outlineSprite.transform.position = six.transform.position;
		else if (weaponFrequency == 5)
			outlineSprite.transform.position = five.transform.position;
		else if (weaponFrequency == 4)
			outlineSprite.transform.position = four.transform.position;
		else if (weaponFrequency == 3)
			outlineSprite.transform.position = three.transform.position;
		else if (weaponFrequency == 2)
			outlineSprite.transform.position = two.transform.position;
	}

}
