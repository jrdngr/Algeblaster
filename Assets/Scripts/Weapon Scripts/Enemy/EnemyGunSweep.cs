using UnityEngine;
using System.Collections;

#pragma warning disable 414

public class EnemyGunSweep : MonoBehaviour {

    [SerializeField] private int damage;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float bulletSweepAngle;
    [SerializeField] private float fireSpeed;

    private float currentAngle;
    private Vector3 angleVelocity;
    private GameObject myMesh;
    private GameObject myBullet;
    private Timer bulletTimer;


    void Awake() {
        Initialize();
    }

    void Start() {
        bulletTimer = gameObject.AddComponent<Timer>();
        bulletTimer.Trigger += FireGun;
        bulletTimer.Go(fireSpeed);
    }

    void Initialize() {
        myMesh = transform.Find("MSGSweepMesh").gameObject;
        myMesh.SetActive(false);
        myBullet = (GameObject)Resources.Load("Weapons/Enemy/EnemyBullet");
        fireSpeed = 1 / fireSpeed;
        bulletSweepAngle *= (Mathf.PI / 180f);
        currentAngle = Mathf.PI;
    }

    void FireGun() {
        if (currentAngle > 2*Mathf.PI - 2*bulletSweepAngle || currentAngle < Mathf.PI - 2*bulletSweepAngle)
            bulletSweepAngle *= -1;
        currentAngle += bulletSweepAngle;
        angleVelocity = new Vector3(bulletVelocity * Mathf.Cos(currentAngle), bulletVelocity * Mathf.Sin(currentAngle), 0);
        SpawnBullet(damage, angleVelocity);
        bulletTimer.Go(fireSpeed);
    }

    void SpawnBullet(int dmg, Vector3 vel) {
        GameObject currentBullet = (GameObject)Instantiate(myBullet, transform.position, Quaternion.identity);
        currentBullet.GetComponent<EnemyWeapon>().Damage = dmg;
        currentBullet.GetComponent<EnemyWeapon>().Velocity = vel;
    }

}
