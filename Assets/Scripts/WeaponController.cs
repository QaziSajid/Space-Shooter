using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

	private AudioSource audiosource;
	public GameObject shot;
	public Transform shotSpawn;

	// Use this for initialization
	void Start () {
		audiosource= GetComponent <AudioSource> ();
		InvokeRepeating ("Fire", 1.0f, 1.0f);

	}
	
	// Update is called once per frame
	void Fire () {
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audiosource.Play ();
	}
}
