using UnityEngine;
using System.Collections;

public class ProjectileShot : MonoBehaviour {

	public Rigidbody bullet;
	public float bulletSpeed;

	void Start() {
		bullet = GetComponent<Rigidbody> ();
	}

	void FixedUpdate() {
		if (Input.GetButtonDown ("e"))
			bullet.velocity = new Vector3 (10, 0, 10);
	}
}
	