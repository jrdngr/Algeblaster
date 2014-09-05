using UnityEngine;
using System.Collections;

public class DisintegratorLauncher : Weapon {

    //Projectile properties
    private float blastRadius;
    private float rocketAcceleration;
    private bool homing;
    private float homingRadius;
    private float homingAcceleration;
    private float rotationDamping;

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
        homingRadius = weaponManager.DisintegratorHomingRadius;
        homingAcceleration = weaponManager.DisintegratorHomingAcceleration;
        rotationDamping = weaponManager.DisintegratorRotationDamping;
        leftDummy = transform.Find("LeftDummy").gameObject;
        rightDummy = transform.Find("RightDummy").gameObject;
    }

    public override void Fire() {
        if (canFire) {
            GetComponent<AudioSource>().Play();
            canFire = false;
            delayTimer.Go(delay);
            colorGlowEffect = playerWeaponManager.WeaponColorGlow;
            projectileVelocity = new Vector3(0, 0, 0);
            projectileFrequency = playerWeaponManager.Frequency;
            projectileColor = (WeaponHit.WeaponColor)playerWeaponManager.CurrentColor;
            GameObject myRocket;
            if (fireLeft) {
                myRocket = (GameObject)Instantiate(projectile, leftDummy.transform.position, Quaternion.Euler(-90,0,0));
                fireLeft = false;
                if (myFrequencyMode == PlayerManager.FrequencyModes.Color) {
                    GameObject myGlow = (GameObject)Instantiate(colorGlowEffect, leftDummy.transform.position, Quaternion.identity);
                    myGlow.transform.parent = myRocket.transform;
                }
            }
            else {
                myRocket = (GameObject)Instantiate(projectile, rightDummy.transform.position, Quaternion.Euler(-90, 0, 0));
                fireLeft = true;
                if (myFrequencyMode == PlayerManager.FrequencyModes.Color) {
                    GameObject myGlow = (GameObject)Instantiate(colorGlowEffect, rightDummy.transform.position, Quaternion.identity);
                    myGlow.transform.parent = myRocket.transform;
                }
            }
            myRocket.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileColor, projectileHitEffect);
            myRocket.GetComponent<DisintegratorRocket>().SetRocketProperties(rocketAcceleration, blastRadius, homing, homingRadius, homingAcceleration, rotationDamping);
        }
    }

}
