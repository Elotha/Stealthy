using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowControl : MonoBehaviour
{
	public Animator anim;
	Rigidbody2D rb;
	public float speed;
	Vector3 lastdir;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}



	void HandleMove()
	{
		float moveX = 0;
		float moveY = 0;

		if (Input.GetKey(KeyCode.W))
		{
			moveY = +1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			moveY = -1f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			moveX = +1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			moveX = -1f;
		}

		bool isIdle = moveX == 0 && moveY == 0;

		if (isIdle)
		{
			ıdle(lastdir);
			rb.velocity = Vector2.zero;
		}
		else
		{
			Vector3 moveDir = new Vector3(moveX, moveY).normalized;
			lastdir = moveDir;
			run(moveDir);
			rb.velocity = moveDir * speed;
		}

	}

	void ıdle(Vector3 dir)
	{
		if (dir.x > 0.8f)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			GetComponent<SpriteRenderer>().flipX = false;

		}
		// sağ ise true
		anim.SetBool("isIdle", true);
		anim.SetFloat("lastX", Mathf.Abs(dir.x));
		anim.SetFloat("lastY", dir.y);
	}

	void run(Vector3 dir)
	{
		if (dir.x > 0.8f)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else
		{
			GetComponent<SpriteRenderer>().flipX = false;

		}
		anim.SetBool("isIdle", false);
		anim.SetFloat("moveX", Mathf.Abs(dir.x));
		anim.SetFloat("moveY", dir.y);
	}

}
