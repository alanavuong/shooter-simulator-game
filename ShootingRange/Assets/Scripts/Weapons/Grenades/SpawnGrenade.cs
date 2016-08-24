using UnityEngine;
using System.Collections;

//this script will spawn a grenade in front of the player screen

public class SpawnGrenade : MonoBehaviour 
{	
	public GameObject grenade;

	void Update () 
	{
		if (Input.GetButtonDown ("Grenade")) 
		{
			Instantiate (grenade, transform.position, Quaternion.identity);
		}
	}
}
