using UnityEngine;
using System.Collections;

// Manages the player's shield
public class pShieldMgr : MonoBehaviour {

	[SerializeField] private float shieldDrainSpeed = 3f;
	[SerializeField] private float delayThreshold = 25;
	[SerializeField] private float implosionForce;
	[SerializeField] private float implosionRadius;
	[SerializeField] private GameObject shield;

    private bool shieldOn = false;
	private bool shieldDelay = false;

    public bool ShieldOn {
        get {
            return shieldOn;
        }
        set {
            shieldOn = value;
        }
    }
    public float ShieldDrainSpeed {
        get {
            return shieldDrainSpeed;
        }
    }

	void Start(){
		shield.SetActive(false);
	}

	void Update(){
		CheckInputs();
	}

	void FixedUpdate(){
		ManageShield();
		if (GetComponent<pMoveMgr>().CurrentJuice > delayThreshold)
			shieldDelay = false;
		else if(!shieldOn)
			shieldDelay = true;
	}

	// Checks for player inputs
	// Turns on the shield
	// Makes the player suck up health and exp orbs
	void CheckInputs(){
		if (Input.GetButton ("Shield") && GetComponent<pMoveMgr>().CurrentJuice > 0  && !shieldDelay){
			shieldOn = true;
			Collider[] colliders = Physics.OverlapSphere(transform.position, implosionRadius);
			foreach (Collider c in colliders){
				if (c.rigidbody && (c.rigidbody.CompareTag("HealthOrb") || c.rigidbody.CompareTag("ExpOrb"))){
					c.rigidbody.AddExplosionForce(-implosionForce, transform.position, implosionRadius, 0, ForceMode.Force);
				}
			}
		}
		else 
			shieldOn = false;
	}

	void ManageShield(){
		shield.SetActive(shieldOn);
	}
}
