using UnityEngine;
using System.Collections;

public class PFMColorDeathManager : MonoBehaviour {

    private const int orbModifier = 5;

    [SerializeField] private GameObject leftSpawn;
    [SerializeField] private GameObject rightSpawn;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnedRocket;

    private int currentHP;
    private WeaponHit.WeaponColor lastHitColor = WeaponHit.WeaponColor.blue;
    private PFMColorManager myColorManager;
    private GameObject newShipPrefab;
    private EnemyHealthManager healthMgr;
    private Level currentLevel;

    void Start() {
        currentLevel = GameObject.Find("Level").GetComponent<Level>();
        healthMgr = GetComponent<EnemyHealthManager>();
        myColorManager = GetComponent<PFMColorManager>();
        currentHP = healthMgr.CurrentHP;
        newShipPrefab = (GameObject)Resources.Load("Enemies/Minions/ColorPFMinion");
    }

    void Update() {
        currentHP = healthMgr.CurrentHP;
        lastHitColor = healthMgr.LastHitColor;
        CheckHitpoints();
    }

    void CheckHitpoints() {
        if (currentHP <= 0) {
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            //If the minion can't split anymore, it dies
            if (myColorManager.IsPrimary) {
                GetComponent<MinPFPowerupMgr>().SpawnOrbs(transform.position, orbModifier);
                EventManager.TriggerKilledMinion();
                Destroy(this.gameObject);
            }
            //If it can split, it does
            else {
                WeaponHit.WeaponColor newColor = WeaponHit.WeaponColor.blue;
                WeaponHit.WeaponColor otherNewColor = WeaponHit.WeaponColor.blue;
                bool spawnBoth = false;
                switch (myColorManager.CurrentColor) {
                    case WeaponHit.WeaponColor.purple:
                        if (lastHitColor == WeaponHit.WeaponColor.yellow) {
                            spawnBoth = true;
                            newColor = WeaponHit.WeaponColor.blue;
                            otherNewColor = WeaponHit.WeaponColor.red;
                        }
                        else if (lastHitColor == WeaponHit.WeaponColor.blue)
                            newColor = WeaponHit.WeaponColor.red;
                        else
                            newColor = WeaponHit.WeaponColor.blue;
                        break;
                    case WeaponHit.WeaponColor.green:
                        if (lastHitColor == WeaponHit.WeaponColor.red) {
                            spawnBoth = true;
                            newColor = WeaponHit.WeaponColor.blue;
                            otherNewColor = WeaponHit.WeaponColor.yellow;
                        }
                        else if (lastHitColor == WeaponHit.WeaponColor.blue)
                            newColor = WeaponHit.WeaponColor.yellow;
                        else
                            newColor = WeaponHit.WeaponColor.blue;
                        break;
                    case WeaponHit.WeaponColor.orange:
                        if (lastHitColor == WeaponHit.WeaponColor.blue) {
                            spawnBoth = true;
                            newColor = WeaponHit.WeaponColor.yellow;
                            otherNewColor = WeaponHit.WeaponColor.red;
                        }
                        else if (lastHitColor == WeaponHit.WeaponColor.red)
                            newColor = WeaponHit.WeaponColor.yellow;
                        else
                            newColor = WeaponHit.WeaponColor.red;
                        break;
                }
                if (spawnBoth){
                    GameObject newMinion = (GameObject)Instantiate(newShipPrefab, leftSpawn.transform.position, Quaternion.Euler(90, 0, 0));
                    newMinion.GetComponent<PFMColorManager>().CurrentColor = newColor;
                    newMinion.GetComponent<PFMColorManager>().IsPrimary = true;
                    currentLevel.AddEnemy(newMinion);
                    newMinion = (GameObject)Instantiate(newShipPrefab, rightSpawn.transform.position, Quaternion.Euler(90, 0, 0));
                    newMinion.GetComponent<PFMColorManager>().CurrentColor = otherNewColor;
                    newMinion.GetComponent<PFMColorManager>().IsPrimary = true;
                    currentLevel.AddEnemy(newMinion);
                    Instantiate(spawnedRocket, transform.position, Quaternion.Euler(new Vector3(0, 225, 180)));                    
                    Destroy(this.gameObject);
                    EventManager.TriggerCreatedMinion();
                }
                else{
                    GameObject newMinion = (GameObject)Instantiate(newShipPrefab, transform.position, Quaternion.Euler(90,0,0));
                    newMinion.GetComponent<PFMColorManager>().CurrentColor = newColor;
                    newMinion.GetComponent<PFMColorManager>().IsPrimary = true;
                    currentLevel.AddEnemy(newMinion);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
