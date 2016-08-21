using UnityEngine;
using System.Collections;

public class DestroyTarget : MonoBehaviour {

	public static int numberOfTarget = 0;

	// Use this for initialization
	void OnTriggerEnter(Collider otherGameObject){
		if (otherGameObject.tag == "Bullet") {
			Destroy (otherGameObject.gameObject);
			Destroy (gameObject);
		}
	}

	void NumberOfTarget()
	{
		++numberOfTarget;
	}
}