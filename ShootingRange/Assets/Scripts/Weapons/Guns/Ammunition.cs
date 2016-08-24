using UnityEngine;
using System.Collections;

public class Ammunition : MonoBehaviour {

	//public RaycastShooting getRaycastScript;//access to raycast script
	public int numberOfClips;//the number of clip picked up
	private bool excessiveAmmo = false;//boolean determine whether to destroy ammo if at max ammo

	//this script controls quanity of ammo picked up

	//refill some number of clips
	void OnTriggerStay(Collider other){
		if (other.tag == "Player") {//detect if player can pick it up automatically
			//excessiveAmmo = getRaycastScript.refillAmmunition (numberOfClips, false);//refill ammo from another script and determine boolean flag
			if(excessiveAmmo == true){//if player can fit ammo than destroy this item
				Destroy (gameObject);
			}
		}
	}
}
