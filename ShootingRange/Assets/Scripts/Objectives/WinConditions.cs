using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/*
 * This scripts relates to the objective whether player wins/loses
 * */

public class WinConditions : MonoBehaviour {

	public Text victoryText;//display victory message
	public Text unfinishedObjectiveText;//display ongoing mission message
	public Text gameOverText; //display lose when conditions are met

	public RaycastShooting getRaycastScript;
	//public CountdownTimer getCountdownScript;


	public Text timerText;//this text will print out the remaning time
	private float minutes;//use for a countdown timer
	private float seconds;
	public float startingTime;//the starting time for a countdown

	public int requiredTargetDestroyed;//the number of target needed to destroy to win

	private bool timerStop = false;// play or pause timer

	void Start(){
		victoryText.text = "";//default no message in the beginning
		gameOverText.text = "";//default no message in the beginning
		hideUnfinishedObjective ();
		timerText.text = "";
		hideUnfinishedObjective ();
		//currentHealth = maxHealth;
	}

	void Update () {
		if (timerStop == false) {
			getTimer ();//Call the time function
		}

		if(startingTime < 1){//if timer is out of time than display game over
			displayGameOver ();
		}
	}
	
	//check if player reach their desination and goa
	void OnTriggerStay(Collider player)
	{
		//print (getCountdownScript.startingTime);
		if ((player.tag == "Player") && (requiredTargetDestroyed <= 0) && (startingTime > 1)) {
			timerStop = true;//timer has stop
			hideUnfinishedObjective();//turn off if already inside						
			victoryText.text = "You Win!";
		} else if (player.tag == "Player" && startingTime < 1) {
			displayGameOver ();
			hideUnfinishedObjective();
		}else if(player.tag == "Player"){
			unfinishedObjectiveText.text = "You haven't destroyed all the targets yet.";//Targets still remaining
		}
	}

	//remove remaining objective if player is not present at objective
	void OnTriggerExit(Collider player){//tell player if they didn't finish their objective upon reaching the goal
		hideUnfinishedObjective ();
	}

	//creates a countdown timer
	void getTimer()
	{
		if (Mathf.Floor (startingTime) != 0) {//timer keep tickin down until it reach zero
			startingTime = startingTime - (Time.deltaTime);//decreases the time limit by real time
			
			minutes = Mathf.Floor (startingTime) / 60;//convert 60 seconds for every one minute
			seconds = Mathf.Floor (startingTime) % 60;//when less than 60 seconds than it spills over as 
			//remaining seconds
			if (seconds >= 10) {
				timerText.text = "Time Remaining: " + Mathf.Floor (minutes).ToString () + ":" + 
					seconds.ToString ();
			} else {//if the remaning seconds is below 10 then it set up a 0 as a placeholder for tens place
				timerText.text = "Time Remaining: " + Mathf.Floor (minutes).ToString () + ":0" + 
					seconds.ToString ();
			}
		}
	}

	//if player runs out of time then display game over
	void displayGameOver()
	{
		gameOverText.text = "Game Over";
	}

	//hide unfinish objective upon leaving the trigger zone
	void hideUnfinishedObjective(){
		unfinishedObjectiveText.text = "";
	}

	//destroy target decrease the number of targets needed to be destroy
	void targetCounter(int targetDestroy)
	{
		requiredTargetDestroyed -= targetDestroy;
	}

	/*public void damageTarget(float gunDamage){
		if ((Input.GetKey ("e")) && (currentHealth >= 0)) {
			currentHealth -= gunDamage;
		} else {
			Destroy (target);
		}*/

}
