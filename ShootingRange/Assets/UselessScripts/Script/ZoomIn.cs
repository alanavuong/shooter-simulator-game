using UnityEngine;
using System.Collections;

public class ZoomIn : MonoBehaviour {
	public float fieldOfView;
	private Camera mainCamera;
	public GameObject cameraObject;

	// Update is called once per frame
	void Start(){
		//cameraObject = gameObject.GetComponent<Camera> ();
		mainCamera = cameraObject.GetComponent<Camera> ();

	}
	void Update () {
		if (Input.GetKey ("r")) {
			mainCamera.fieldOfView = 45;
		} else {
			mainCamera.fieldOfView = 60;
		}
	}
}
