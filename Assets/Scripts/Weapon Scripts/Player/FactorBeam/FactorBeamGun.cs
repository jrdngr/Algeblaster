using UnityEngine;
using System.Collections;

public class FactorBeamGun : Weapon {

    private bool hasTractor;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.FactorBeamFireDelay;
        projectile = weaponManager.FactorBeamProjectile;
        projectileDamage = weaponManager.FactorBeamDamage;
        projectileSpeed = weaponManager.FactorBeamProjectileSpeed;
        projectileHitEffect = weaponManager.FactorBeamHitEffect;
        projectileType = WeaponHit.WeaponType.pos;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        projectileVelocity = Vector3.zero;
        hasTractor = weaponManager.FactorBeamHasTractor;
    }


}
