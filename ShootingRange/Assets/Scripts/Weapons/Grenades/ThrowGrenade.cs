using UnityEngine;
using System.Collections;

//this script will enable throwing a grenade and exploding after contact with collider after some seconds

public class ThrowGrenade : MonoBehaviour 
{

	public Rigidbody grenadeRigidbody;
	public CapsuleCollider playerGrenade;//the collider needs to hit another collider to trigger fuse time
	public GameObject grenade;
	public GameObject player;
	public GameObject playerCamera;
	public GameObject objectiveLocation;
	public float grenadeDamage;//damage it deals towards targets
	public float grenadeDistance;//there really isn't a unit for this
	public float explosionRadius;//grenade radius on the trigger of an explosion
	public float waitTime;//fuse time before grenade explosions upon hitting a collider such as a floor

	private float verticalDistance;//the direction forward/back on xz-plane
	private float horizontalDistance;//the direction left/right on xz-plane
	private float upwardVelocity;//grenade speed going upward on the y-axis
	private float forwardVelocity;//forward velocity is a combination of the vertical and horizontal distance

	//the grenade's velocity
	void Start()
	{
		playerGrenade = GetComponent<CapsuleCollider> ();
		player = GameObject.Find("Player");
		playerCamera = GameObject.Find ("First Person Camera");
		objectiveLocation = GameObject.Find ("Objective Location");

		upwardVelocity = grenadeDistance * Mathf.Sin (playerCamera.transform.eulerAngles.x * (Mathf.PI / 180));
		forwardVelocity = grenadeDistance * Mathf.Cos (playerCamera.transform.eulerAngles.x * (Mathf.PI / 180));

		//breaks the down the forward velocity down to a velocity in the x and z coordinate
		verticalDistance = forwardVelocity * Mathf.Cos (player.transform.eulerAngles.y * (Mathf.PI / 180));
		horizontalDistance = forwardVelocity * Mathf.Sin (player.transform.eulerAngles.y * (Mathf.PI / 180));

		///velocity must take a x and z argument hence breaking upwardVelocity
		grenadeRigidbody.velocity = new Vector3 (horizontalDistance, -upwardVelocity, verticalDistance); //final grenade velocity
	}

	//gravity falls down 9.8 meters per second
	void FixedUpdate()
	{
		grenadeRigidbody.velocity -= new Vector3 (0f, 9.8f * Time.deltaTime, 0f);
	}

	//if the grenades hit the ground then create explosion within trigger zone
	IEnumerator OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground") 
		{
			yield return StartCoroutine ("fuseTimer");
		}
	}

	//grenade damage anything of the label targets
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Target") 
		{
			if(other.gameObject != null)
			{
				GameObject.FindGameObjectWithTag("Target").SendMessage ("applyDamage", grenadeDamage);//subtract target's health by damage
			}
		}

		Destroy (GameObject.Find ("Grenade(Clone)"));//prevents grenade from stopping another grenade from exploding
	}

	//a fuse time when a grenade hits the ground before it explodes
	IEnumerator fuseTimer()
	{
		yield return new WaitForSeconds(waitTime);
		transform.GetComponent<Collider>().isTrigger = true;//this trigger the event for anything insides its radius
		playerGrenade.radius = explosionRadius;//explosion radius expands quickly

	}

}
