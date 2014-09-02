using UnityEngine;
using System.Collections;

// Manages the weapon panel at the bottom center of the GUI
public class oldWeaponMgr : MonoBehaviour {

	private int selectedWeapon = 1;

	[SerializeField] private GameObject weaponTag1;
	[SerializeField] private GameObject w1Up;
	[SerializeField] private GameObject w1Down;
	[SerializeField] private GameObject weaponTag2;
	[SerializeField] private GameObject w2Up;
	[SerializeField] private GameObject w2Down;
	[SerializeField] private GameObject weaponTag3;
	[SerializeField] private GameObject w3Up;
	[SerializeField] private GameObject w3Down;
	[SerializeField] private GameObject weaponTag4;
	[SerializeField] private GameObject w4Up;
	[SerializeField] private GameObject w4Down;
	[SerializeField] private GameObject weaponTag5;
	[SerializeField] private GameObject w5Up;
	[SerializeField] private GameObject w5Down;

	void Start(){
		weaponTag1.renderer.sortingLayerName = "GUI Top";
		weaponTag1.renderer.sortingOrder = 1;
		weaponTag2.renderer.sortingLayerName = "GUI Top";
		weaponTag2.renderer.sortingOrder = 1;
		weaponTag3.renderer.sortingLayerName = "GUI Top";
		weaponTag3.renderer.sortingOrder = 1;
		weaponTag4.renderer.sortingLayerName = "GUI Top";
		weaponTag4.renderer.sortingOrder = 1;
		weaponTag5.renderer.sortingLayerName = "GUI Top";
		weaponTag5.renderer.sortingOrder = 1;
	}

	// Moves weapon tags up and down based on whether or not they are selected
	public void ChangeWeapon(int weap){
		selectedWeapon = weap;
		if (selectedWeapon == 1)
			weaponTag1.transform.position = w1Up.transform.position;
		else
			weaponTag1.transform.position = w1Down.transform.position;
		if (selectedWeapon == 2)
			weaponTag2.transform.position = w2Up.transform.position;
		else
			weaponTag2.transform.position = w2Down.transform.position;
		if (selectedWeapon == 3)
			weaponTag3.transform.position = w3Up.transform.position;
		else
			weaponTag3.transform.position = w3Down.transform.position;
		if (selectedWeapon == 4)
			weaponTag4.transform.position = w4Up.transform.position;
		else
			weaponTag4.transform.position = w4Down.transform.position;
		if (selectedWeapon == 5)
			weaponTag5.transform.position = w5Up.transform.position;
		else
			weaponTag5.transform.position = w5Down.transform.position;

	}

}
