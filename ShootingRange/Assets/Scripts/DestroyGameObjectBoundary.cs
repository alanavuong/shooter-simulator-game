using UnityEngine;
using System.Collections;

public class DestroyGameObjectBoundary : MonoBehaviour {

	//Destroy grenades that slip through the floor or player
	void OnTriggerEnter(Collider other)
	{
		Destroy (other.gameObject);
	}
}
