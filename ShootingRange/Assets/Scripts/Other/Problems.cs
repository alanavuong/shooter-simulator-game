using UnityEngine;
using System.Collections;

public class Problems : MonoBehaviour {
	//WHEN FINISHING GAME
	//make cursor invisible
	//make code more readiable
	//fix the text positions for ammo count, win conditions, etc
	//return fire button to Input.GetButton("Fire1") in RaycastShooting.cs in Gun folders in Weapons folders
	//create aim down sights button in place for Input.GetKey("o") in RaycastShooting.cs in GUn folders in Weapons folders
	//don't forget to put back throwing grenade option

	//WHEN GAME IS LAGGING/SLOWER/INEFFICENT
	//consider using break upon checking inventory
	//consider using direct variables instead of SendMessage

	//IN PROCRESS
	//fix documentation style if not good enough
	//give gun a reload time
	//when making a menu turn cursor on then off when off the menu
	//grenade currently has no what you call it ammo count or recovering the ammo count
	//not picking up ammo anymore
	//set player height in FirstPersonController.cs script to the same height as inspector
	//change the capsule height upon crouching
	//fix character controller height or camera height? whatever is better
	//set up grenade ammo acount
	//crouching up seems glitching and not smooth
	//fix the speed problem and make cleaner code and more readiablet
	//player lose camera and gun and everything about destruction by boundary below

	//FIXED
	//player can't fire during aiming down the sights SOLUTION JUST CHECK IF THERE NOT PRESSING THE AIM DOWN SIGHT BUTTON
	//fix the target points coun
	//lower the sensitivity upon zooming in
	//Don't allow the player to shoot during shooting motion
	//destroy grenade over time period if in midair too long POSSIBLE SOLUTION PLACE AT THE A GIANT BOUNDARY DESTROYER
	//grenade sometimes don't turn into triggers
	//remove the gunSpawnPoint end point, unessecary
	//move the spawn grenade point somewhere not on the camera
	//set up a health system to destroy the gameobjects SOLUTION USE SENDMESSAGE MAY NEED TO CHANGE TO DIRECT VARIABLE
	//Decrease speed during aim down sights 	SOLUTION:JUST CHECK WHETHER THEY ARE USING THE BUTTON
	//fixed grenade fuse time SOLUTION USE WAIT FOR SECONDS FUNITON COURTINE(MISPELL) AND CHANGE CODE ORDER
	//if player is running and aim down sight then aim down sight overriddes the aim down sight motion SOLUTION CHECK IF AIM DOWN SIGHT BUTTON
	//it requires two click to fire your gun, understand the reason for this condition. problem is probably...
	//...Input.GetButton("Some key")) SOLUYION ASUS MOUSE X PROBLEM
	//Consider allow the player to shoot while crouching SOLUTION SET INNER LOOP WITH RAYCAST IF CONDITOON
	//player can't sprint and shoot at the same time SOLUTION CHECK WHETHER IF PLAYER IS PRESSING THE SPRINT BUTTON
}
