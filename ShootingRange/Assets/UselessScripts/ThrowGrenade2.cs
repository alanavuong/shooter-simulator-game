using UnityEngine;
using System.Collections;

public class ThrowGrenade2 : MonoBehaviour {
	
	//This script throws a grenade while gravity push it down
	private Rigidbody grenadeRigidbody;
	public float grenadeVelocity;//velocity of the grenade where direction change based on player direction
	public GameObject grenade;
	public float verticalDistance;//
	public float horizontalDistance;

	void Start(){
		grenadeRigidbody = GetComponent<Rigidbody> ();
		//first convert degree of player into radian and determine using the unit circle the direction of grenade
		verticalDistance = grenadeVelocity * Mathf.Cos (grenade.transform.eulerAngles.y * (Mathf.PI/180));
		horizontalDistance = grenadeVelocity * Mathf.Sin (grenade.transform.eulerAngles.y * (Mathf.PI/180));
		grenadeRigidbody.velocity = new Vector3 (verticalDistance, 0, horizontalDistance);
	}
}
