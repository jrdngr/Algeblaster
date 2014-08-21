using UnityEngine;
using System.Collections;

#pragma warning disable 414

public class EnemyGunSpread : MonoBehaviour {

    [SerializeField] private int damage;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletAngle;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float volleyDelay;
    [SerializeField] private int bulletsPerVolley;

    private Vector3 fireDirection;
    private GameObject myMesh;
    private GameObject myBullet;
    private Timer bulletTimer;
    private Timer volleyTimer;

    
    void Awake() {
        Initialize();
    }

    void Start() {
        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Trigger += FireGun;
        volleyTimer = gameObject.AddComponent<Timer>();
        volleyTimer.Trigger += TriggerGun;
        volleyTimer.Go(volleyDelay);
    }

    void Initialize() {
        myMesh = transform.Find("MSGSpreadMesh").gameObject;
        myMesh.SetActive(false);
        myBullet = (GameObject)Resources.Load("Weapons/Enemy/EnemyBullet");
        bulletAngle *= (Mathf.PI / 180f);  //Converts angle to radians
    }


    void TriggerGun() {
        bulletTimer.Go(fireSpeed, bulletsPerVolley);
        volleyTimer.Go(volleyDelay);
    }

    void FireGun() {
        fireDirection = transform.parent.transform.forward;
        float sideAngle = Mathf.Atan2(fireDirection.y,fireDirection.x);
        Vector3 sideVelocity;
        SpawnBullet(damage, fireDirection * bulletVelocity);
        sideVelocity = new Vector3(Mathf.Cos(sideAngle + bulletAngle), Mathf.Sin(sideAngle + bulletAngle), 0) * bulletVelocity;
        SpawnBullet(damage, sideVelocity);
        sideVelocity = new Vector3(Mathf.Cos(sideAngle - bulletAngle), Mathf.Sin(sideAngle - bulletAngle), 0) * bulletVelocity;
        SpawnBullet(damage, sideVelocity);
    }

    void SpawnBullet(int dmg, Vector3 vel) {
        GameObject currentBullet = (GameObject)Instantiate(myBullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<EnemyWeapon>().Damage = dmg;
        currentBullet.GetComponent<EnemyWeapon>().Velocity = vel;
    }

}
