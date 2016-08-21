using UnityEngine;
using System.Collections;

public class GunMechanic : MonoBehaviour {
	
	public GameObject bullet;//
	public Transform bulletSpawn;//bullet will spawn here
	public float rateOfFire;//how fast the gun shoots
	
	private float bulletWaitTime;//how long it takes for the next shot to be shot
	/*
	// Use this for initialization
	void Start () {
	}
*/	
	// Update is called once per frame
	void Update () {
		if ((Input.GetButton ("Fire1")) && (Time.time > bulletWaitTime)) {//input for shooting and wait time for next bullet shot condition
			bulletWaitTime = Time.time + rateOfFire;//the wait time is based on the rate of fire
			Instantiate (bullet, bulletSpawn.position, bulletSpawn.rotation);//clone the bullet in front of the bullet spawn which is front
			//of the gun
		}
	}
}
