using UnityEngine;
using System.Collections;

public class FodderPowerupMgr : MonoBehaviour {

    [SerializeField] private float explosionRadius = 1f;
    [SerializeField] private float explosionForce = 1000f;
    [SerializeField] private float spawnDistanceFromCenter = 0.5f;
    [SerializeField] private int minHealth = 0;
    [SerializeField] private int maxHealth = 4;
    
    private GameObject healthOrbPrefab;

    void Awake() {
        healthOrbPrefab = (GameObject)Resources.Load("Pickups/HealthOrb");
    }

    public void SpawnOrbs(Vector3 pos, int modifier) {
        //Health
        int numberOfHealthOrbs = Random.Range(minHealth, maxHealth) * modifier;
        for (int i = 0; i < numberOfHealthOrbs; i++) {
            float hoX = Random.Range(-spawnDistanceFromCenter, spawnDistanceFromCenter);
            float hoY = Random.Range(-spawnDistanceFromCenter, spawnDistanceFromCenter);
            Instantiate(healthOrbPrefab, new Vector3(pos.x + hoX, pos.y + hoY, 0), Quaternion.identity);
        }

        Collider[] colliders = Physics.OverlapSphere(pos, explosionRadius);
        foreach (Collider c in colliders) {
            if (c.GetComponent<Rigidbody>() && c.GetComponent<Rigidbody>().CompareTag("HealthOrb")) {
                c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, pos, explosionRadius, 0, ForceMode.Force);
            }
        }
    }

}
