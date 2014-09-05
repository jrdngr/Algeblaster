using UnityEngine;
using System.Collections;

#pragma warning disable 0414

public class NegatronGun : Weapon {

    private int myShape;

    protected override void Awake() {
        base.Awake();
        delay = weaponManager.NegatronFireDelay;
        projectile = weaponManager.NegatronProjectile;
        projectileDamage = weaponManager.NegatronDamage;
        projectileSpeed = weaponManager.NegatronProjectileSpeed;
        projectileHitEffect = weaponManager.NegatronHitEffect;
        projectileType = WeaponHit.WeaponType.neg;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        myShape = weaponManager.NegatronShape;
    }

}
