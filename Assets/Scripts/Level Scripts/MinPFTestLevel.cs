using UnityEngine;
using System.Collections;

#pragma warning disable 414

public class MinPFTestLevel : MonoBehaviour {

	[SerializeField] private float spawnDelay = 1f;
	[SerializeField] private int maxPFMinions = 1;
	[SerializeField] private GameObject pfMinion;

	private int numberOfPFMinions = 0;
	private float pfXMin;
	private float pfXMax;
	private float pfYMin;
	private float pfYMax;

	void Start(){
		EventManager.CreatedMinion += IncrementMinionCount;
		EventManager.KilledMinion += DecrementMinionCount;

		pfXMin = pfMinion.GetComponent<MinPFMoveMgr>().XMin;
		pfXMax = pfMinion.GetComponent<MinPFMoveMgr>().XMax;
		pfYMin = pfMinion.GetComponent<MinPFMoveMgr>().YMin;
		pfYMax = pfMinion.GetComponent<MinPFMoveMgr>().YMax;
	}

	void OnDisable(){
		EventManager.CreatedMinion -= IncrementMinionCount;
		EventManager.KilledMinion -= DecrementMinionCount;
	}

	void Update(){
		ManageLevel ();
	}

	// Spawns Prime Factor Minions if there are fewer than the max in play
	void ManageLevel(){
		if (numberOfPFMinions < maxPFMinions)
			SpawnPFMinion();
	}

	// Spawns a prime factor minion.
	void SpawnPFMinion(){
		float x = Random.Range (pfXMin, pfXMax);
		float y = Random.Range ((pfYMin + pfYMax)/2, pfYMax);
		Instantiate(pfMinion, new Vector3(x, y, 0), Quaternion.Euler (new Vector3(90,0,0)));
		numberOfPFMinions++;
	}

	void IncrementMinionCount(){
		numberOfPFMinions++;
	}
	
	void DecrementMinionCount(){
		numberOfPFMinions--;
	}

}
