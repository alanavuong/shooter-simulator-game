using UnityEngine;
using System.Collections;

public class WaitTimeScript : MonoBehaviour {

	// Use this for initialization
	void Start() {
		StartCoroutine(Example());
	}
	
	IEnumerator Example() {
		yield return new WaitForSeconds(5);
		Destroy (gameObject);
	}
}
