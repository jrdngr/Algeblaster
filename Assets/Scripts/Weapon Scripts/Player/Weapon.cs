using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

    protected bool canFire = true;
    protected WeaponManager weaponManager;
    protected LevelManager levelManager;
    protected playerWeaponManager playerWeaponManager;
    protected PlayerManager playerManager;

    //Weapon properties
    protected float delay;
    protected Timer delayTimer;
    protected GameObject projectile;
    protected Transform projectileSpawnPosition;
    protected PlayerManager.FrequencyModes myFrequencyMode;
    protected GameObject colorGlowEffect;

    //Properties to pass on to instantiated projectiles
    protected int projectileDamage;
    protected int projectileFrequency;
    protected WeaponHit.WeaponColor projectileColor;
    protected float projectileSpeed;
    protected Vector3 projectileVelocity = Vector3.zero;
    protected Rect projectileBounds;
    protected WeaponHit.WeaponType projectileType;
    protected GameObject projectileHitEffect;

    protected virtual void Awake() {
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        levelManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<LevelManager>();
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        playerWeaponManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerWeaponManager>();
        myFrequencyMode = playerManager.FrequencyMode;
        delayTimer = gameObject.AddComponent<Timer>();
        delayTimer.Trigger += ReadyToFire;
        projectileBounds = levelManager.bounds;
    }

    protected virtual void ReadyToFire() {
        canFire = true;
    }

    public virtual void Fire() {
        if (canFire) {
            canFire = false;
            delayTimer.Go(delay);
            colorGlowEffect = playerWeaponManager.WeaponColorGlow;
            projectileVelocity = new Vector3(0, projectileSpeed, 0);
            projectileFrequency = playerWeaponManager.Frequency;
            projectileColor = (WeaponHit.WeaponColor)playerWeaponManager.CurrentColor;
            GameObject myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
            myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileColor, projectileHitEffect);
            if (myFrequencyMode == PlayerManager.FrequencyModes.Color) {
                GameObject myGlow = (GameObject)Instantiate(colorGlowEffect, projectileSpawnPosition.transform.position, Quaternion.identity);
                myGlow.transform.parent = myBullet.transform;
            }
        }
    }

}
