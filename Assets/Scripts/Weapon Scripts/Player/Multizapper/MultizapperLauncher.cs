using UnityEngine;
using System.Collections;

public class MultizapperLauncher : Weapon {

    private float zapRange;
    private float zapSpeed;
    private int zapDamage;
    private float chainRange;
    private int numberOfChains;
    private bool hasTether;
    private bool hasBallOfSteel = false;
    private GameObject zapPrefab;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.MultizapperFireDelay;
        hasTether = weaponManager.MultizapperHasTether;
        if (hasTether)
            hasBallOfSteel = weaponManager.MultizapperHasBallOfSteel;
        if (hasBallOfSteel) {
            projectile = weaponManager.MultizapperBallOfSteelBall;
            projectileHitEffect = weaponManager.MultizapperBallOfSteelHitEffect;
        }
        else {
            projectile = weaponManager.MultizapperBall;
            projectileHitEffect = weaponManager.MultizapperHitEffect;
        }
        projectileDamage = weaponManager.MultizapperBallDamage;
        projectileSpeed = weaponManager.MultizapperBallSpeed;
        projectileType = WeaponHit.WeaponType.mult;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        zapRange = weaponManager.MultizapperZapRange;
        zapSpeed = weaponManager.MultizapperZapSpeed;
        zapDamage = weaponManager.MultizapperZapDamage;
        chainRange = weaponManager.MultizapperChainRange;
        numberOfChains = weaponManager.MultizapperNumberOfChains;
        zapPrefab = weaponManager.MultizapperZap;
    }

    public void BallDead() {
        canFire = true;
    }

    public override void Fire() {
        if (canFire) {
            canFire = false;
            colorGlowEffect = playerWeaponManager.WeaponColorGlow;
            projectileVelocity = new Vector3(0, projectileSpeed, 0);
            projectileFrequency = playerWeaponManager.Frequency;
            projectileColor = (WeaponHit.WeaponColor)playerWeaponManager.CurrentColor;
            GameObject myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
            if (hasTether)
                myBullet.GetComponent<SpringJoint>().connectedBody = transform.parent.rigidbody;
            else
                myBullet.GetComponent<SpringJoint>().breakForce = 0;
            myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileColor, projectileHitEffect);
            myBullet.GetComponent<MultizapperBall>().SetBallProperties(hasTether, hasBallOfSteel, zapPrefab, delay, zapRange, zapSpeed, zapDamage, chainRange, numberOfChains);
            myBullet.GetComponent<MultizapperBall>().MyLauncher = this.gameObject;
            if (myFrequencyMode == PlayerManager.FrequencyModes.Color) {
                GameObject myGlow = (GameObject)Instantiate(colorGlowEffect, projectileSpawnPosition.transform.position, Quaternion.identity);
                myGlow.transform.parent = myBullet.transform;
            }
        }
    }

}
