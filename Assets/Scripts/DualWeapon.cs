using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualWeapon : MonoBehaviour {

	private AudioSource audiosource;
	public GameObject shot;
	public Transform shotSpawn;
	public Transform shotSpawn2;
	// Use this for initialization
	void Start () {
		audiosource= GetComponent <AudioSource> ();
		InvokeRepeating ("Fire", 1.0f, 1.0f);

	}
	
	// Update is called once per frame
	void Fire () {
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
		audiosource.Play ();
	}
}
