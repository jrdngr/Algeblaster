using UnityEngine;
using System.Collections;

public class playerWeaponManager : MonoBehaviour {

    //Frequency management
    const int frequencyMin = 2;
    const int frequencyMax = 9;
    private int currentFrequency = 2;
    private PlayerManager playerManager;
    private TextMesh frequencyDisplay;
    
    //Weapon management
    private int currentWeapon = 0;
    private Weapon[] attachedWeapons = new Weapon[5];
    private WeaponManager weaponManager;
    private TextMesh weaponDisplay;

    //Public properties
    public int Frequency {
        get { return currentFrequency; }
    }

    void Awake() {
        playerManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<PlayerManager>();
        weaponManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<WeaponManager>();
        frequencyDisplay = playerManager.FrequencyDisplay.GetComponent<TextMesh>();
        weaponDisplay = playerManager.WeaponDisplay.GetComponent<TextMesh>();
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
    }

    public void Fire() {
        attachedWeapons[currentWeapon].Fire();
    }

    public void ChangeFrequency(bool moveUp) {
        if (moveUp)
            currentFrequency++;
        else
            currentFrequency--;
        currentFrequency = Mathf.Clamp(currentFrequency, frequencyMin, frequencyMax);
        frequencyDisplay.text = currentFrequency.ToString();
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
