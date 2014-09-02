using UnityEngine;
using System.Collections;

public class playerInputManager : MonoBehaviour {

    private playerMovementManager moveManager;
    private playerShieldManager shieldManager;
    private playerWeaponManager weaponManager;

    void Awake() {
        moveManager = GetComponent<playerMovementManager>();
        shieldManager = GetComponent<playerShieldManager>();
        weaponManager = GetComponent<playerWeaponManager>();
    }

	void Update () {
        //Movement
        moveManager.XMovement = Input.GetAxis("Horizontal");
        moveManager.YMovement = Input.GetAxis("Vertical");

        //Dashing
        if (Input.GetButtonDown("DashLeft"))
            moveManager.StartDash(playerMovementManager.DashDirection.left);
        if (Input.GetButtonDown("DashRight"))
            moveManager.StartDash(playerMovementManager.DashDirection.right);        

        //Shield
        if (Input.GetButton("Shield"))
            shieldManager.ShieldOn();
        if (Input.GetButtonUp("Shield"))
            shieldManager.ShieldOff();
        
        //Frequency
        if (Input.GetButtonDown("ChangeFreqUp"))
            weaponManager.ChangeFrequency(true);
        if (Input.GetButtonDown("ChangeFreqDown"))
            weaponManager.ChangeFrequency(false);

        //Weapon Selection
        if (Input.GetButtonDown("Weapon1"))
            weaponManager.ChangeWeapon(0);
        if (Input.GetButtonDown("Weapon2"))
            weaponManager.ChangeWeapon(1);
        if (Input.GetButtonDown("Weapon3"))
            weaponManager.ChangeWeapon(2);
        if (Input.GetButtonDown("Weapon4"))
            weaponManager.ChangeWeapon(3);
        if (Input.GetButtonDown("Weapon5"))
            weaponManager.ChangeWeapon(4);
        if (Input.GetButtonDown("WeaponSwapUp"))
            weaponManager.SwapWeapon(true);
        if (Input.GetButtonDown("WeaponSwapDown"))
            weaponManager.SwapWeapon(false);

        //Weapon Firing
        if (Input.GetButton("Fire1"))
            weaponManager.Fire();             
	}
}
