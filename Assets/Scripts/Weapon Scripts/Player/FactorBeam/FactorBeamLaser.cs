using UnityEngine;
using System.Collections;

public class FactorBeamLaser : MonoBehaviour {

    private const float beamLength = 20f;
    private const float bottomWidth = 0.2f;
    private const float topWidth = 1f;
    private const float growthSpeed = 2;
    private const float damageTickTime = 0.20f;

    private float startTime;
    private float currentBottomWidth;
    private float currentTopWidth;
    private bool canDamage = true;
    private bool hasTractor;
    private bool hasTractoredEnemy;
    private float tractorDistance;
    private GameObject tractorTarget;
    private Timer damageTickTimer;
    private LineRenderer myLine;
    private WeaponHit hit = new WeaponHit();
    private GameObject hitEffect;
    private GameObject holdEffect;
    private playerWeaponManager playerWeaponManager;
    private WeaponManager weaponManager;

    void Awake() {
        damageTickTimer = gameObject.AddComponent<Timer>();
        damageTickTimer.Trigger += ResetTick;
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        playerWeaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerWeaponManager>();
        hasTractor = weaponManager.FactorBeamHasTractor;
        hit.damage = weaponManager.FactorBeamDamage;
        hit.type = WeaponHit.WeaponType.fac;
        hitEffect = weaponManager.FactorBeamHitEffect;
        holdEffect = weaponManager.FactorBeamTractorHoldEffect;
    }

    void OnEnable() {
        if (hasTractor) {
            myLine = transform.Find("greenline").GetComponent<LineRenderer>();
            transform.Find("redline").gameObject.SetActive(false);
        }
        else {
            myLine = transform.Find("redline").GetComponent<LineRenderer>();
            transform.Find("greenline").gameObject.SetActive(false);
        }
        currentBottomWidth = 0f;
        currentTopWidth = 0f;
        myLine.SetWidth(currentBottomWidth, currentTopWidth);
        startTime = Time.time;
        canDamage = true;
        tractorTarget = null;
        hasTractoredEnemy = false;
    }

    void OnTriggerStay(Collider other) {
        if (!hasTractor && other.gameObject.GetComponent<EnemyHealthManager>() != null && canDamage) {
            other.gameObject.GetComponent<EnemyHealthManager>().Hit(hit);
            GameObject myEffect = (GameObject)Instantiate(hitEffect, other.transform.position, Quaternion.identity);
            Destroy(myEffect, 5f);
            canDamage = false;
            damageTickTimer.Go(damageTickTime);
        }
        else if (hasTractor && other.gameObject.CompareTag("Fodder")) {
            if (!hasTractoredEnemy) {
                tractorDistance = Vector3.Distance(transform.position, other.transform.position);
                tractorTarget = other.gameObject;
            }
            hasTractoredEnemy = true;
            tractorTarget.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + tractorDistance, 0);
            IStunnable stunnableTarget = tractorTarget.GetComponent(typeof(IStunnable)) as IStunnable;
            stunnableTarget.Stun();
        }
    }

    void Update() {
        hit.frequency = playerWeaponManager.Frequency;

        Vector3 newPos = transform.position;
        newPos.y += beamLength;
        myLine.SetPosition(0, transform.position);
        myLine.SetPosition(1, newPos);

        //Make beam grow when it first appears
        if (currentTopWidth < topWidth) {
            float timeFactor = Time.time - startTime;
            currentBottomWidth = Mathf.Lerp(0, bottomWidth, timeFactor * growthSpeed);
            currentTopWidth = Mathf.Lerp(0, topWidth, timeFactor * growthSpeed);
            myLine.SetWidth(currentBottomWidth, currentTopWidth);
        }
    }

    void ResetTick() {
        canDamage = true;
    }
}
