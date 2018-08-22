using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumble;
    Rigidbody ast;
	// Use this for initialization
	void Start () {
        ast = GetComponent<Rigidbody>();
        ast.angularVelocity = Random.insideUnitSphere * tumble;
		
	}
}
