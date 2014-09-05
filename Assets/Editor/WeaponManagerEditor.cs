using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(WeaponManager))]
public class WeaponManagerEditor : Editor {

    public override void OnInspectorGUI() {
        WeaponManager thisWeaponManager = (WeaponManager)target;

        EditorGUILayout.LabelField("Positron Gun", EditorStyles.boldLabel);
        thisWeaponManager.PositronGun = (GameObject)EditorGUILayout.ObjectField("Positron Gun", thisWeaponManager.PositronGun, typeof(GameObject), false);
        thisWeaponManager.PositronProjectile = (GameObject)EditorGUILayout.ObjectField("Projectile", thisWeaponManager.PositronProjectile, typeof(GameObject), false);
        thisWeaponManager.PositronHitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Effect", thisWeaponManager.PositronHitEffect, typeof(GameObject), false);
        thisWeaponManager.PositronDamage = EditorGUILayout.IntField("Damage", thisWeaponManager.PositronDamage);
        thisWeaponManager.PositronFireDelay = EditorGUILayout.FloatField("Fire Delay", thisWeaponManager.PositronFireDelay);
        thisWeaponManager.PositronProjectileSpeed = EditorGUILayout.FloatField("Projectile Speed", thisWeaponManager.PositronProjectileSpeed);
        thisWeaponManager.PositronHasSpread = EditorGUILayout.Toggle("Spread", thisWeaponManager.PositronHasSpread);
        thisWeaponManager.PositronNumberOfBullets = EditorGUILayout.IntField("Number of Bullets", thisWeaponManager.PositronNumberOfBullets);
        if (thisWeaponManager.PositronNumberOfBullets < 1)
            thisWeaponManager.PositronNumberOfBullets = 1;
        if (thisWeaponManager.PositronNumberOfBullets > 2 && !thisWeaponManager.PositronHasSpread)
            thisWeaponManager.PositronNumberOfBullets = 2;
        if (thisWeaponManager.PositronHasSpread && thisWeaponManager.PositronNumberOfBullets < 3)
            thisWeaponManager.PositronNumberOfBullets = 3;
        if (thisWeaponManager.PositronNumberOfBullets >= 4)
            thisWeaponManager.PositronNumberOfBullets = 5;
        if (thisWeaponManager.PositronNumberOfBullets == 2)
            thisWeaponManager.PositronTwoBulletsSpawnOffset = EditorGUILayout.FloatField("Spawn Offset", thisWeaponManager.PositronTwoBulletsSpawnOffset);
        if (thisWeaponManager.PositronHasSpread)
            thisWeaponManager.PositronSpreadAngle = EditorGUILayout.FloatField("Spread Angle", thisWeaponManager.PositronSpreadAngle);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Negatron Gun", EditorStyles.boldLabel);
        thisWeaponManager.NegatronGun = (GameObject)EditorGUILayout.ObjectField("Negatron Gun", thisWeaponManager.NegatronGun, typeof(GameObject), false);
        thisWeaponManager.NegatronProjectile = (GameObject)EditorGUILayout.ObjectField("Projectile", thisWeaponManager.NegatronProjectile, typeof(GameObject), false);
        thisWeaponManager.NegatronHitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Effect", thisWeaponManager.NegatronHitEffect, typeof(GameObject), false);
        thisWeaponManager.NegatronDamage = EditorGUILayout.IntField("Damage", thisWeaponManager.NegatronDamage);
        thisWeaponManager.NegatronFireDelay = EditorGUILayout.FloatField("Fire Delay", thisWeaponManager.NegatronFireDelay);
        thisWeaponManager.NegatronProjectileSpeed = EditorGUILayout.FloatField("Projectile Speed", thisWeaponManager.NegatronProjectileSpeed);
        thisWeaponManager.NegatronShape = EditorGUILayout.IntField("Shape", thisWeaponManager.NegatronShape);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Disintegrator", EditorStyles.boldLabel);
        thisWeaponManager.Disintegrator = (GameObject)EditorGUILayout.ObjectField("Disintegrator", thisWeaponManager.Disintegrator, typeof(GameObject), false);
        thisWeaponManager.DisintegratorProjectile = (GameObject)EditorGUILayout.ObjectField("Projectile", thisWeaponManager.DisintegratorProjectile, typeof(GameObject), false);
        thisWeaponManager.DisintegratorHitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Effect", thisWeaponManager.DisintegratorHitEffect, typeof(GameObject), false);
        thisWeaponManager.DisintegratorDamage = EditorGUILayout.IntField("Damage", thisWeaponManager.DisintegratorDamage);
        thisWeaponManager.DisintegratorFireDelay = EditorGUILayout.FloatField("Fire Delay", thisWeaponManager.DisintegratorFireDelay);
        thisWeaponManager.DisintegratorProjectileSpeed = EditorGUILayout.FloatField("Projectile Speed", thisWeaponManager.DisintegratorProjectileSpeed);
        thisWeaponManager.DisintegratorAcceleration = EditorGUILayout.FloatField("Projectile Acceleration", thisWeaponManager.DisintegratorAcceleration);
        thisWeaponManager.DisintegratorBlastRadius = EditorGUILayout.FloatField("Blast Radius", thisWeaponManager.DisintegratorBlastRadius);
        thisWeaponManager.DisintegratorHoming = EditorGUILayout.Toggle("Homing", thisWeaponManager.DisintegratorHoming);
        thisWeaponManager.DisintegratorHomingRadius = EditorGUILayout.FloatField("Homing Radius", thisWeaponManager.DisintegratorHomingRadius);
        thisWeaponManager.DisintegratorHomingAcceleration = EditorGUILayout.FloatField("Homing Acceleration", thisWeaponManager.DisintegratorHomingAcceleration);
        thisWeaponManager.DisintegratorRotationDamping = EditorGUILayout.FloatField("Rotation Damping", thisWeaponManager.DisintegratorRotationDamping);
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Multizapper", EditorStyles.boldLabel);
        thisWeaponManager.Multizapper = (GameObject)EditorGUILayout.ObjectField("Multizapper", thisWeaponManager.Multizapper, typeof(GameObject), false);
        thisWeaponManager.MultizapperBall = (GameObject)EditorGUILayout.ObjectField("Ball", thisWeaponManager.MultizapperBall, typeof(GameObject), false);
        thisWeaponManager.MultizapperBallOfSteelBall = (GameObject)EditorGUILayout.ObjectField("Ball of Steel", thisWeaponManager.MultizapperBallOfSteelBall, typeof(GameObject), false);
        thisWeaponManager.MultizapperZap = (GameObject)EditorGUILayout.ObjectField("Zap", thisWeaponManager.MultizapperZap, typeof(GameObject), false);
        thisWeaponManager.MultizapperHitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Effect", thisWeaponManager.MultizapperHitEffect, typeof(GameObject), false);
        thisWeaponManager.MultizapperBallOfSteelHitEffect = (GameObject)EditorGUILayout.ObjectField("BoS Hit Effect", thisWeaponManager.MultizapperBallOfSteelHitEffect, typeof(GameObject), false);
        thisWeaponManager.MultizapperBallSpeed = EditorGUILayout.FloatField("Ball Speed", thisWeaponManager.MultizapperBallSpeed);
        thisWeaponManager.MultizapperBallDamage = EditorGUILayout.IntField("Ball Damage", thisWeaponManager.MultizapperBallDamage);
        thisWeaponManager.MultizapperBallOfSteelDamage = EditorGUILayout.IntField("Ball of Steel Damage", thisWeaponManager.MultizapperBallOfSteelDamage);
        thisWeaponManager.MultizapperZapRange = EditorGUILayout.FloatField("Zap Range", thisWeaponManager.MultizapperZapRange);
        thisWeaponManager.MultizapperZapSpeed = EditorGUILayout.FloatField("Zap Speed", thisWeaponManager.MultizapperZapSpeed);
        thisWeaponManager.MultizapperZapDamage = EditorGUILayout.IntField("Zap Damage", thisWeaponManager.MultizapperZapDamage);
        thisWeaponManager.MultizapperFireDelay = EditorGUILayout.FloatField("Zap Delay", thisWeaponManager.MultizapperFireDelay);
        thisWeaponManager.MultizapperChainRange = EditorGUILayout.FloatField("Chain Range", thisWeaponManager.MultizapperChainRange);
        thisWeaponManager.MultizapperNumberOfChains = EditorGUILayout.IntField("Number of Chains", thisWeaponManager.MultizapperNumberOfChains);
        thisWeaponManager.MultizapperHasTether = EditorGUILayout.Toggle("Tether", thisWeaponManager.MultizapperHasTether);
        thisWeaponManager.MultizapperHasBallOfSteel = EditorGUILayout.Toggle("Ball of Steel", thisWeaponManager.MultizapperHasBallOfSteel);
        
        EditorGUILayout.Separator();

        EditorGUILayout.LabelField("Factor Beam", EditorStyles.boldLabel);
        thisWeaponManager.FactorBeam = (GameObject)EditorGUILayout.ObjectField("Factor Beam", thisWeaponManager.FactorBeam, typeof(GameObject), false);
        thisWeaponManager.FactorBeamProjectile = (GameObject)EditorGUILayout.ObjectField("Projectile", thisWeaponManager.FactorBeamProjectile, typeof(GameObject), false);
        thisWeaponManager.FactorBeamHitEffect = (GameObject)EditorGUILayout.ObjectField("Hit Effect", thisWeaponManager.FactorBeamHitEffect, typeof(GameObject), false);
        thisWeaponManager.FactorBeamDamage = EditorGUILayout.IntField("Damage", thisWeaponManager.FactorBeamDamage);
        thisWeaponManager.FactorBeamFireDelay = EditorGUILayout.FloatField("Fire Delay", thisWeaponManager.FactorBeamFireDelay);
        thisWeaponManager.FactorBeamProjectileSpeed = EditorGUILayout.FloatField("Projectile Speed", thisWeaponManager.FactorBeamProjectileSpeed);
        thisWeaponManager.FactorBeamHasTractor = EditorGUILayout.Toggle("Tractor", thisWeaponManager.FactorBeamHasTractor);
        thisWeaponManager.FactorBeamJuiceDrain = EditorGUILayout.IntField("Juice Cost", thisWeaponManager.FactorBeamJuiceDrain);
        thisWeaponManager.FactorBeamTractorJuiceDrain = EditorGUILayout.IntField("Tractor Juice Cost", thisWeaponManager.FactorBeamTractorJuiceDrain);
        thisWeaponManager.FactorBeamTractorHoldEffect = (GameObject)EditorGUILayout.ObjectField("Tractor Hold Effect", thisWeaponManager.FactorBeamTractorHoldEffect, typeof(GameObject), false);
        thisWeaponManager.FactorBeamTractorBumpDamage = EditorGUILayout.IntField("Tractor Bump Damage", thisWeaponManager.FactorBeamTractorBumpDamage);
        EditorGUILayout.Separator();


        EditorUtility.SetDirty(thisWeaponManager);
    }

}
