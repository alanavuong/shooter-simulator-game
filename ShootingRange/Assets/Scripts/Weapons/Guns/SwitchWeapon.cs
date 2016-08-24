using UnityEngine;
using System.Collections;

public class SwitchWeapon : MonoBehaviour {

	//this script allows gun switching
	public bool isHoldingWeapon = false;
	public RaycastShooting getRaycastScript;
	public GameObject rifle;
	public GameObject pistol;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Switch Weapon")) {//clicking button allows switching
			print ("clicked!");
			isHoldingWeapon = !isHoldingWeapon;//turn boolean to true/false 
			rifle.SetActive (!isHoldingWeapon);//one is off than the other is on
			pistol.SetActive (isHoldingWeapon);
		}

		getRaycastScript.displayMagazineCount ();//subtract from ammunation count
		getRaycastScript.displayammunitionCount ();//display ammo count
	}
}
