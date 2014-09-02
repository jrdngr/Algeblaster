using UnityEngine;
using System.Collections;

// Manages player animations
public class playerAnimationManager : MonoBehaviour {

	private Animator anim;

	void Start(){
		anim = GetComponent<Animator> ();
	}

	void Update(){
		CheckDirection();
	}

	// If the player is holding a direction, this function sets the relevant parameter of the Animator
	// to true so that the Animator can decide whether to animate a right or left turn
	void CheckDirection (){
		if (Input.GetAxis("Horizontal") > 0 && !anim.GetBool("holdingLeft")){
			anim.SetBool ("holdingRight", true);
		}
		else if (Input.GetAxis("Horizontal") < 0 && !anim.GetBool ("holdingRight")){
			anim.SetBool("holdingLeft", true);
		}
		else {
			anim.SetBool ("holdingRight", false);
			anim.SetBool ("holdingLeft", false);
		}
	}
}
