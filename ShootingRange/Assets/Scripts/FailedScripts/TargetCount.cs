using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetCount : MonoBehaviour {

	public RaycastShooting getRaycastScript;
	public WinConditions getWinConditionsScript;
	public Text remainingTargetCountText;

	void printRemaningCount(){
		remainingTargetCountText.text = "We won!";
	}
}
