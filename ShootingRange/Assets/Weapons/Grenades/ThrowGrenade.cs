using UnityEngine;
using System.Collections;

//this script will enable throwing a grenade and exploding after contact with collider after some seconds
//notes:the grenades is affected by direction the player is pointing with their crosshair
//notes:the trigger zone exists to allow during game testing to see the radius of the explosion
public class ThrowGrenade : MonoBehaviour {

	public Rigidbody grenadeRigidbody;
	public CapsuleCollider playerGrenade;//the collider needs to hit another collider to trigger fuse time
	public GameObject grenade;
	public GameObject player;
	public GameObject playerCamera;
	public GameObject objectiveLocation;
	public float grenadeDamage;//power/damage of grenade upon exploding
	public float grenadeDistance;//determine how far the grenades travel from point A to point B
	public float explosionRadius;//grenade explosive radius upon exploding
	public float waitTime;//fuse time before grenade explosions upon hitting a collider such as a floor

	//the following represents components of a vector to give the grenade the right speed and direction
	private float verticalDistance;//the direction forward/back on xz-plane
	private float horizontalDistance;//the direction left/right on xz-plane
	private float upwardVelocity;//grenade speed going up
	private float forwardVelocity;//grenade speed thrown forward on the xz-plane including a vertical and 
								  //horizontal distance component

	//the grenade's velocity
	void Start(){
		playerGrenade = GetComponent<CapsuleCollider> ();
		player = GameObject.Find("Player");
		playerCamera = GameObject.Find ("First Person Camera");
		objectiveLocation = GameObject.Find ("Objective Location");

		upwardVelocity = grenadeDistance * Mathf.Sin (playerCamera.transform.eulerAngles.x * (Mathf.PI / 180));
		forwardVelocity = grenadeDistance * Mathf.Cos (playerCamera.transform.eulerAngles.x * (Mathf.PI / 180));
		verticalDistance = forwardVelocity * Mathf.Cos (player.transform.eulerAngles.y * (Mathf.PI / 180));
		horizontalDistance = forwardVelocity * Mathf.Sin (player.transform.eulerAngles.y * (Mathf.PI / 180));
		//grenade's final velocity is always the same but its velocity component change depending on the cos/sin
		grenadeRigidbody.velocity = new Vector3 (horizontalDistance, -upwardVelocity, verticalDistance);
	}

	//gravity push down 9.8 meters per second
	void FixedUpdate(){
		grenadeRigidbody.velocity -= new Vector3 (0f, 9.8f * Time.deltaTime, 0f);
	}

	//if greande hit a gameobject then create explosion within trigger zone
	IEnumerator OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Ground") {//check for the label tag before the fuse time
			yield return StartCoroutine ("fuseTimer");
		}
	}

	//grenade elimates anything of the label target
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Target") {
			if(other.gameObject != null){
				GameObject.FindGameObjectWithTag("Target").SendMessage ("applyDamage", grenadeDamage);
			}
		}
		Destroy (GameObject.Find ("Grenade(Clone)"));//avoids grenades destroying other grenades
	}

	//a fuse time when a grenade hits the ground before it explodes
	IEnumerator fuseTimer()
	{
		yield return new WaitForSeconds(waitTime);
		transform.GetComponent<Collider>().isTrigger = true;//this trigger the event for anything insides its radius
		playerGrenade.radius = explosionRadius;//explosion radius expands quickly

	}

}
