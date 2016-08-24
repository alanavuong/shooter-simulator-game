using UnityEngine;
using System.Collections;

public class TargetHealthPoint : MonoBehaviour {

	//how many health point the target has
	public float totalHealth;//starting health points for target
	private float currentHealth;//health decrease upon damage

	void Start()
	{
		currentHealth = totalHealth;//makes health independent of target 1, 2, 3, etc.
	}

	//this check for damage dealt on target
	void applyDamage(float damage)
	{
		currentHealth -= damage;//target lose health
		if (currentHealth <= 0) {//health below or at zero means object destroys itself
			GameObject.Find ("Objective Location").SendMessage("targetCounter", 1);//this decreases the number of required targets to destroy by 1
			Destroy (gameObject);
		}
	}
}
