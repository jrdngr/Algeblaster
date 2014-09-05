using UnityEngine;
using System.Collections;

#pragma warning disable 0414

public class OrbiterSpawningTest : MonoBehaviour {

    public bool oneBuddy;
    public float groupOffset;
    public bool noFriends;

    private bool enemyActive = false;
    private Vector3 spawnPos = new Vector3(0, 12, 0);
    private GameObject myBuddy;
    private GameObject[] myGroup = new GameObject[3];
    private GameObject buddyPrefab;

    void Start() {
        buddyPrefab = (GameObject)Resources.Load("Enemies/Fodder/Orbiter");
    }

    void Update() {
        if ( oneBuddy && myBuddy == null)
            SpawnEnemy();
        if (!oneBuddy) {
            bool enemyAlive = false;
            for (int i = 0; i < 3; i++) {
                if (myGroup[i] != null)
                    enemyAlive = true;
            }
            if (!enemyAlive)
                SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        if (!noFriends) {
            if (oneBuddy)
                myBuddy = (GameObject)Instantiate(buddyPrefab, spawnPos, Quaternion.identity);
            else {
                myGroup[0] = (GameObject)Instantiate(buddyPrefab, spawnPos, Quaternion.identity);
                spawnPos.x = -groupOffset;
                myGroup[1] = (GameObject)Instantiate(buddyPrefab, spawnPos, Quaternion.identity);
                spawnPos.x = groupOffset;
                myGroup[2] = (GameObject)Instantiate(buddyPrefab, spawnPos, Quaternion.identity);
                spawnPos.x = 0;
            }
        }
    }

}
