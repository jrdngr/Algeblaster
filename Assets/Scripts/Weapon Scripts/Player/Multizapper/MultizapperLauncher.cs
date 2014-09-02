using UnityEngine;
using System.Collections;

public class MultizapperLauncher : Weapon {

    private float zapRange;
    private float zapSpeed;
    private int zapDamage;
    private float chainRange;
    private int numberOfChains;
    private bool hasTether;
    private GameObject zapPrefab;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.MultizapperFireDelay;
        projectile = weaponManager.MultizapperBall;
        projectileDamage = weaponManager.MultizapperBallDamage;
        projectileSpeed = weaponManager.MultizapperBallSpeed;
        projectileHitEffect = weaponManager.MultizapperHitEffect;
        projectileType = WeaponHit.WeaponType.mult;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        projectileVelocity = Vector3.zero;
        zapRange = weaponManager.MultizapperZapRange;
        zapSpeed = weaponManager.MultizapperZapSpeed;
        zapDamage = weaponManager.MultizapperZapDamage;
        chainRange = weaponManager.MultizapperChainRange;
        numberOfChains = weaponManager.MultizapperNumberOfChains;
        hasTether = weaponManager.MultizapperHasTether;
        zapPrefab = weaponManager.MultizapperZap;
    }

    public void BallDead() {
        canFire = true;
    }

    public override void Fire() {
        if (canFire) {
            canFire = false;
            projectileVelocity = new Vector3(0, projectileSpeed, 0);
            projectileFrequency = playerWeaponManager.Frequency;
            GameObject myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
            myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
            myBullet.GetComponent<MultizapperBall>().SetBallProperties(zapPrefab, delay, zapRange, zapSpeed, zapDamage, chainRange, numberOfChains);
            myBullet.GetComponent<MultizapperBall>().MyLauncher = this.gameObject;
        }
    }

}
