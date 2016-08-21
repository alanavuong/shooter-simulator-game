using UnityEngine;
using System.Collections;

public class DestroyBulletOverTime : MonoBehaviour {

	public float bulletLifespan;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, bulletLifespan);//destroy the bullet after x amount of seconds
	}
}
