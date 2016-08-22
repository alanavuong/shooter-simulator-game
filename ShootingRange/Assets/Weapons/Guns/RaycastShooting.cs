using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This scripts relates to guns
 * */

[System.Serializable]
public class GunStats
{
	public float roundsPerMinute;//how fast the gun fires
	public int bulletPerFire; //bullet shot per click
	public float bulletRange;//distance of the bullet trail
	public int gunClipSize; //how much bullet in clip
	public int numberOfClips;//how many magazine does the ammonation contain;
	public float gunRecoil;//how much the guns moves from its original position
	public float bulletDamage;//damage per bullet
}

public class RaycastShooting : MonoBehaviour {

	public GunStats gunStats;//stats of gun ability
	private float nextFire;//how long it takes for the next shot to shoot
	private float roundsPerSecond;//conversation factor for rounds per minute
	private bool magazineLoaded = true; //check if magazine is empty
	private int ammunationCount;//total ammunation usable
	public int gunClipSizeMax = 0;
	public int bulletTransfer;//for each bullet consume, reloading will take more ammo out
	public int magazineSpillOver;//prevent current maximimun ammo from being your current magazine ammo
	public int maximumAmmunation;//the max ammunation stored

	public int targetCount = 0;//would prefer public variable but it would be unacessiable
	public Text remainingTargetsCountText;	//display the remaining number of target
	private int remainingTargets;

	public WinConditions getWinConditionsScript;//gets the game object with win condition script

	public GameObject bulletHole;//spawn bullet hole upon hitting objects
	//public Transform bulletEndPoint;//bullet end point through travel
	public float bulletHoleLifespan;//how long bullet hole stays

	//public GameObject ammunation;

	public Text bulletMagazineText;//tracks the ammunation of the gun magzine
	public Text ammunationText;//display total ammunation
	public Text reloadMessageText;//if there an empty magazine than tell player instruction to reload

	public float fieldOfView;//how close the camera focus forward
	private Camera playerCamera;
	public GameObject camera;

	void Start(){
		displayTargetLeftText ();//display count of remaining target
		gunClipSizeMax = gunStats.gunClipSize;//store the maximu, mag size for comparison
		ammunationCount = gunStats.numberOfClips * gunClipSizeMax;
		maximumAmmunation = ammunationCount;//store the maximum ammunation for the gun
		displayMagazineCount ();//subtract from ammunation count
		displayAmmunationCount ();//display ammo count
		reloadMessage();//display reload message upon emptying whole clip
		playerCamera = camera.GetComponent<Camera> ();
	}
	// Update is called once per frame
	void Update () {
		//displayMagazineCount ();
		pullGunTrigger ();//perform gun fire
		reloadGun ();//refill gun and return unuse bullets into the ammunation count
		aimDownTheSight ();//gun will zoom in and points towards crosshair
	}

	void displayTargetLeftText(){//display number of targets left
		remainingTargets = getWinConditionsScript.requiredTargetDestroyed - targetCount;//Display number of target and subtract when destroyed
		remainingTargetsCountText.text = "Targeting left: "+ remainingTargets;//display remaining targets
		//print ("whatever");
	}

	//fire your gun with a line trail
	void pullGunTrigger(){
		//transform.rotation = Quaternion.identity;

		Vector3 bulletSpawnPoint = transform.forward;//bullet shoot forward
		//Quaternion randomAngle = Random.rotation;//produce a random angle
		//Quaternion bulletRotationAngle = Quaternion.LookRotation (bulletSpawnPoint);//
		//transform.rotation = Quaternion.RotateTowards(bulletRotationAngle, randomAngle, Random.Range (0.0f, gunStats.gunRecoil));
		//transit position between bullet start to some random float

		//Debug.DrawLine(transform.position, bulletEndPoint.position, Color.red);
		RaycastHit hit;

		if ((Input.GetKey("p") && !Input.GetButton ("Sprint")) && (Time.time > nextFire) &&
		    magazineLoaded == true) {
			print ("you are in the code");
			//fire gun and control rate of fire, fire only if gun has ammo
			bulletShotCounter ();
			roundsPerSecond = Mathf.Pow (gunStats.roundsPerMinute / 60.0f, -1);
			nextFire = Time.time + roundsPerSecond;//calculate a time for the next bullet to fire
			Ray ray = new Ray(transform.position, transform.forward);//shoot from spawn point toward forward
			if(Physics.Raycast (ray, out hit, gunStats.bulletRange)){//raycast fire at gameobject
				if (hit.collider.gameObject.tag == "Target")//if target tagged than it destroyed
				{
					if(hit.collider.gameObject != null)
					{
						hit.collider.gameObject.SendMessage ("applyDamage", gunStats.bulletDamage);
						/*Destroy(hit.transform.gameObject);//destroy tagged target
						++targetCount;//a counter to keep track of target destroyed
						displayTargetLeftText();//change target count if player destroys target*/
					}
				}else if(hit.collider.gameObject)//if bullet miss target than create a bullet hole
				{
					Instantiate(bulletHole, hit.point, Quaternion.identity);
				}
			}
		}
	}

	//keeps track of how much bullet shot
	void bulletShotCounter(){
		if (gunStats.gunClipSize > 0) {//gun can only subtract if there ammo inside
			gunStats.gunClipSize -= gunStats.bulletPerFire;//control how many bullet is fire per tap
			++bulletTransfer;
			//++gunClipSizeMax;
			displayMagazineCount ();
		} else {
			magazineLoaded = false;//gun can't fire with no bullet inside
			reloadMessageText.text = "Press r to reload";//no ammo will give a notification of no ammo available

		}
	}

	//gun reloads except at full magazine
	void reloadGun(){
		if (Input.GetButton ("Reload") && (gunStats.gunClipSize != gunClipSizeMax) && ammunationCount > 0
			&& (ammunationCount >= bulletTransfer)) {
			//gun can only reload if you lost ammo and you have enough ammo
			magazineLoaded = true;//gun fire enable
			ammunationCount -= bulletTransfer;//take a pool of ammo for each lost
			gunStats.gunClipSize += bulletTransfer;//refill your magazine with the bullet lost
			//gunStats.gunClipSize = gunClipSizeMax;
			displayMagazineCount ();
			bulletTransfer = 0;//now bullet consume will check next magazine for current amount of ammo
			displayAmmunationCount ();
			reloadMessage();
		} else if(Input.GetButton("Reload") && (gunStats.gunClipSize != gunClipSizeMax) && ammunationCount
		          < bulletTransfer && ammunationCount > 0){
			//this second condition prevent ammunation from reaching negative ammo
			magazineLoaded = true;
			gunStats.gunClipSize += ammunationCount;//add remaining clip into magazine
			ammunationCount = 0;//prevent ammo count from reducing below zero
			displayMagazineCount ();
			displayAmmunationCount ();
			bulletTransfer -= ammunationCount;
			bulletTransfer = 0;
			reloadMessage();
		}
	}

	//display the magazine count
	public void displayMagazineCount(){
		bulletMagazineText.text = gunStats.gunClipSize.ToString ();
	}

	//display the ammunation counts
	public void displayAmmunationCount(){
		ammunationText.text = ammunationCount.ToString ();
	}

	//hide message for reloading beyond zero ammunation in magazines
	void reloadMessage(){
		reloadMessageText.text = "";
	}

	//player refill ammo upon picking it up
	public bool refillAmmunation(int numberOfClips, bool excessiveAmmo){
		//excessiveAmmo = true;
		if ((gunClipSizeMax + maximumAmmunation) <= (gunStats.gunClipSize + ammunationCount)) {//can't pass max ammunation
			excessiveAmmo = false;//ammo can't be pick up with their max ammo
		}else if ((maximumAmmunation) < (gunStats.gunClipSize + ammunationCount) && 
		          (gunStats.gunClipSize + ammunationCount) < (gunClipSizeMax + maximumAmmunation)) {
			//add bullet to ammunation pool for the first clip
			ammunationCount += (maximumAmmunation + gunClipSizeMax) - (ammunationCount + gunStats.gunClipSize);
			displayAmmunationCount ();
			excessiveAmmo = true;
		}else if ((gunClipSizeMax <= (gunStats.gunClipSize + ammunationCount) && (gunStats.gunClipSize + ammunationCount) <= maximumAmmunation)) {
			//add a magazine to pool of ammo
			ammunationCount += numberOfClips * gunClipSizeMax;
			displayAmmunationCount ();
			excessiveAmmo = true;
		} else if(0 <= (gunStats.gunClipSize + ammunationCount) && (gunStats.gunClipSize + ammunationCount) < gunClipSizeMax){
			//maintain bulletTransfer to prevent diminishing magazine max size for the last remaining clip
			ammunationCount += numberOfClips * gunClipSizeMax;
			bulletTransfer = gunClipSizeMax - gunStats.gunClipSize;
			displayAmmunationCount ();
			excessiveAmmo = true;
		}

		return excessiveAmmo;//goes into another script to decide to destroy ammo or not

		}

	void aimDownTheSight()
	{
		if(Input.GetKey ("o")){
			playerCamera.fieldOfView = fieldOfView;
		}else{
			playerCamera.fieldOfView = 60.0f;
		}
	}

}

/*

This gives the player max ammo so that broken
		if (Input.GetKey ("q")) {
			print("you press q");
			magazineSpillOver = gunClipSizeMax - gunStats.gunClipSize;
			bulletTransfer = magazineSpillOver;
			ammunationCount = gunStats.numberOfClips * gunClipSizeMax + bulletTransfer;
			displayAmmunationCount();
			}
I forgot the problem here
		if(gunStats.ammunationCount > gunClipSizeMax){
		}else{
				gunStats.ammunationCount = 0;
				bulletTransfer = 0;
		}
Modulus operator is bad idea for ammo
		}else if (ammunationCount < gunStats.gunClipSize) {
			//bulletTransfer =  gunClipSizeMax - gunStats.gunClipSize % gunClipSizeMax;
			bulletTransfer += gunClipSizeMax % gunStats.gunClipSize;
			print (bulletTransfer);
			ammunationCount += numberOfClips * gunClipSizeMax;
			displayAmmunationCount ();
			print ("bullet consume is");
			print ("1");
		}else if (maximumAmmunation >= (ammunationCount + gunStats.gunClipSize)) {//add one mag to ammo pool
			ammunationCount += numberOfClips * gunClipSizeMax;
			displayAmmunationCount ();
			print("choice 1 active");
		} else if ((maximumAmmunation + gunClipSizeMax) > (ammunationCount + gunStats.gunClipSize)) {
			//maximize the total ammo including the clip and ammo pool base on the remaining ammo in the clip
			ammunationCount = maximumAmmunation + (gunClipSizeMax - gunStats.gunClipSize % gunClipSizeMax);
			//set ammo to max ammo plus bullet consume 
			displayAmmunationCount ();
			print("choice 2 active");
*/