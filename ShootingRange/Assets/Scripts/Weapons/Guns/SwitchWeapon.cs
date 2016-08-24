using UnityEngine;
using System.Collections;

//this script allows gun switching

public class SwitchWeapon : MonoBehaviour 
{	
	public RaycastShooting getRaycastScript;
	public GameObject rifle;
	public GameObject pistol;
	public bool isHoldingWeapon = false;
	
	// Update is called once per frame
	void Update () 
	{
		//clicking button allows switching
		if (Input.GetButtonDown ("Switch Weapon")) 
		{
			isHoldingWeapon = !isHoldingWeapon;//a toggle between rifle and pistol
			rifle.SetActive (!isHoldingWeapon);//one is off than the other is on
			pistol.SetActive (isHoldingWeapon);
		}

		getRaycastScript.displayMagazineCount ();//display new magazine count
		getRaycastScript.displayammunitionCount ();//display new ammo count
	}
}
