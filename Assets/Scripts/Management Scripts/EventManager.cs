using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void EventHandler();
	public static event EventHandler KilledMinion;
	public static event EventHandler CreatedMinion;

	public bool playerDead = false;

    //These two minion methods are called by the level script for a given level.  
    //They will probably be useless once levels are handbuilt but it's good for the test levels
	public static void TriggerCreatedMinion(){
		if (CreatedMinion != null)
			CreatedMinion();
	}

	public static void TriggerKilledMinion(){
		if (KilledMinion != null)
			KilledMinion();
	}

	void Update(){
		if (playerDead)
			StartCoroutine("Reset");
	}

	// Called when the player dies.  Loads up Controls scene
	IEnumerator Reset(){
		yield return new WaitForSeconds(1f);
        Application.LoadLevel("OrbiterSpawningTest");
	}

}
