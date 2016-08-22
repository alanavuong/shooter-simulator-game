using UnityEngine;
using System.Collections;

public class DestroyBulletHoleByTime : MonoBehaviour {

	public float bulletHoleLifespan;

	//destroy bullet hole upon a period of time when spawn
	void Start () {
		Destroy (gameObject, bulletHoleLifespan);
	}
}
