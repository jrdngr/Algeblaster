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
    [SerializeField] private float disintegratorBlastRadius;
    public float DisintegratorBlastRadius {
        get { return disintegratorBlastRadius; }
        set { disintegratorBlastRadius = value; }
    }


    //Multizapper
    [SerializeField] private GameObject multizapper;
    public GameObject Multizapper {
        get { return multizapper; }
        set { multizapper = value; }
    }
    [SerializeField] private int multizapperDamage;
    public int MultizapperDamage{
        get { return multizapperDamage; }
        set { multizapperDamage = value; }
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

    void Awake() {
//        DontDestroyOnLoad(transform.gameObject);
    }

}
