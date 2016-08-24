using UnityEngine;
using System.Collections;

//this just a place to list all my to do list for this program

public class ProgramToDoList : MonoBehaviour 
{

	//CURRENT TASKS FOR THIS GAME OR ISSUES
	//fix progamming style if it is not good enough
	//give gun a reload time
	//when making a menu turn cursor on then off when leaving the menu
	//grenade currently has no count or recovering the ammo count
	//player is not picking up ammo anymore
	//set player height in FirstPersonController.cs script to the same height as inspector
	//change the capsule height upon crouching
	//fix character controller height or camera height? whatever is better
	//standing up from crouching seems glitchly and not smooth
	//player loses camera, gun, and everything upon dropping into the destory boundary

	//SUGGESTION TO SPEED UP THE GAME IF NECCESSARY
	//consider using break upon checking inventory
	//consider using direct variables instead of SendMessage

	//BEFORE RELEASING THE GAME TO DO LIST
	//make cursor invisible
	//fix the text positions for user interface like ammo count, win conditions, etc
	//change fire button to Input.GetButton("Fire1") in RaycastShooting.cs in Gun folders in Weapons folders
	//create aim down sights button for Input.GetKey("o") in RaycastShooting.cs in the gun folders in the weapons folders
	//consider a way to cancel grenade throw

	//FIXED ISSUES
	//player can't fire during aiming down the sights 

	//fix the target points count
	//lower the sensitivity upon zooming in

	//destroy grenade when it falls through the ground 
	//possible solution:place create a giant boundary destroyer below the ground

	//grenade sometimes don't turn increase trigger size

	//remove the gunSpawnPoint end point

	//move the spawn grenade point somewhere not on the camera

	//set up a health system to destroy the gameobjects 
	//solution:use sendmessage may need to change to direct variable

	//decrease speed during aim down sights 	
	//solution:just check whether they are using the button and modify camera speed

	//fixed grenade fuse time solution use wait for seconds function coroutine and change put it in the write place

	//if player is running and aim down sight. then, aim down sight overriddes the aim down sight motion solution check if they are using aim down sight button

	//it requires two click to fire your gun

	//the player can't fire and aim down the sights	
	//solution:my keyboard mouse can't do it but a keyboard mouse can do it

	//allow the player to shoot while crouching 

	//player can't sprint and shoot at the same time 
	//solution:create an if condition that accounts for the sprinting and shooting condition
}
