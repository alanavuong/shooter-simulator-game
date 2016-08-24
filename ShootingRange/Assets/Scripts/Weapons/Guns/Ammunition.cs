using UnityEngine;
using System.Collections;

//this script controls quanity of ammo picked up

public class Ammunition : MonoBehaviour 
{

	public RaycastShooting getRaycastScript;//access to raycast script
	public int numberOfClips;//the number of clip picked up
	private bool excessiveAmmo = false;//boolean determine whether to destroy ammo if at max ammo

	//refill upon touching ammunition box
	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			excessiveAmmo = getRaycastScript.refillAmmunition (numberOfClips, false);//refill ammo from another script and determine boolean flag
			//if player has ammo space than destroy this object
			if(excessiveAmmo == true)
			{
				Destroy (gameObject);
			}
		}
	}

}
