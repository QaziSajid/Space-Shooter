using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player")
			return;
		else 
		{
			PlayerController rh = other.GetComponent<PlayerController> ();
			if (rh != null) 
			{
				rh.restoreHealth ();
			}
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
