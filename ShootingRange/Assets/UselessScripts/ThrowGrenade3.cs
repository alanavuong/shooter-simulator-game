using UnityEngine;
using System.Collections;

public class ThrowGrenade3 : MonoBehaviour {
	
	//This script throws a grenade while gravity push it down
	private Rigidbody grenadeRigidbody;
	public float grenadeVelocity;//velocity of the grenade where direction change based on player direction
	public GameObject grenade;
	public float verticalDistance;//
	public float horizontalDistance;
	public float totalDistance;
	public float upwardVelocity;
	public float forwardVelocity;

	void Start(){
		grenadeRigidbody = GetComponent<Rigidbody> ();
		upwardVelocity = totalDistance * Mathf.Sin (grenade.transform.eulerAngles.x * (Mathf.PI / 180));
		forwardVelocity = totalDistance * Mathf.Cos (grenade.transform.eulerAngles.x * (Mathf.PI / 180));
		verticalDistance = forwardVelocity * Mathf.Cos (grenade.transform.eulerAngles.y * (Mathf.PI / 180));
		horizontalDistance = forwardVelocity * Mathf.Sin (grenade.transform.eulerAngles.y * (Mathf.PI / 180));
		print ("upwardVelocity:" + upwardVelocity);
		print ("forwardVelocity:" + forwardVelocity);
		print ("verticalDistance:" + verticalDistance);
		print ("horizontalDistnace:" + horizontalDistance);
		grenadeRigidbody.velocity = new Vector3 (verticalDistance, upwardVelocity, horizontalDistance);
	}
}
