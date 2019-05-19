using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MissControl : MonoBehaviour
{
	private GameObject target;
	private Rigidbody2D rb;
	public float speed;
	public float rotateSpeed = 200;
	float damage = 3;
	bool stop;
	Vector2 lastspeed;

	void Start()
	{
		stop = false;
		speed = 0.5f;
		target = GameObject.FindGameObjectWithTag("Player");
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		if(stop)
		{
			
		}
		else
		{
			Vector2 direction = (Vector2)target.transform.position - rb.position;

			direction.Normalize();

			float rotateAmount = Vector3.Cross(direction, transform.up).z;

			rb.angularVelocity = rotateAmount * -rotateSpeed;
			rb.velocity = transform.up * speed;


			if (speed < 2)
			{
				speed = speed + 0.01f;
			}
		}
		

	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		//patlama efekti

		if (collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerController>().dead();
		}
		if (collision.gameObject.tag == "Shadow")
		{
			collision.gameObject.GetComponent<ShadowControl>().git();
		}

		Destroy(gameObject);
	}


	public void changespeed()
	{
		if(stop)
		{
			rb.velocity = lastspeed;
			stop = false;
		}
		else
		{
			stop = true;
			lastspeed = rb.velocity;
			rb.velocity = Vector2.zero;

		}
	}

}