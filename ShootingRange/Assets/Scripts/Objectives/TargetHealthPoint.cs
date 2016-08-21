using UnityEngine;
using System.Collections;

public class TargetHealthPoint : MonoBehaviour {

	//how many health point the target has
	public float totalHealth;//starting health points for target
	private float currentHealth;//health decrease upon damage

	void Start()
	{
		currentHealth = totalHealth;//so health is not connected with other health script
	}

	//this check for damage dealt on target
	void applyDamage(float damage)
	{
		currentHealth -= damage;//target lose health
		if (currentHealth <= 0) {//health below or at zero means object destroys itself
			GameObject.Find ("Objective Location").SendMessage("targetCounter", 1);//reduce number of target needed to be destroy by one
			Destroy (gameObject);
		}
	}
}
