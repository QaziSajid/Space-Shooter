using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : MonoBehaviour {
	public float zspeed;
	public float xspeed;
	private PlayerController posget;
	private Rigidbody ast;
	private float px;
	private int rel;
	void Start()
	{
		GameObject temp = GameObject.FindWithTag ("Player");
		if (temp != null)
			posget = temp.GetComponent<PlayerController> ();
		ast= GetComponent<Rigidbody>();
	}
	void FixedUpdate()
	{
		px = posget.getX ();
		rel = (int)Mathf.Sign (px - transform.position.x);
		ast.velocity = new Vector3(rel*xspeed,0,zspeed);
	}
}
