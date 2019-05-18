using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowControl : MonoBehaviour
{

	Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}
}
