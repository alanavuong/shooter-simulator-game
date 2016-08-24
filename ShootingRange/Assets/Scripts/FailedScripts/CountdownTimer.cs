using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownTimer : MonoBehaviour {
	/*
	public Text timerText;//this text will print out the remaning time
	private float minutes;
	private float seconds;
	public float startingTime;//the starting time for a countdown

	public WinConditions getWinConditionScript;

	void Update () {
		if (getWinConditionScript.timerStop == false) {
			getTimerFunction ();//Call the time function
		}
	}

	void getTimerFunction()
	{
		if (Mathf.Floor (startingTime) != 0) {
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
	}*/

}
