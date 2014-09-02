using UnityEngine;
using System.Collections;

public class DisintegratorLauncher : Weapon {

    //Projectile properties
    private float blastRadius;
    private float rocketAcceleration;
    private bool homing;

    //Launcher management
    private bool fireLeft = true;
    private GameObject leftDummy;
    private GameObject rightDummy;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.DisintegratorFireDelay;
        projectile = weaponManager.DisintegratorProjectile;
        projectileDamage = weaponManager.DisintegratorDamage;
        projectileSpeed = weaponManager.DisintegratorProjectileSpeed;
        projectileHitEffect = weaponManager.DisintegratorHitEffect;
        projectileType = WeaponHit.WeaponType.div;
        blastRadius = weaponManager.DisintegratorBlastRadius;
        rocketAcceleration = weaponManager.DisintegratorAcceleration;
        homing = weaponManager.DisintegratorHoming;
        leftDummy = transform.Find("LeftDummy").gameObject;
        rightDummy = transform.Find("RightDummy").gameObject;
    }

    public override void Fire() {
        if (canFire) {
            canFire = false;
            delayTimer.Go(delay);
            projectileVelocity = new Vector3(0, projectileSpeed, 0);
            projectileFrequency = playerWeaponManager.Frequency;
            GameObject myRocket;
            if (fireLeft) {
                myRocket = (GameObject)Instantiate(projectile, leftDummy.transform.position, Quaternion.identity);
                fireLeft = false;
            }
            else {
                myRocket = (GameObject)Instantiate(projectile, rightDummy.transform.position, Quaternion.identity);
                fireLeft = true;
            }
            myRocket.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
            myRocket.GetComponent<DisintegratorRocket>().Acceleration = rocketAcceleration;
            myRocket.GetComponent<DisintegratorRocket>().Homing = homing;
            myRocket.GetComponent<DisintegratorRocket>().BlastRadius = blastRadius;
        }
    }

}
