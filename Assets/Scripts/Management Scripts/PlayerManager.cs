using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerManager : MonoBehaviour {

    public enum FrequencyModes { Color, ThreeNumber, FiveNumber, NineNumber };

    //Movement
    [SerializeField] private float maxSpeed;
    public float MaxSpeed {
        get {
            return maxSpeed; }
        set {
            maxSpeed = value;
            if (maxSpeed < 0)
                maxSpeed = 0;
        }
    }
    [SerializeField] private float dashForce;
    public float DashForce {
        get { return dashForce; }

        set {
            dashForce = value;
            if (dashForce < 0)
                dashForce = 0;
        }

    }
    [SerializeField] private float dashTime;
    public float DashTime {
        get { return dashTime; }
        set { dashTime = value; }
    }
    [SerializeField] private float dashDelay;
    public float DashDelay {
        get { return dashDelay; }
        set {
            dashDelay = value;
            if (dashDelay < 0)
                dashDelay = 0;
        }
    }
    [SerializeField] private float dashDoubleTapDelay;
    public float DashDoubleTapDelay {
        get { return dashDoubleTapDelay; }
        set { dashDoubleTapDelay = value; }
    }
    [SerializeField] private float thrustForce;
    public float ThrustForce {
        get { return thrustForce; }
        set { thrustForce = value; }
    }
    [SerializeField] private GameObject dashEffect;
    public GameObject DashEffect {
        get { return dashEffect; }
        set { dashEffect = value; }
    }
    [SerializeField] private float bumpForce;
    public float BumpForce {
        get { return bumpForce; }
        set { bumpForce = value; }
    }
    [SerializeField] private float bumpTime;
    public float BumpTime {
        get { return bumpTime; }
        set { bumpTime = value; }
    }
    [SerializeField] private GameObject bumpEffect;
    public GameObject BumpEffect {
        get { return bumpEffect; }
        set { bumpEffect = value; }
    }


    //Shield
    [SerializeField] private int juiceCost;
    public int JuiceCost {
        get { return juiceCost; }
        set {
            juiceCost = value;
            if (juiceCost < 0)
                juiceCost = 0;
        } 
    }
    [SerializeField] private float juiceDrainTickTime;
    public float JuiceDrainTickTime {
        get { return juiceDrainTickTime; }
        set { juiceDrainTickTime = value; }
    }
    [SerializeField] private float emptyJuiceDelay;
    public float EmptyJuiceDelay {
        get { return emptyJuiceDelay; }
        set { emptyJuiceDelay = value; }
    }


    //Health
    [SerializeField] private int maxHitpoints;
    public int MaxHitpoints {
        get { return maxHitpoints; }
        set {
            maxHitpoints = value;
            if (maxHitpoints < 0)
                maxHitpoints = 0;
        }
    }
    [SerializeField] private int bumpDamage;
    public int BumpDamage {
        get { return bumpDamage; }
        set { bumpDamage = value; }
    }
    [SerializeField] private GameObject playerDeathEffect;
    public GameObject PlayerDeathEffect {
        get { return playerDeathEffect; }
        set { playerDeathEffect = value; }
    }
    [SerializeField] private GameObject healthBar;
    public GameObject HealthBar {
        get { return healthBar; }
        set { healthBar = value; }
    }

    //Juice
    [SerializeField] private int maxJuice;
    public int MaxJuice {
        get { return maxJuice; }
        set { maxJuice = value; }
    }
    [SerializeField] private float juiceRefillSpeed;
    public float JuiceRefillSpeed {
        get { return juiceRefillSpeed; }
        set { juiceRefillSpeed = value; }
    }
    [SerializeField] private GameObject juiceBar;
    public GameObject JuiceBar {
        get { return juiceBar; }
        set { juiceBar = value; }
    }

    //Weapon Management
    [SerializeField] private GameObject frequencyDisplay;
    public GameObject FrequencyDisplay {
        get { return frequencyDisplay; }
        set { frequencyDisplay = value; }
    }
    [SerializeField] private GameObject weaponDisplay;
    public GameObject WeaponDisplay {
        get { return weaponDisplay; }
        set { weaponDisplay = value; }
    }
    [SerializeField] private GameObject colorDisplay;
    public GameObject ColorDisplay {
        get { return colorDisplay; }
        set { colorDisplay = value; }
    }
    [SerializeField] private GameObject weaponColorGlow;
    public GameObject WeaponColorGlow {
        get { return weaponColorGlow; }
        set { weaponColorGlow = value; }
    }

    //Unlocked Weapons
    [SerializeField] private bool hasPositron;
    public bool HasPositron {
        get { return hasPositron; }
        set { hasPositron = value; }
    }
    [SerializeField] private bool hasNegatron;
    public bool HasNegatron {
        get { return hasNegatron; }
        set { hasNegatron = value; }
    }
    [SerializeField] private bool hasDisintegrator;
    public bool HasDisintegrator {
        get { return hasDisintegrator; }
        set { hasDisintegrator = value; }
    }
    [SerializeField] private bool hasMultizapper;
    public bool HasMultizapper {
        get { return hasMultizapper; }
        set { hasMultizapper = value; }
    }
    [SerializeField] private bool hasFactorBeam;
    public bool HasFactorBeam {
        get { return hasFactorBeam; }
        set { hasFactorBeam = value; }
    }

    //Player Progression
    [SerializeField] private FrequencyModes frequencyMode;
    public FrequencyModes FrequencyMode {
        get { return frequencyMode; }
        set { frequencyMode = value; }
    }
    [SerializeField] private int credits;
    public int Credits {
        get { return credits; }
        set { credits = value; }
    }
    
    //Shield Upgrades
    [SerializeField] private bool hasShieldMagnet;
    public bool HasShieldMagnet {
        get { return hasShieldMagnet; }
        set { hasShieldMagnet = value; }
    }
    [SerializeField] private float shieldMagnetForce;
    public float ShieldMagnetForce {
        get { return shieldMagnetForce; }
        set { shieldMagnetForce = value; }
    }
    [SerializeField] private float shieldMagnetRadius;
    public float ShieldMagnetRadius {
        get { return shieldMagnetRadius; }
        set { shieldMagnetRadius = value; }
    }

/*    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }*/

}
