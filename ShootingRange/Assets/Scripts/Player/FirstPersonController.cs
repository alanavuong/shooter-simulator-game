using UnityEngine;
using System.Collections;

//this scripts relate to the player's movement and camera

public class FirstPersonController : MonoBehaviour {

	private float currentSprintSpeed;
	public float movementSpeed = 5.0f;//speed of player
	public float sprintSpeed;//speed of sprinting player
	public float mouseSensitivity = 5.0f;//speed of player's camera

	private float horizontalRotation = 0f;
	private float verticalRotation = 0f;
	private float currentAimDownSightSensitivitySpeed;//speed of camera rotation decrease upon aiming down the sights
	public float aimDownSightSensitivitySpeed;
	public float rotationVerticalLimit = 60.0f;//how far a player can look up and down

	public GameObject playerCamera;//the first person camera controlled the player
	private CharacterController playerController;

	private float verticalSpeed;//speed going forward and back
	private float horizontalSpeed;//speed going left and right

	public float playerHeight;//player will transit between height through standing and crouching
	public float playerCrouchHeight;//player height decrease upon crouching
	public float crouchingSpeedTransition; //how fast the player moves from standing to crouching or vise versa
	private float currentCrouchingMovementSpeed;
	public float crouchingMovementSpeed;//movement speed decrease upon crouching
	private float playerCurrentHeight;//player's return to these height 

	private bool isCrouching = false;//crooching determines whether sprinting available and speed change

	private float currentAimDownSightMovementSpeed;//the fixed aim down speed
	public float aimDownSightMovementSpeed;//aim down sight speed varies based on their hotkeys

	// Use this for initialization
	void Start () 
	{
		playerController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rotateCamera ();
		movePlayer ();
	}

	//player can control their direction of sight
	void rotateCamera()
	{
		//aim down sight speed slows rotation speed upon clicking this hotkey
		if (Input.GetKey ("o")) 
		{
			currentAimDownSightSensitivitySpeed = aimDownSightSensitivitySpeed;
		} 
		else 
		{
			currentAimDownSightSensitivitySpeed = 1.0f;
		}

		horizontalRotation = Input.GetAxis ("Mouse X") * mouseSensitivity * currentAimDownSightSensitivitySpeed;//keyboard input for horizontal camera movement
		transform.Rotate (0, horizontalRotation, 0);//move along the horizontal direction with the camera	

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity * currentAimDownSightSensitivitySpeed;//keyboard input for vertical movement
		verticalRotation = Mathf.Clamp (verticalRotation, -rotationVerticalLimit, rotationVerticalLimit);//set limit for vertical direction from max vertical to min vertical direction
		playerCamera.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);//move along vertical direction for camera 

	}

	//relate to movement of player's body
	void movePlayer()
	{
		playerMovementOnXZ_Plane ();
		playerCrouching ();
	}

	//enable forward,backward,left and right movement
	void playerMovementOnXZ_Plane()
	{
		//speed increase upon spriting but can't sprint when crouching or aiming down the sights
		if (Input.GetButton ("Sprint") && isCrouching == false && !Input.GetKey ("o") &&!Input.GetButton ("Move Backward")) 
		{
			currentSprintSpeed = sprintSpeed;
		} 
		else 
		{
			currentSprintSpeed = 1.0f;
		}

		//speed decrease upon crouching
		if (isCrouching == true) 
		{
			currentCrouchingMovementSpeed = crouchingMovementSpeed;
		} 
		else 
		{
			currentCrouchingMovementSpeed = 1.0f;
		}

		//speed decrease upon aiming down the sights
		if (Input.GetKey ("o")) 
		{
			currentAimDownSightMovementSpeed = aimDownSightMovementSpeed;
		} 
		else 
		{
			currentAimDownSightMovementSpeed = 1.0f;
		}

		//movement speed changes based on multiplier
		verticalSpeed = Input.GetAxis ("Vertical") * movementSpeed * currentSprintSpeed * currentCrouchingMovementSpeed * 
			currentAimDownSightMovementSpeed;
		horizontalSpeed = Input.GetAxis ("Horizontal") * movementSpeed * currentSprintSpeed * currentCrouchingMovementSpeed * 
			currentAimDownSightMovementSpeed;

		Vector3 speed = new Vector3 (horizontalSpeed, 0, verticalSpeed);//the player moves along the xz plane
		
		speed = transform.rotation * speed;//this allow the movement to follow the direction of the camera pointed direction
		CharacterController playerController = GetComponent<CharacterController> ();//
		
		playerController.SimpleMove( speed );
	}

	//player will move downward with a lower camera view
	void playerCrouching(){
		if (Input.GetButton ("Crouch")) 
		{
			//keep crouching until minimum height
			if (playerController.height > playerCrouchHeight) 
			{
				playerController.height -= crouchingSpeedTransition;
			}

		}
		//keep standing until maximum height
		else if (playerController.height < playerHeight) 
		{
			playerController.height += crouchingSpeedTransition;
		}
		
		playerCurrentHeight = playerController.height;//change game controller or player to match height

		//isCrouching takes a value to decide whether to sprint or not
		if (playerCurrentHeight >= playerHeight) 
		{
			isCrouching = false;
		} 
		else 
		{
			isCrouching = true;
		}
	}

}
