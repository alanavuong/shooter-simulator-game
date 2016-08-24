using UnityEngine;
using System.Collections;

//how many health point the target has

public class TargetHealthPoint : MonoBehaviour {
	
	public float totalHealth;//starting health points for that 1 target
	private float currentHealth;//health decrease upon damage

	void Start()
	{
		currentHealth = totalHealth;//makes health independent of target 1, 2, 3, etc.
	}

	//this check for damage dealt on target
	void applyDamage(float damage)
	{
		currentHealth -= damage;//target lose health

		//health below or at zero means object destroys itself
		if (currentHealth <= 0) 
		{
			GameObject.Find ("Objective Location").SendMessage("targetCounter", 1);//this decreases the number of required targets to destroy by 1
			Destroy (gameObject);
		}
	}

}
