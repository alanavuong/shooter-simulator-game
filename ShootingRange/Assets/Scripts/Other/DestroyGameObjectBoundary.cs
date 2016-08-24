using UnityEngine;
using System.Collections;

public class DestroyGameObjectBoundary : MonoBehaviour {

	//intended to destroy grenades that slip through the floor or the player
	void OnTriggerEnter(Collider other)
	{
		Destroy (other.gameObject);
	}
}
