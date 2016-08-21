using UnityEngine;
using System.Collections;

/*
 *This scripts relate to the player's movement and controls
 */

public class FirstPersonController : MonoBehaviour {

	public float movementSpeed = 5.0f;//speed of player
	private float currentSprintSpeed;
	public float sprintSpeed;//speed of sprinting player
	public float mouseSensitivity = 5.0f;//speed of player's camera

	private float horizontalRotation = 0f;
	private float verticalRotation = 0f;//
	private float currentAimDownSightSensitivitySpeed;//speed of camera rotation decrease upon aiming down the sights
	public float aimDownSightSensitivitySpeed;
	public float rotationVerticalLimit = 60.0f;//how far a player can look up and down

	public GameObject playerCamera;//the first person camera controlled the player
	//public GameObject player;
	private CharacterController playerController;

	private float verticalSpeed;//speed going forward and back
	private float horizontalSpeed;//speed going left and right

	public float playerHeight;//player will transit between height through standing and crouching
	public float playerCrouchHeight;//player height decrease upon crouching
	public float crouchingSpeedTransition; //how fast the player moves from standing to crouching or vise versa
	private float currentCrouchingMovementSpeed;
	public float crouchingMovementSpeed;//movement speed decrease upon crouching
	private float playerCurrentHeight;//how player to return to this height

	private bool isCrouching = false;//crooching determines whether sprinting available and speed change

	private float currentAimDownSightMovementSpeed;
	public float aimDownSightMovementSpeed;//speed when aimming gun

	// Use this for initialization
	void Start () {
		//Screen.lockCursor = true;//hide the mouse cursor
		playerController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotateCamera ();
		movePlayer ();
	}

	//player can control their direction of sight
	void rotateCamera(){
		//rotation
		if (Input.GetKey ("o")) {
			currentAimDownSightSensitivitySpeed = aimDownSightSensitivitySpeed;
		} else {
			currentAimDownSightSensitivitySpeed = 1.0f;
		}
		horizontalRotation = Input.GetAxis ("Mouse X") * mouseSensitivity * currentAimDownSightSensitivitySpeed;//keyboard input for horizontal camera movement
		transform.Rotate (0, horizontalRotation, 0);//move along the horizontal direction with the camera
		
		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity * currentAimDownSightSensitivitySpeed;//keyboard input for vertical movement
		verticalRotation = Mathf.Clamp (verticalRotation, -rotationVerticalLimit, rotationVerticalLimit);//set limit for vertical direction from max vertical to min vertical direction
		playerCamera.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);//move along vertical direction for camera 
		//without interfering with the the horizontal camera

	}

	//relate to movement of player's body
	void movePlayer(){
		playerMovementOnXZ_Plane ();
		playerCrouching ();
		//playerjump ();
		/*if (Input.GetButtonDown ("Jump")) {
			gameObject.transform.position += new Vector3(0, 10, 0);
		}*/

		/*else{
			playerController.height = playerHeight;
		}*/

	}

	//enable forward,backward,left and right movement
	void playerMovementOnXZ_Plane(){

		//speed increase upon spriting but can't sprint when crouching or aiming down the sights
		if (Input.GetButton ("Sprint") && isCrouching == false && !Input.GetKey ("o") &&!Input.GetButton ("Move Backward")) {
			currentSprintSpeed = sprintSpeed;
		} else {
			currentSprintSpeed = 1.0f;
		}

		//speed decrease upon crouching
		if (isCrouching == true) {
			currentCrouchingMovementSpeed = crouchingMovementSpeed;
		} else {
			currentCrouchingMovementSpeed = 1.0f;
		}

		//speed decrease upon aiming down the sights
		if (Input.GetKey ("o")) {
			currentAimDownSightMovementSpeed = aimDownSightMovementSpeed;
		} else {
			currentAimDownSightMovementSpeed = 1.0f;
		}

		//speed varies upon sprinting, crouching, and aiming down the sights. if the conditions of those three don't apply the speed multiply
		//won't change current movement speed
		verticalSpeed = Input.GetAxis ("Vertical") * movementSpeed * currentSprintSpeed * currentCrouchingMovementSpeed * 
			currentAimDownSightMovementSpeed;
		horizontalSpeed = Input.GetAxis ("Horizontal") * movementSpeed * currentSprintSpeed * currentCrouchingMovementSpeed * 
			currentAimDownSightMovementSpeed;
		/*print ("after");
		print ("norm" + movementSpeed);
		print ("fast" + sprintSpeed);
		print ("ducking" + crouchingMovementSpeed);
		print ("total" + movementSpeed * sprintSpeed / movementSpeed);*/

		Vector3 speed = new Vector3 (horizontalSpeed, 0, verticalSpeed);//the player moves along the xz plane
		
		speed = transform.rotation * speed;//this allow the movement to follow the direction of the camera pointed direction
		CharacterController playerController = GetComponent<CharacterController> ();//
		
		playerController.SimpleMove( speed );
	}

	//player will move downward with a lower camera view
	void playerCrouching(){
		if (Input.GetButton ("Crouch")) {
			if (playerController.height > playerCrouchHeight) {//keep crouching until minimum height
				playerController.height -= crouchingSpeedTransition;
				//playerController.height = playerCrouchHeight;
			}

		} else if (playerController.height < playerHeight) {//keep standing until maximum height
			playerController.height += crouchingSpeedTransition;
		}
		
		playerCurrentHeight = playerController.height;//change game controller to match height
		if (playerCurrentHeight >= playerHeight) {//boolean to determine whether sprinting allowed
			isCrouching = false;
		} else {
			isCrouching = true;
		}
	}

	/*void playerjump(){//player jumps into the air once
		if (Input.GetKey ("j")) {//PLEASE CHANGE THIS INTO A BUTTON
			print ("You press the jump button");
			float jumpHeight = 2.0f;
			float jumpSpeed = 9.8f;
			Vector3 jumpPosition = new Vector3(horizontalSpeed, jumpHeight + transform.position.y, verticalSpeed);

			Vector3.MoveTowards (transform.position, jumpPosition, jumpSpeed * Time.deltaTime); 
		}
	}*/
}
