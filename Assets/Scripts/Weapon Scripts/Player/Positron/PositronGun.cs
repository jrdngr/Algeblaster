using UnityEngine;
using System.Collections;

public class PositronGun : Weapon {

    private bool hasSpread;
    private int numberOfBullets;
    private float twoBulletsSpawnOffset;
    private float spreadAngle;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.PositronFireDelay;
        projectile = weaponManager.PositronProjectile;
        projectileDamage = weaponManager.PositronDamage;
        projectileSpeed = weaponManager.PositronProjectileSpeed;
        projectileHitEffect = weaponManager.PositronHitEffect;
        projectileType = WeaponHit.WeaponType.pos;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        hasSpread = weaponManager.PositronHasSpread;
        numberOfBullets = weaponManager.PositronNumberOfBullets;
        twoBulletsSpawnOffset = weaponManager.PositronTwoBulletsSpawnOffset;
        spreadAngle = weaponManager.PositronSpreadAngle;
        projectileVelocity = Vector3.zero;
        spreadAngle *= (Mathf.PI / 180) ;
    }

    public override void Fire() {
        if (canFire) {
            canFire = false;
            delayTimer.Go(delay);
            projectileFrequency = playerWeaponManager.Frequency;
            if (numberOfBullets == 1) {
                projectileVelocity = new Vector3(0, projectileSpeed, 0);
                GameObject myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
            }
            else if (numberOfBullets == 2) {
                projectileVelocity = new Vector3(0, projectileSpeed, 0);
                Vector3 spawnPosition = new Vector3(projectileSpawnPosition.transform.position.x, projectileSpawnPosition.transform.position.y, projectileSpawnPosition.transform.position.z);
                spawnPosition.x += twoBulletsSpawnOffset;
                //First Bullet
                GameObject myBullet = (GameObject)Instantiate(projectile, spawnPosition, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
                //Second Bullet
                spawnPosition.x -= 2 * twoBulletsSpawnOffset;
                myBullet = (GameObject)Instantiate(projectile, spawnPosition, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
            }
            else if (numberOfBullets >= 3) {
                //First Bullet
                projectileVelocity = new Vector3(0, projectileSpeed, 0);
                GameObject myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
                //Second Bullet
                projectileVelocity.x = projectileSpeed * Mathf.Cos((Mathf.PI / 2) + spreadAngle);
                projectileVelocity.y = projectileSpeed * Mathf.Sin((Mathf.PI / 2) + spreadAngle);
                myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
                //Third Bullet
                projectileVelocity.x = projectileSpeed * Mathf.Cos((Mathf.PI / 2) - spreadAngle);
                projectileVelocity.y = projectileSpeed * Mathf.Sin((Mathf.PI / 2) - spreadAngle);
                myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);

                if (numberOfBullets == 5) {
                    //Fourth Bullet
                    projectileVelocity.x = projectileSpeed * Mathf.Cos((Mathf.PI / 2) + (2 * spreadAngle));
                    projectileVelocity.y = projectileSpeed * Mathf.Sin((Mathf.PI / 2) + (2 * spreadAngle));
                    myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                    myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
                    //Fifth Bullet
                    projectileVelocity.x = projectileSpeed * Mathf.Cos((Mathf.PI / 2) - (2 * spreadAngle));
                    projectileVelocity.y = projectileSpeed * Mathf.Sin((Mathf.PI / 2) - (2 * spreadAngle));
                    myBullet = (GameObject)Instantiate(projectile, projectileSpawnPosition.transform.position, Quaternion.identity);
                    myBullet.GetComponent<Projectile>().SetProperties(projectileSpeed, projectileBounds, projectileVelocity, projectileDamage, projectileFrequency, projectileType, projectileHitEffect);
                }

            }
        }
    }

}
