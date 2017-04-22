using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Character_Controller : MonoBehaviour {
    public float speed = 30f;
    private Rigidbody2D rb;

    public void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update(){
        Vector3 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input *= speed;
        rb.velocity = input;
    }
}
