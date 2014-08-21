using UnityEngine;
using System.Collections;

#pragma warning disable 414

public class EnemyGunLine : MonoBehaviour {

    [SerializeField] private int damage;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float distanceBetweenBullets;
    [SerializeField] private float fireSpeed;
    [SerializeField] private float volleyDelay;
    [SerializeField] private int bulletsPerVolley;
    [SerializeField] private int bulletsAtATime = 0;

    private GameObject myMesh;
    private GameObject myBullet;
    private Timer bulletTimer;
    private Timer volleyTimer;


    void Awake() {
        Initialize();
        if (bulletsAtATime == 0)
            bulletsAtATime = 3;
    }

    void Start() {
        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Trigger += FireGun;
        volleyTimer = gameObject.AddComponent<Timer>();
        volleyTimer.Trigger += TriggerGun;
        volleyTimer.Go(volleyDelay);
        if (bulletsAtATime < 0)
            bulletsAtATime *= -1;
        if (bulletsAtATime % 2 == 0)
            bulletsAtATime--;
    }

    void Initialize() {
        myMesh = transform.Find("MSGLineMesh").gameObject;
        myBullet = (GameObject)Resources.Load("Weapons/Enemy/EnemyBullet");
    }

    void TriggerGun() {
        bulletTimer.Go(fireSpeed, bulletsPerVolley);
        volleyTimer.Go(volleyDelay);
    }

    void FireGun() {
        SpawnBullet(damage, transform.parent.transform.forward * bulletVelocity);
    }

    void SpawnBullet(int dmg, Vector3 vel) {
        Vector3 pos = transform.position;
        GameObject currentBullet = (GameObject)Instantiate(myBullet, pos, Quaternion.identity);
        currentBullet.GetComponent<EnemyWeapon>().Damage = dmg;
        currentBullet.GetComponent<EnemyWeapon>().Velocity = vel;

        if (bulletsAtATime > 1) {
            for (int i = 1; i < (bulletsAtATime + 1) / 2; i++) {
                pos = transform.position;
                pos.x += distanceBetweenBullets * i;
                currentBullet = (GameObject)Instantiate(myBullet, pos, Quaternion.identity);
                currentBullet.GetComponent<EnemyWeapon>().Damage = dmg;
                currentBullet.GetComponent<EnemyWeapon>().Velocity = vel;

                pos.x -= 2 * distanceBetweenBullets * i;
                currentBullet = (GameObject)Instantiate(myBullet, pos, Quaternion.identity);
                currentBullet.GetComponent<EnemyWeapon>().Damage = dmg;
                currentBullet.GetComponent<EnemyWeapon>().Velocity = vel;
            }
        }

    }
}
