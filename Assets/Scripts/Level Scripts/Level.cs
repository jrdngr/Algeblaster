using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Level : MonoBehaviour {

    public bool hasMothership;
    public string mothershipEquation;
    public int experienceReward;
    public List<Wave> waveList = new List<Wave>();

    private int currentWave = 0;
    private bool spawnedWave = false;
    private bool spawnedMothership = false;
    private List<GameObject> currentWaveEnemies = new List<GameObject>();

    private God god;
    private GameObject orbiterPrefab;
    private GameObject choochooPrefab;
    private GameObject pfmPrefab;
    private GameObject mothershipPrefab;

    void Start() {
        god = GameObject.FindGameObjectWithTag("God").GetComponent<God>();
        orbiterPrefab = (GameObject)Resources.Load("Enemies/Fodder/Orbiter");
        choochooPrefab = (GameObject)Resources.Load("Enemies/Fodder/ChooChoo");
        pfmPrefab = (GameObject)Resources.Load("Enemies/Minions/PrimeFactorMinion");
        mothershipPrefab = (GameObject)Resources.Load("Enemies/Mothership/Mothership");
    }

    void Update() {
		if (!spawnedWave && currentWave < waveList.Count) {
			for (int i = 0; i < waveList[currentWave].EnemyList.Count; i++) {
				SpawnEnemy(waveList[currentWave].EnemyList[i]);
			}
			spawnedWave = true;
		}
		if (spawnedWave) {
			for (int i = 0; i < currentWaveEnemies.Count; i++) {
				if (currentWaveEnemies[i] == null)
					currentWaveEnemies.RemoveAt(i);
			}
		}
		if (currentWaveEnemies.Count <= 0) {
			currentWave++;
			spawnedWave = false;
		}
        if (currentWave >= waveList.Count && hasMothership && !spawnedMothership) {
            GameObject ms = (GameObject)Instantiate(mothershipPrefab, new Vector3(0, god.bounds.yMax, 0), Quaternion.identity);
            ms.GetComponent<Mothership>().Equation = mothershipEquation;
            spawnedMothership = true;
        }
}


    /* format:  tsdddaaa
     * t: type of enemy -- (o)rbiter, (c)hoochoo, (p)fminion
     * s: side to spawn on -- (t)op, (b)ottom, (l)eft, (r)ight
     * ddd: distance along line of the side to spawn on.  measured by percentage from left or bottom, 0-100
     * aaa: starting angle for enemies that move at an angle.  not needed for enemies that don't
     * 
     * examples:
     * ot050 spawns an orbiter in the middle of the top
     * cr075045 spawns a choochoo 3/4 of the way up the right side.  it will move at a 45 degree angle
     * 
    */


    void SpawnEnemy(string input) {
        Vector3 spawnPosition;
        float myDistance;
        int myAngle = 0;

        myDistance = float.Parse(input[2].ToString() + input[3].ToString() + input[4].ToString())/100f;

        switch(input[1]){
            case 't':
                spawnPosition = new Vector3(god.bounds.xMin + (god.bounds.width * myDistance), god.bounds.yMax, 0);
                break;
            case 'b':
                spawnPosition = new Vector3(god.bounds.xMin + (god.bounds.width * myDistance), god.bounds.yMin, 0);
                break;
            case 'l':
                spawnPosition = new Vector3(god.bounds.xMin, god.bounds.yMin + (god.bounds.height * myDistance), 0);
                break;
            case 'r':
                spawnPosition = new Vector3(god.bounds.xMax, god.bounds.yMin + (god.bounds.height * myDistance), 0);
                break;
            default:
                spawnPosition = new Vector3((god.bounds.xMax - god.bounds.xMin) * myDistance, god.bounds.yMax, 0);
                break;
        }

        if (input.Length > 5)
            myAngle = (int)(input[5] + input[6] + input[7]);

        switch(input[0]){
            case 'o':
                currentWaveEnemies.Add((GameObject)Instantiate(orbiterPrefab, spawnPosition, Quaternion.identity));
                break;
            case 'c':
                currentWaveEnemies.Add((GameObject)Instantiate(choochooPrefab, spawnPosition, Quaternion.identity));
                currentWaveEnemies[currentWaveEnemies.Count - 1].GetComponent<ChooChoo>().MoveAngle = myAngle;
                break;
            case 'p':
                currentWaveEnemies.Add((GameObject)Instantiate(pfmPrefab, spawnPosition, Quaternion.Euler(90,0,0)));
                break;
            default:
                break;
        }

    }

}
