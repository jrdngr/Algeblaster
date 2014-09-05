using UnityEngine;
using System.Collections;

public class playerWeaponManager : MonoBehaviour {

    //Frequency management
    const int frequencyMin = 2;
    const int frequencyMax = 9;
    const int colorMin = 0;
    const int colorMax = 2;
    private int currentFrequency = 2;
    private int currentColor = 0;
    private PlayerManager playerManager;
    private TextMesh frequencyDisplay;
    private PlayerManager.FrequencyModes myFrequencyMode;
    private Renderer colorDisplay;
    
    //Weapon management
    private int currentWeapon = 0;
    private Weapon[] attachedWeapons = new Weapon[5];
    private WeaponManager weaponManager;
    private TextMesh weaponDisplay;
    private GameObject weaponColorGlow;
    public GameObject WeaponColorGlow {
        get { return weaponColorGlow; }
        set { weaponColorGlow = value; }
    }

    //Public properties
    public int Frequency {
        get { return currentFrequency; }
    }
    public int CurrentColor {
        get { return currentColor; }
    }

    void Awake() {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        myFrequencyMode = playerManager.FrequencyMode;
        frequencyDisplay = playerManager.FrequencyDisplay.GetComponent<TextMesh>();
        colorDisplay = playerManager.ColorDisplay.GetComponent<Renderer>();
        weaponDisplay = playerManager.WeaponDisplay.GetComponent<TextMesh>();
        weaponColorGlow = playerManager.WeaponColorGlow;
        weaponColorGlow.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1, 0.90f);
        if (playerManager.HasPositron) {
            GameObject newWeapon = (GameObject)Instantiate(weaponManager.PositronGun, transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            attachedWeapons[0] = newWeapon.GetComponent<Weapon>();
        }
        if (playerManager.HasNegatron) {
            GameObject newWeapon = (GameObject)Instantiate(weaponManager.NegatronGun, transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            attachedWeapons[1] = newWeapon.GetComponent<Weapon>();
        }
        if (playerManager.HasDisintegrator) {
            GameObject newWeapon = (GameObject)Instantiate(weaponManager.Disintegrator, transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            attachedWeapons[2] = newWeapon.GetComponent<Weapon>();
        }
        if (playerManager.HasMultizapper) {
            GameObject newWeapon = (GameObject)Instantiate(weaponManager.Multizapper, transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            attachedWeapons[3] = newWeapon.GetComponent<Weapon>();
        }
        if (playerManager.HasFactorBeam) {
            GameObject newWeapon = (GameObject)Instantiate(weaponManager.FactorBeam, transform.position, Quaternion.identity);
            newWeapon.transform.parent = transform;
            attachedWeapons[4] = newWeapon.GetComponent<Weapon>();
        }
    }

    public void Fire() {
        attachedWeapons[currentWeapon].Fire();
    }

    public void ChangeFrequency(bool moveUp) {
        //Number mode
        if (myFrequencyMode != PlayerManager.FrequencyModes.Color) {
            if (moveUp)
                currentFrequency++;
            else
                currentFrequency--;
            currentFrequency = Mathf.Clamp(currentFrequency, frequencyMin, frequencyMax);
            frequencyDisplay.text = currentFrequency.ToString();
        }
        
        //Color mode 
        else {
            if (moveUp)
                currentColor++;
            else
                currentColor--;
            currentColor = Mathf.Clamp(currentColor, colorMin, colorMax);
            switch (currentColor) {
                case 0:
                    colorDisplay.material.color = Color.blue;
                    weaponColorGlow.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1, 0.90f);
                    break;
                case 1:
                    colorDisplay.material.color = Color.red;
                    weaponColorGlow.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 0, 0.5f);
                    break;
                case 2:
                    colorDisplay.material.color = Color.yellow;
                    weaponColorGlow.GetComponent<ParticleSystem>().startColor = new Color(1, 1 ,0, 0.5f);
                    break;
                default:
                    colorDisplay.material.color = Color.blue;
                    weaponColorGlow.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1, 0.75f);
                    break;
            }
        }
    }

    public void ChangeWeapon(int selection) {
        if (attachedWeapons[selection]) {
            currentWeapon = selection;
            weaponDisplay.text = (currentWeapon + 1).ToString();
        }
    }

    public void SwapWeapon(bool moveUp) {
        if (moveUp && attachedWeapons[currentWeapon + 1])
            currentWeapon++;
        else if (attachedWeapons[currentWeapon - 1])
            currentWeapon--;
        weaponDisplay.text = currentWeapon.ToString();
    }

}
