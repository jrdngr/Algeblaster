using UnityEngine;
using System.Collections;

public class MinPFPowerupMgr : Minion {

	[SerializeField] private float explosionRadius = 1f;
	[SerializeField] private float explosionForce = 1000f;
	[SerializeField] private float spawnDistanceFromCenter = 0.5f;
	[SerializeField] private int minHealth = 0;
	[SerializeField] private int maxHealth = 4;
	[SerializeField] private int minExp = 2;
	[SerializeField] private int maxExp = 20;
    [SerializeField] private GameObject healthOrbPrefab;
    [SerializeField] private GameObject expOrbPrefab;

	public void SpawnOrbs(Vector3 pos, int modifier){
		//Health
		int numberOfHealthOrbs = Random.Range (minHealth,maxHealth) * modifier;
		for (int i = 0; i < numberOfHealthOrbs; i++){
			float hoX = Random.Range (-spawnDistanceFromCenter, spawnDistanceFromCenter);
			float hoY = Random.Range (-spawnDistanceFromCenter, spawnDistanceFromCenter);
			Instantiate(healthOrbPrefab, new Vector3(pos.x + hoX, pos.y + hoY, 0), Quaternion.identity);
		}
		Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);
		foreach (Collider c in colliders){
			if (c.rigidbody && (c.rigidbody.CompareTag("HealthOrb"))){
				c.rigidbody.AddExplosionForce(explosionForce, pos, explosionRadius, 0, ForceMode.Force);
			}
		}
	}
}
