using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Level : MonoBehaviour {

    private const float squadronSpawnDistance = 2f;

    public bool hasMothership;
    public string mothershipEquation;
    public int experienceReward;
    public List<Wave> waveList = new List<Wave>();

    private int currentWave = 0;
    private bool spawnedWave = false;
    private bool spawnedMothership = false;
    private List<GameObject> currentWaveEnemies = new List<GameObject>();

    private PlayerManager playerManager;
    private LevelManager levelManager;
    private GameObject orbiterPrefab;
    private GameObject choochooPrefab;
    private GameObject squadronPrefab;
    private GameObject pfmPrefab;
    private GameObject mothershipPrefab;

    void Start() {
        levelManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<LevelManager>();
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        orbiterPrefab = (GameObject)Resources.Load("Enemies/Fodder/Orbiter");
        choochooPrefab = (GameObject)Resources.Load("Enemies/Fodder/ChooChoo");
        squadronPrefab = (GameObject)Resources.Load("Enemies/Fodder/Squadron");
        mothershipPrefab = (GameObject)Resources.Load("Enemies/Mothership/Number/Mothership");
        if (playerManager.FrequencyMode == PlayerManager.FrequencyModes.Color)
            pfmPrefab = (GameObject)Resources.Load("Enemies/Minions/ColorPFMinion");
        else
            pfmPrefab = (GameObject)Resources.Load("Enemies/Minions/PrimeFactorMinion");
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
            GameObject ms = (GameObject)Instantiate(mothershipPrefab, new Vector3(0, levelManager.bounds.yMax, 0), Quaternion.identity);
            ms.GetComponent<Mothership>().Equation = mothershipEquation;
            spawnedMothership = true;
        }
}


    /* format:  tsdddaaa
     * t: type of enemy -- (o)rbiter, (c)hoochoo, (s)quadron, (p)fminion
     * s: side to spawn on -- (t)op, (b)ottom, (l)eft, (r)ight
     * ddd: distance along line of the side to spawn on.  measured by percentage from left or bottom, 0-100
     * aaa: choochoo -- starting angle in degrees
     * aaa: squadron -- number of units, pattern, direction
     
     * patterns: 0 = straight, 1 = curve, 3 = zigzag
     * direction: 0 = left, 1 = right
     * 
     * examples:
     * ot050 spawns an orbiter in the middle of the top
     * cr075045 spawns a choochoo 3/4 of the way up the right side.  It will move at a 45 degree angle
     * st025311 spawns 3 squadron units 1/4 of the way across the top.  They will move in a curve to the right.  
    */


    void SpawnEnemy(string input) {
        Vector3 spawnPosition;
        float myDistance;
        int myAngle = 0;
        int numberOfSquadronUnits = 0;

        myDistance = float.Parse(input[2].ToString() + input[3].ToString() + input[4].ToString())/100f;

        switch(input[1]){
            case 't':
                spawnPosition = new Vector3(levelManager.bounds.xMin + (levelManager.bounds.width * myDistance), levelManager.bounds.yMax, 0);
                break;
            case 'b':
                spawnPosition = new Vector3(levelManager.bounds.xMin + (levelManager.bounds.width * myDistance), levelManager.bounds.yMin, 0);
                break;
            case 'l':
                spawnPosition = new Vector3(levelManager.bounds.xMin, levelManager.bounds.yMin + (levelManager.bounds.height * myDistance), 0);
                break;
            case 'r':
                spawnPosition = new Vector3(levelManager.bounds.xMax, levelManager.bounds.yMin + (levelManager.bounds.height * myDistance), 0);
                break;
            default:
                spawnPosition = new Vector3((levelManager.bounds.xMax - levelManager.bounds.xMin) * myDistance, levelManager.bounds.yMax, 0);
                break;
        }

        if (input.Length > 5) {
            numberOfSquadronUnits = int.Parse(input[5].ToString());
            myAngle = (int)(input[5] + input[6] + input[7]);
        }
        switch(input[0]){
            case 'o':
                currentWaveEnemies.Add((GameObject)Instantiate(orbiterPrefab, spawnPosition, Quaternion.identity));
                break;
            case 'c':
                currentWaveEnemies.Add((GameObject)Instantiate(choochooPrefab, spawnPosition, Quaternion.identity));
                currentWaveEnemies[currentWaveEnemies.Count - 1].GetComponent<ChooChoo>().MoveAngle = myAngle;
                break;
            case 's':
                for (int i = 0; i < numberOfSquadronUnits; i++) {
                    currentWaveEnemies.Add((GameObject)Instantiate(squadronPrefab, spawnPosition, Quaternion.Euler(90, 0, 0)));
                    currentWaveEnemies[currentWaveEnemies.Count - 1].GetComponent<Squadron>().Pattern = int.Parse(input[6].ToString());
                    currentWaveEnemies[currentWaveEnemies.Count - 1].GetComponent<Squadron>().SquadronDirection = int.Parse(input[7].ToString());
                    spawnPosition.y += squadronSpawnDistance;
                    if (int.Parse(input[7].ToString()) == 1)
                        spawnPosition.x -= squadronSpawnDistance;
                    else
                        spawnPosition.x += squadronSpawnDistance;
                }
                break;
            case 'p':
                currentWaveEnemies.Add((GameObject)Instantiate(pfmPrefab, spawnPosition, Quaternion.Euler(90,0,0)));
                break;
            default:
                break;
        }

    }

    public void AddEnemy(GameObject newEnemy) {
        currentWaveEnemies.Add(newEnemy);
    }

}
