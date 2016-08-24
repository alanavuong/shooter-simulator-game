using UnityEngine;
using System.Collections;

//destroy bullet upon a period of time when spawn

public class DestroyBulletHoleByTime : MonoBehaviour 
{

	public float bulletHoleLifespan;

	void Start () 
	{
		Destroy (gameObject, bulletHoleLifespan);
	}
}
