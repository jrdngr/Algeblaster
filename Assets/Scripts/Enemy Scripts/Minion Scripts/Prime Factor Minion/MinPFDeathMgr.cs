using UnityEngine;
using System.Collections;

// Manages the minion's health and spawns new minions when appropriate
public class MinPFDeathMgr : Minion {

    [SerializeField] private int orbModifier;
    [SerializeField] private GameObject leftSpawn;
    [SerializeField] private GameObject rightSpawn;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnedRocket;

    private ArrayList factorList;
    private int currentHP;
    private int shipNumber = 0;
    private int lastHitFreq = 0;
    private WeaponHit.WeaponColor lastHitColor = WeaponHit.WeaponColor.blue;
    private GameObject newShipPrefab;
    private EnemyHealthManager healthMgr;

    public int OrbModifier {
        get {
            return orbModifier;
        }
        set {
            orbModifier = value;
        }
    }

    void Start() {
        healthMgr = GetComponent<EnemyHealthManager>();
        currentHP = healthMgr.CurrentHP;
        shipNumber = GetComponent<MinPFNumberMgr>().GetShipNumber();
        factorList = GetComponent<MinPFNumberMgr>().FactorList;
        newShipPrefab = (GameObject)Resources.Load("Enemies/Minions/PrimeFactorMinion");
        if (orbModifier < 1)
            orbModifier = 1;
    }

    void Update() {
        currentHP = healthMgr.CurrentHP;
        lastHitFreq = healthMgr.LastHitFrequency;
        lastHitColor = healthMgr.LastHitColor;
        CheckHitpoints();
    }

    // Checks to see if this minion should be dead, splits the minion into factors or reduces the minion
    // by a given factor depending on the frequency of the last bullet to hit
    void CheckHitpoints() {
        bool hitPrime = false;
        if (currentHP <= 0) {
            int newValue = 0;
            int newValueIndex = 0;
            GameObject explosion = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(explosion, 2f);
            //If the minion can't split anymore, it dies
            if (factorList.Count == 0) {
                GetComponent<MinPFPowerupMgr>().SpawnOrbs(transform.position, orbModifier);
                EventManager.TriggerKilledMinion();
                Destroy(this.gameObject);
            }
            //If it can split, it does
            else {
                for (int i = 0; i < factorList.Count; i++) {
                    if (shipNumber % lastHitFreq == 0 && shipNumber != lastHitFreq) {
                        newValue = lastHitFreq;
                        hitPrime = true;
                    }
                }
                if (hitPrime) {
                    GameObject hitFactorShip = (GameObject)Instantiate(newShipPrefab, transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    hitFactorShip.GetComponent<MinPFNumberMgr>().SetShipNumber(shipNumber / newValue);
                    Destroy(this.gameObject);
                }
                else {
                    newValueIndex = Random.Range(0, factorList.Count);
                    //Generate right ship with randomly chosen prime factor
                    GameObject newShipRight = (GameObject)Instantiate(newShipPrefab, rightSpawn.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    newShipRight.GetComponent<MinPFNumberMgr>().SetShipNumber((int)factorList[newValueIndex]);
                    newShipRight.GetComponent<MinPFDeathMgr>().OrbModifier = orbModifier - 1;
                    //Generate left ship with a new number reduced by the previously chosen prime factor
                    GameObject newShipLeft = (GameObject)Instantiate(newShipPrefab, leftSpawn.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
                    newShipLeft.GetComponent<MinPFNumberMgr>().SetShipNumber(shipNumber / (int)factorList[newValueIndex]);
                    newShipLeft.GetComponent<MinPFDeathMgr>().OrbModifier = orbModifier - 1;
                    Instantiate(spawnedRocket, transform.position, Quaternion.Euler(new Vector3(0, 225, 180)));
                    EventManager.TriggerCreatedMinion();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
