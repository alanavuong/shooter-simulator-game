using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//this script relates to guns

[System.Serializable]
public class GunStats
{
	public float bulletDamage;//damage per bullet
	public float roundsPerMinute;//how fast the gun fires
	public int bulletPerFire; //bullet shot per click
	public float bulletRange;//distance of the bullet trail
	public int gunClipSize; //how much bullet in clip
	public int numberOfClips;//how many magazine does the ammonation contain
	public float gunRecoil;//how much the guns moves from its original position
}

public class RaycastShooting : MonoBehaviour 
{

	public GunStats gunStats;//stats of gun's ability
	private float nextFire;//how long it takes for the next shot to shoot
	private float roundsPerSecond;//conversation rounds per minute to rounds per second
	private bool magazineLoaded = true; //check if magazine is empty
	private int ammunitionCount;//total ammunition usable that not in the gun
	public int gunClipSizeMax = 0;
	public int bulletTransfer;//the number of bullets you shoot in one magazine is the number of bullet you reload with
	public int magazineSpillOver;//prevent current maximimun ammo from being your current magazine ammo
	public int maximumammunition;//the max ammunition stored

	public int targetCount = 0;//would prefer private variable but it only work as public
	public Text remainingTargetsCountText;//display the remaining number of target
	private int remainingTargets;

	public WinConditions getWinConditionsScript;

	public GameObject bulletHole;//spawn bullet hole upon hitting objects
	public float bulletHoleLifespan;//how long bullet hole stays


	public Text bulletMagazineText;//tracks the ammunition of the gun magzine
	public Text ammunitionText;//display total ammunition
	public Text reloadMessageText;//if there an empty magazine than tell player instruction to reload

	public float fieldOfView;//how close the camera focus forward
	private Camera playerCamera;
	public GameObject camera;

	void Start()
	{
		displayRemainingTargets ();//display count of remaining target
		gunClipSizeMax = gunStats.gunClipSize;//start off with a full magazine
		ammunitionCount = gunStats.numberOfClips * gunClipSizeMax;//start off with this fixed amount of ammo
		maximumammunition = ammunitionCount;//store the maximum ammunition for the gun
		displayMagazineCount ();
		displayammunitionCount ();
		reloadMessage();//display reload message upon emptying whole clip
		playerCamera = camera.GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () 
	{
		pullGunTrigger ();//perform gun fire
		reloadGun ();//refill magazine ammunition
		aimDownTheSight ();//gun will zoom in and points towards crosshair
	}

	//display number of targets left
	void displayRemainingTargets()
	{
		remainingTargets = getWinConditionsScript.requiredTargetDestroyed - targetCount;//Display number of target and subtract when destroyed
		remainingTargetsCountText.text = "Targeting left: "+ remainingTargets;//display remaining targets
	}

	//fire your gun with a line trail
	void pullGunTrigger()
	{

		Vector3 bulletSpawnPoint = transform.forward;//bullet shoot forward
		RaycastHit hit;

		if ((Input.GetKey("p") && !Input.GetButton ("Sprint")) && (Time.time > nextFire) &&
		    magazineLoaded == true) 
		{
			bulletShotCounter ();
			roundsPerSecond = Mathf.Pow (gunStats.roundsPerMinute / 60.0f, -1);
			nextFire = Time.time + roundsPerSecond;//calculate a time for the next bullet to fire

			//shoot from spawn point in front of the gun to the forward direciton
			Ray ray = new Ray(transform.position, transform.forward);

			//raycast fire at target
			if(Physics.Raycast (ray, out hit, gunStats.bulletRange)){
				//if target tagged is hit by your gun trail
				if (hit.collider.gameObject.tag == "Target")
				{
					if(hit.collider.gameObject != null)
					{
						hit.collider.gameObject.SendMessage ("applyDamage", gunStats.bulletDamage);//deal bullet damage
					}
				//if bullet miss target than create a bullet hole
				}else if(hit.collider.gameObject)
				{
					Instantiate(bulletHole, hit.point, Quaternion.identity);
				}
			}
		}

	}

	//keeps track of how much bullet shot starting from a full magazine
	void bulletShotCounter()
	{
		//gun can only subtract if there ammo inside
		if (gunStats.gunClipSize > 0) 
		{
			gunStats.gunClipSize -= gunStats.bulletPerFire;//control how many bullet is fire per tap
			++bulletTransfer;
			displayMagazineCount ();
		} 
		else 
		{
			magazineLoaded = false;//gun can't fire with no bullet inside
			reloadMessageText.text = "Press r to reload";//no ammo will give a notification of no ammo available

		}
	}

	//gun reloads except at full magazine
	void reloadGun()
	{
		//gun can only reload if you lost magazine ammo and you have enough ammo
		if (Input.GetButton ("Reload") && (gunStats.gunClipSize != gunClipSizeMax) && ammunitionCount > 0
			&& (ammunitionCount >= bulletTransfer)) 
		{
			magazineLoaded = true;//gun fire enable

			ammunitionCount -= bulletTransfer;//takes that pool of ammo for each lost
			gunStats.gunClipSize += bulletTransfer;//refill your magazine with that pool of ammo
			bulletTransfer = 0;//now bullet transfer reset to prepare for next magazine

			displayMagazineCount ();
			displayammunitionCount ();
			reloadMessage();
		} 
		//gun has lost magazine ammo and there not enough ammo left to fill magazine completely and ammo isn't negative
		else if(Input.GetButton("Reload") && (gunStats.gunClipSize != gunClipSizeMax) && ammunitionCount
		          < bulletTransfer && ammunitionCount > 0)
		{
			magazineLoaded = true;

			gunStats.gunClipSize += ammunitionCount;//add remaining clip into magazine
			ammunitionCount = 0;//prevents ammo count from reducing below zero
			bulletTransfer = 0;//this ready for the next magazine if player finds more ammunition

			displayMagazineCount ();
			displayammunitionCount ();
			reloadMessage();
		}
	}
	
	public void displayMagazineCount()
	{
		bulletMagazineText.text = gunStats.gunClipSize.ToString ();
	}
	
	public void displayammunitionCount()
	{
		ammunitionText.text = ammunitionCount.ToString ();
	}

	//hide message to reload until player reach zero magazine
	void reloadMessage()
	{
		reloadMessageText.text = "";
	}

	//player refill ammo upon picking ammo box up
	public bool refillAmmunition(int numberOfClips, bool excessiveAmmo)
	{
		//can't add more ammo if you have max ammunition
		if ((gunClipSizeMax + maximumammunition) <= (gunStats.gunClipSize + ammunitionCount)) 
		{
			excessiveAmmo = false;//ammo can't be pick up with their max ammo
		}
		//prevent picking up ammo if player is suppose to be full already
		else if ((maximumammunition) < (gunStats.gunClipSize + ammunitionCount) && 
		          (gunStats.gunClipSize + ammunitionCount) < (gunClipSizeMax + maximumammunition)) 
		{
			excessiveAmmo = true;
			ammunitionCount += (maximumammunition + gunClipSizeMax) - (ammunitionCount + gunStats.gunClipSize);
			displayammunitionCount ();
		}
		//adds a full magazine to pool of ammo ammusing there ammo doesn't suppass maximun ammo limit
		else if ((gunClipSizeMax <= (gunStats.gunClipSize + ammunitionCount) && (gunStats.gunClipSize + ammunitionCount) <= maximumammunition)) 
		{
			excessiveAmmo = true;
			ammunitionCount += numberOfClips * gunClipSizeMax;
			displayammunitionCount ();
		} 
		//maintains bulletTransfer count to prevent magazine size becoming the previous magazine size when it reload
		else if(0 <= (gunStats.gunClipSize + ammunitionCount) && (gunStats.gunClipSize + ammunitionCount) < gunClipSizeMax)
		{
			excessiveAmmo = true;

			ammunitionCount += numberOfClips * gunClipSizeMax;
			bulletTransfer = gunClipSizeMax - gunStats.gunClipSize;

			displayammunitionCount ();
		}

		return excessiveAmmo;//goes into another script to decide to destroy ammo box or not

		}


	//decrease field of view upon aiming down the sight
	void aimDownTheSight()
	{
		if(Input.GetKey ("o"))
		{
			playerCamera.fieldOfView = fieldOfView;
		}
		else
		{
			playerCamera.fieldOfView = 60.0f;
		}
	}

}