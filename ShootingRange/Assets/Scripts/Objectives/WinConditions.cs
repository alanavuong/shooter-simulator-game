using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//this scripts relates to the objective whether the player wins/loses

public class WinConditions : MonoBehaviour {

	public Text unfinishedObjectiveText;//display ongoing mission message
	public Text victoryText;//display victory message on fullfilling certain conidionts
	public Text gameOverText; //display lose when conditions are met

	public Text timerText;//this text will print out the remaning time
	public float startingTime;//the starting time for a countdown
	private float minutes;//use for a countdown timer
	private float seconds;
	private bool timerStop = false;//play or pause timer

	public int requiredTargetDestroyed;//the number of target needed to destroy to win


	void Start()
	{
		victoryText.text = "";//you don't want to tell they win in the beginning
		gameOverText.text = "";//you don't want to tell they lose in the beginning
		timerText.text = "";//this is just placeholder
		hideUnfinishedObjective ();
	}

	void Update () 
	{
		if (timerStop == false) 
		{
			getTimer ();
		}

		//if timer is out of time than display game over
		if(startingTime < 1)
		{
			displayGameOver ();
		}
	}
	
	//check if player reach their desination and goal
	void OnTriggerStay(Collider player)
	{
		//the player wins the game only if they destroy all targets before time runs out and reach the desination
		if ((player.tag == "Player") && (requiredTargetDestroyed <= 0) && (startingTime > 1)) 
		{
			timerStop = true;
			hideUnfinishedObjective();//stop showing the current objective if they already won						
			victoryText.text = "You Win!";
		} 
		else if (player.tag == "Player" && startingTime < 1) 
		{
			displayGameOver ();
			hideUnfinishedObjective();
		}
		//if the objective isn't finish execute this
		else if(player.tag == "Player")
		{
			unfinishedObjectiveText.text = "You haven't destroyed all the targets yet.";
		}
	}

	//if they leave the objective location than stop telling them about the objective isn't finish
	void OnTriggerExit(Collider player)
	{
		hideUnfinishedObjective ();
	}

	//creates a countdown timer
	void getTimer()
	{
		if (Mathf.Floor (startingTime) != 0) //floor exists to prevent negative seconds
		{
			startingTime = startingTime - (Time.deltaTime);//decreases the time limit by real time
			
			minutes = Mathf.Floor (startingTime) / 60;//convert 60 seconds for every one minute
			seconds = Mathf.Floor (startingTime) % 60;//when less than 60 seconds than it spills over as 

			//this just format to show minutes and seconds
			if (seconds >= 10) 
			{
				timerText.text = "Time Remaining: " + Mathf.Floor (minutes).ToString () + ":" + 
					seconds.ToString ();
			}
			//if the remaning seconds is below 10 then it set up a 0 as a placeholder for tens place
			else 
			{
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
	void hideUnfinishedObjective()
	{
		unfinishedObjectiveText.text = "";
	}

	//destroy target decrease the number of targets needed to be destroy
	void targetCounter(int targetDestroy)
	{
		requiredTargetDestroyed -= targetDestroy;
	}

}
