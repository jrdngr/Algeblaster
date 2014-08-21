using UnityEngine;
using System.Collections;

// Manages player weapon selection
// Dictates when a gun can fire
/// Instantiates projectiles when the player fires
public class pWeaponMgr : MonoBehaviour {

	[SerializeField] private float positronFireSpeed = 3f;
	[SerializeField] private float negatronFireSpeed = 1f;
	[SerializeField] private float rocketFireSpeed = 0.5f;
	[SerializeField] private float secondRocketDelay = 0.5f;
	[SerializeField] private float factorBeamFireSpeed = 10f;
	[SerializeField] private float weaponSwapDelay = 0.1f;
	[SerializeField] private int factorBeamJuiceCost = 10;

	[SerializeField] private GameObject leftGun;
	[SerializeField] private GameObject rightGun;
	[SerializeField] private GameObject centerGun;
	[SerializeField] private GameObject leftRocket;
	[SerializeField] private GameObject rightRocket;
	[SerializeField] private GameObject positronBullet;
	[SerializeField] private GameObject negatronWave;
	[SerializeField] private GameObject rocket;
	[SerializeField] private GameObject multiZapper;
	[SerializeField] private GameObject factorBeamBit;
	[SerializeField] private GameObject deadRocketLeft;
	[SerializeField] private GameObject deadRocketRight;
	[SerializeField] private GameObject gui;

	private float lastBullet = 0f;
	private float lastLeftRocket = 0f;
	private float lastRightRocket = 0f;
	private float lastWeaponSwap = 0f;
	private bool leftRocketReady = true;
	private bool rightRocketReady = true;
	private bool fireLeftRocket = true;
    private bool zapperOut = false;
	private int selectedWeapon = 1;

    public bool ZapperOut {
        set {
            zapperOut = value;
        }
    }

	delegate void FireGun();
	FireGun fireGun;

	void Update(){
		CheckInputs();
		if (Time.timeSinceLevelLoad - lastLeftRocket > 1/rocketFireSpeed){
			leftRocketReady = true;
			deadRocketLeft.gameObject.SetActive(true);
		}
		if (Time.timeSinceLevelLoad - lastRightRocket > 1/rocketFireSpeed){
			rightRocketReady = true;
			deadRocketRight.gameObject.SetActive(true);
		}

	}

	
	// Checks for any player inputs and responds accordingly
	void CheckInputs(){
		switch (selectedWeapon){
		case 1:
			fireGun = FirePos;
			break;
		case 2:
			fireGun = FireNeg;
			break;
		case 3:
			fireGun = FireMult;
			break;
		case 4:
			fireGun = FireDiv;
			break;
		case 5:
			fireGun = FireFac;
			break;
		default:
			Debug.LogError("Invalid weapon selection");
			break;
		}

		if (Input.GetButton ("Fire1"))
			fireGun();
		if (Input.GetButton ("Weapon1"))
			selectedWeapon = 1;
		if (Input.GetButton ("Weapon2"))
			selectedWeapon = 2;
		if (Input.GetButton ("Weapon3"))
			selectedWeapon = 3;
		if (Input.GetButton ("Weapon4"))
			selectedWeapon = 4;
		if (Input.GetButton ("Weapon5"))
			selectedWeapon = 5;
		if (Input.GetAxis ("WeaponSwap") > 0 && Time.timeSinceLevelLoad - lastWeaponSwap > weaponSwapDelay
		    && selectedWeapon < 5){
			selectedWeapon++;
			lastWeaponSwap = Time.timeSinceLevelLoad;
		}
		if (Input.GetAxis ("WeaponSwap") < 0 && Time.timeSinceLevelLoad - lastWeaponSwap > weaponSwapDelay
		    && selectedWeapon > 1){
			selectedWeapon--;
			lastWeaponSwap = Time.timeSinceLevelLoad;
		}
		gui.GetComponent<WeaponMgr>().ChangeWeapon(selectedWeapon);
		selectedWeapon = Mathf.Clamp(selectedWeapon, 1, 5);
	}

	 
    // Fires the Positron gun
	void FirePos(){
		if (Time.timeSinceLevelLoad - lastBullet > 1/positronFireSpeed){
			Instantiate (positronBullet, leftGun.transform.position, Quaternion.identity);
			Instantiate (positronBullet, rightGun.transform.position, Quaternion.identity);
			lastBullet = Time.timeSinceLevelLoad;
		}
	}

	// Fires the Negatron gun
	void FireNeg(){
		if (Time.timeSinceLevelLoad - lastBullet > 1/negatronFireSpeed){
			Instantiate (negatronWave, centerGun.transform.position, Quaternion.identity);
			lastBullet = Time.timeSinceLevelLoad;
		}
	}

	 
	// Fires the Multizapper
	void FireMult(){
		if (!zapperOut){
			GameObject zapper = (GameObject)Instantiate (multiZapper, centerGun.transform.position, Quaternion.identity);
            zapper.GetComponent<Multizapper>().WeaponManager = this;
            lastBullet = Time.timeSinceLevelLoad;
            zapperOut = true;
		}
	}

	 
	// Fires the Disintegrator Rocket
	void FireDiv(){
		if (leftRocketReady || rightRocketReady){
			if (fireLeftRocket && Time.timeSinceLevelLoad - lastBullet > secondRocketDelay){
				Instantiate(rocket, leftRocket.transform.position, Quaternion.Euler(0,45,0));
				deadRocketLeft.gameObject.SetActive(false);
				fireLeftRocket = false;
				leftRocketReady = false;
				lastLeftRocket = Time.timeSinceLevelLoad;
				lastBullet = Time.timeSinceLevelLoad;
			}
			else if (!fireLeftRocket && Time.timeSinceLevelLoad - lastBullet > secondRocketDelay){
				Instantiate(rocket, rightRocket.transform.position, Quaternion.Euler(0,45,0));
				deadRocketRight.gameObject.SetActive (false);
				fireLeftRocket = true;
				rightRocketReady = false;
				lastRightRocket = Time.timeSinceLevelLoad;
				lastBullet = Time.timeSinceLevelLoad;
			}
		}
	}

	 
	// Fires the Factor Beam
	void FireFac(){
		if (Time.timeSinceLevelLoad - lastBullet > 1/factorBeamFireSpeed && GetComponent<pMoveMgr>().CurrentJuice
		     >= factorBeamJuiceCost){
			GetComponent<pMoveMgr>().CurrentJuice -= factorBeamJuiceCost;
			Instantiate (factorBeamBit, centerGun.transform.position, Quaternion.identity);
			lastBullet = Time.timeSinceLevelLoad;
		}
	}
}
