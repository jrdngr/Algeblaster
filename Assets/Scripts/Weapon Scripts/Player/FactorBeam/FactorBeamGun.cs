using UnityEngine;
using System.Collections;

#pragma warning disable 0414

public class FactorBeamGun : Weapon {

    private const float laserOffTime = 0.01f;
    private const float juiceDrainTime = 0.02f;
    private const float emptyJuiceDelay = 1f;

    private bool hasTractor;
    private bool firing = false;
    private bool drainJuice = true;
    private bool goodOnJuice = true;
    private int juiceCost;
    private Timer laserOffTimer;
    private Timer juiceDrainTimer;
    private Timer emptyJuiceTimer;
    private GameObject myLaser;
    private playerJuiceManager juiceManager;
    private FactorBeamLaser myLaserScript;

    protected override void Awake() {
        base.Awake();
        juiceManager = GameObject.FindGameObjectWithTag("Player").GetComponent<playerJuiceManager>();
//        delay = weaponManager.FactorBeamFireDelay;
        projectile = weaponManager.FactorBeamProjectile;
        projectileDamage = weaponManager.FactorBeamDamage;
//        projectileSpeed = weaponManager.FactorBeamProjectileSpeed;
        projectileHitEffect = weaponManager.FactorBeamHitEffect;
        projectileType = WeaponHit.WeaponType.fac;
        projectileSpawnPosition = transform.FindChild("SpawnPoint");
        hasTractor = weaponManager.FactorBeamHasTractor;
        myLaser = (GameObject)Instantiate(projectile, transform.position, Quaternion.identity);
        myLaser.transform.parent = transform;
        myLaser.SetActive(false);
        myLaserScript = myLaser.GetComponent<FactorBeamLaser>();
        laserOffTimer = gameObject.AddComponent<Timer>();
        laserOffTimer.Trigger += LaserOff;
        if (hasTractor)
            juiceCost = weaponManager.FactorBeamTractorJuiceDrain;
        else
            juiceCost = weaponManager.FactorBeamJuiceDrain;
        juiceDrainTimer = gameObject.AddComponent<Timer>();
        juiceDrainTimer.Trigger += ResetJuiceDrain;
        emptyJuiceTimer = gameObject.AddComponent<Timer>();
        emptyJuiceTimer.Trigger += ResetGoodOnJuice;
     }

    void FixedUpdate() {
        if (firing && goodOnJuice) {
            myLaser.SetActive(true);
            if (drainJuice) {
                juiceManager.UseJuice(juiceCost);
                drainJuice = false;
                juiceDrainTimer.Go(juiceDrainTime);
            }
        }
        else
            myLaser.SetActive(false);
        if (!emptyJuiceTimer.Running &&  juiceManager.CurrentJuice <= juiceCost) {
            goodOnJuice = false;
            emptyJuiceTimer.Go(emptyJuiceDelay);
        }
    }

    void LaserOff() {
        firing = false;
    }

    void ResetJuiceDrain() {
        drainJuice = true;
    }

    void ResetGoodOnJuice() {
        goodOnJuice = true;
    }

    public override void Fire() {
        firing = true;
        if (laserOffTimer.Running)
            laserOffTimer.Reset();
        else
            laserOffTimer.Go(laserOffTime);
    }

}
