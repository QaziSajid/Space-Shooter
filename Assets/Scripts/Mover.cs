using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {
    public float speed;
    private Rigidbody ast;
    void Start()
    {
        ast= GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        ast.velocity = new Vector3(0,0,1) * speed;
    }
}
