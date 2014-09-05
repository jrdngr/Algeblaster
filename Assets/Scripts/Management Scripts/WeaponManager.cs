using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponManager : MonoBehaviour {

    //Positron Gun
    [SerializeField] private GameObject positronGun;
    public GameObject PositronGun {
        get { return positronGun; }
        set { positronGun = value; }
    }
    [SerializeField] private int positronDamage;
    public int PositronDamage {
        get { return positronDamage; }
        set { positronDamage = value; }
    }
    [SerializeField] private float positronFireDelay;
    public float PositronFireDelay {
        get { return positronFireDelay; }
        set { positronFireDelay = value; }
    }
    [SerializeField] private float positronProjectileSpeed;
    public float PositronProjectileSpeed {
        get { return positronProjectileSpeed; }
        set { positronProjectileSpeed = value; }
    }
    [SerializeField] private GameObject positronProjectile;
    public GameObject PositronProjectile {
        get { return positronProjectile; }
        set { positronProjectile = value; }
    }
    [SerializeField] private GameObject positronHitEffect;
    public GameObject PositronHitEffect {
        get { return positronHitEffect; }
        set { positronHitEffect = value; }
    }
    [SerializeField] private bool positronHasSpread;
    public bool PositronHasSpread {
        get { return positronHasSpread; }
        set { positronHasSpread = value; }
    }
    [SerializeField] private int positronNumberOfBullets;
    public int PositronNumberOfBullets {
        get { return positronNumberOfBullets; }
        set { positronNumberOfBullets = value; }
    }
    [SerializeField] private float positronTwoBulletsSpawnOffset;
    public float PositronTwoBulletsSpawnOffset {
        get { return positronTwoBulletsSpawnOffset; }
        set { positronTwoBulletsSpawnOffset = value; }
    }
    [SerializeField] private float positronSpreadAngle;
    public float PositronSpreadAngle {
        get { return positronSpreadAngle; }
        set { positronSpreadAngle = value; }
    }

    //Negatron Gun
    [SerializeField] private GameObject negatronGun;
    public GameObject NegatronGun{
        get { return negatronGun; }
        set { negatronGun = value; }
    }
    [SerializeField] private int negatronDamage;
    public int NegatronDamage {
        get { return negatronDamage; }
        set { negatronDamage = value; }
    }
    [SerializeField] private float negatronFireDelay;
    public float NegatronFireDelay {
        get { return negatronFireDelay; }
        set { negatronFireDelay = value; }
    }
    [SerializeField] float negatronProjectileSpeed;
    public float NegatronProjectileSpeed {
        get { return negatronProjectileSpeed; }
        set { negatronProjectileSpeed = value; }
    }
    [SerializeField] private GameObject negatronProjectile;
    public GameObject NegatronProjectile {
        get { return negatronProjectile; }
        set { negatronProjectile = value; }
    }
    [SerializeField] private GameObject negatronHitEffect;
    public GameObject NegatronHitEffect {
        get { return negatronHitEffect; }
        set { negatronHitEffect = value; }
    }
    [SerializeField] private int negatronShape;
    public int NegatronShape {
        get { return negatronShape; }
        set { negatronShape = value; }
    }


    //Disintegrator
    [SerializeField] private GameObject disintegrator;
    public GameObject Disintegrator {
        get { return disintegrator; }
        set { disintegrator = value; }
    }
    [SerializeField] private int disintegratorDamage;
    public int DisintegratorDamage {
        get { return disintegratorDamage; }
        set { disintegratorDamage = value; }
    }
    [SerializeField] private float disintegratorFireDelay;
    public float DisintegratorFireDelay {
        get { return disintegratorFireDelay; }
        set { disintegratorFireDelay = value; }
    }
    [SerializeField] private float disintegratorProjectileSpeed;
    public float DisintegratorProjectileSpeed {
        get { return disintegratorProjectileSpeed; }
        set { disintegratorProjectileSpeed = value; }
    }
    [SerializeField] private GameObject disintegratorProjectile;
    public GameObject DisintegratorProjectile {
        get { return disintegratorProjectile; }
        set { disintegratorProjectile = value; }
    }
    [SerializeField] private GameObject disintegratorHitEffect;
    public GameObject DisintegratorHitEffect {
        get { return disintegratorHitEffect; }
        set { disintegratorHitEffect = value; }
    }
    [SerializeField] private float disintegratorAcceleration;
    public float DisintegratorAcceleration {
        get { return disintegratorAcceleration; }
        set { disintegratorAcceleration = value; }
    }
    [SerializeField] private bool disintegratorHoming;
    public bool DisintegratorHoming {
        get { return disintegratorHoming; }
        set { disintegratorHoming = value; }
    }
    [SerializeField] private float disintegratorHomingRadius;
    public float DisintegratorHomingRadius {
        get { return disintegratorHomingRadius; }
        set { disintegratorHomingRadius = value; }
    }
    [SerializeField] private float disintegratorHomingAcceleration;
    public float DisintegratorHomingAcceleration {
        get { return disintegratorHomingAcceleration; }
        set { disintegratorHomingAcceleration = value; }
    }
    [SerializeField] private float disintegratorBlastRadius;
    public float DisintegratorBlastRadius {
        get { return disintegratorBlastRadius; }
        set { disintegratorBlastRadius = value; }
    }
    [SerializeField] private float disintegratorRotationDamping;
    public float DisintegratorRotationDamping{
        get { return disintegratorRotationDamping; }
        set { disintegratorRotationDamping = value; }
    }

    //Multizapper
    [SerializeField] private GameObject multizapper;
    public GameObject Multizapper {
        get { return multizapper; }
        set { multizapper = value; }
    }
    [SerializeField] private float multizapperFireDelay;
    public float MultizapperFireDelay {
        get { return multizapperFireDelay; }
        set { multizapperFireDelay = value; }
    }
    [SerializeField] private GameObject multizapperHitEffect;
    public GameObject MultizapperHitEffect {
        get { return multizapperHitEffect; }
        set { multizapperHitEffect = value; }
    }
    [SerializeField] private GameObject multizapperBall;
    public GameObject MultizapperBall {
        get { return multizapperBall; }
        set { multizapperBall = value; }
    }
    [SerializeField] private GameObject multizapperZap;
    public GameObject MultizapperZap {
        get { return multizapperZap; }
        set { multizapperZap = value; }
    }
    [SerializeField] private float multizapperBallSpeed;
    public float MultizapperBallSpeed {
        get { return multizapperBallSpeed; }
        set { multizapperBallSpeed = value; }
    }
    [SerializeField] private int multizapperBallDamage;
    public int MultizapperBallDamage {
        get { return multizapperBallDamage; }
        set { multizapperBallDamage = value; }
    }
    [SerializeField] private float multizapperZapRange;
    public float MultizapperZapRange {
        get { return multizapperZapRange; }
        set { multizapperZapRange = value; }
    }
    [SerializeField] private float multizapperZapSpeed;
    public float MultizapperZapSpeed {
        get { return multizapperZapSpeed; }
        set { multizapperZapSpeed = value; }
    }
    [SerializeField] private int multizapperZapDamage;
    public int MultizapperZapDamage {
        get { return multizapperZapDamage; }
        set { multizapperZapDamage = value; }
    }
    [SerializeField] private float multizapperChainRange;
    public float MultizapperChainRange {
        get { return multizapperChainRange; }
        set { multizapperChainRange = value; }
    }
    [SerializeField] private int multizapperNumberOfChains;
    public int MultizapperNumberOfChains {
        get { return multizapperNumberOfChains; }
        set { multizapperNumberOfChains = value; }
    }
    [SerializeField] private bool multizapperHasTether;
    public bool MultizapperHasTether {
        get { return multizapperHasTether; }
        set { multizapperHasTether = value; }
    }
    [SerializeField] private bool multizapperHasBallOfSteel;
    public bool MultizapperHasBallOfSteel {
        get { return multizapperHasBallOfSteel; }
        set { multizapperHasBallOfSteel = value; }
    }
    [SerializeField] private GameObject multizapperBallOfSteelBall;
    public GameObject MultizapperBallOfSteelBall {
        get { return multizapperBallOfSteelBall; }
        set { multizapperBallOfSteelBall = value; }
    }
    [SerializeField] private GameObject multizapperBallOfSteelHitEffect;
    public GameObject MultizapperBallOfSteelHitEffect {
        get { return multizapperBallOfSteelHitEffect; }
        set { multizapperBallOfSteelHitEffect = value; }
    }
    [SerializeField] private int multizapperBallOfSteelDamage;
    public int MultizapperBallOfSteelDamage {
        get { return multizapperBallOfSteelDamage; }
        set { multizapperBallOfSteelDamage = value; }
    }



    //Factor Bream
    [SerializeField] private GameObject factorBeam;
    public GameObject FactorBeam {
        get { return factorBeam; }
        set { factorBeam = value; }
    }
    [SerializeField] private int factorBeamDamage;
    public int FactorBeamDamage {
        get { return factorBeamDamage; }
        set { factorBeamDamage = value; }
    }
    [SerializeField] private float factorBeamFireDelay;
    public float FactorBeamFireDelay {
        get { return factorBeamFireDelay; }
        set { factorBeamFireDelay = value; }
    }
    [SerializeField] private GameObject factorBeamHitEffect;
    public GameObject FactorBeamHitEffect {
        get { return factorBeamHitEffect; }
        set { factorBeamHitEffect = value; }
    }
    [SerializeField] private GameObject factorBeamProjectile;
    public GameObject FactorBeamProjectile {
        get { return factorBeamProjectile; }
        set { factorBeamProjectile = value; }
    }
    [SerializeField] private float factorBeamProjectileSpeed;
    public float FactorBeamProjectileSpeed {
        get { return factorBeamProjectileSpeed; }
        set { factorBeamProjectileSpeed = value; }
    }
    [SerializeField] private bool factorBeamHasTractor;
    public bool FactorBeamHasTractor {
        get { return factorBeamHasTractor; }
        set { factorBeamHasTractor = value; }
    }
    [SerializeField] private int factorBeamJuiceDrain;
    public int FactorBeamJuiceDrain {
        get { return factorBeamJuiceDrain; }
        set { factorBeamJuiceDrain = value; }
    }
    [SerializeField] private int factorBeamTractorJuiceDrain;
    public int FactorBeamTractorJuiceDrain {
        get { return factorBeamTractorJuiceDrain; }
        set { factorBeamTractorJuiceDrain = value; }
    }
    [SerializeField] private GameObject factorBeamTractorHoldEffect;
    public GameObject FactorBeamTractorHoldEffect {
        get { return factorBeamTractorHoldEffect; }
        set { factorBeamTractorHoldEffect = value; }
    }
    [SerializeField] private int factorBeamTractorBumpDamage;
    public int FactorBeamTractorBumpDamage {
        get { return factorBeamTractorBumpDamage; }
        set { factorBeamTractorBumpDamage = value; }
    }

/*    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }*/

}
