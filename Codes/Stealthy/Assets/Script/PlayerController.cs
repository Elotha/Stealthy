using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	Rigidbody2D rb;
	BoxCollider2D bc;
	public GameObject shadow;
	bool isShadow = false;
	Animator anim;
	public float NormalSpeed;
	public float RunSpeed;
	public float CSpeed;
	private float speed;

    void Start()
    {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		bc = GetComponent<BoxCollider2D>();
		shadow.SetActive(false);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
		if(!isShadow)
		{
			if(Input.GetKey("w"))
			{
				rb.velocity = new Vector2(0, speed);
				GetComponent<SpriteRenderer>().flipX = false;
				anim.SetInteger("State",1);
			}
			else if(Input.GetKey("d"))
			{
				rb.velocity = new Vector2(speed, 0);
				GetComponent<SpriteRenderer>().flipX = true;
				anim.SetInteger("State", 3);
			}
			else if (Input.GetKey("s"))
			{
				rb.velocity = new Vector2(0, -speed);
				GetComponent<SpriteRenderer>().flipX = false;
				anim.SetInteger("State", 2);
			}
			else if (Input.GetKey("a"))
			{
				rb.velocity = new Vector2(-speed, 0);
				GetComponent<SpriteRenderer>().flipX = false;
				anim.SetInteger("State", 3);
			}
			else
			{
				rb.velocity = Vector2.zero;
			}
			
		}


		if(Input.GetKey("c"))
		{
			speed = CSpeed;
		}
		else
		{
			if (Input.GetKey("left shift"))
			{
				speed = RunSpeed;
			}
			else
			{
				speed = NormalSpeed;
			}
		}

		

		if(Input.GetKeyDown("space"))
		{
			ToggleShadow();
			
		}
	}


	void ToggleShadow()
	{
		if(isShadow)
		{
			shadow.SetActive(false);
			isShadow = false;
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
			Camera.main.GetComponent<Follow>().ChangeTarget(transform);
			GameObject.Find("LightControl").GetComponent<LightControl>().changeTarget(transform);
			gameObject.GetComponent<Animator>().enabled = true;
		}
		else
		{
			shadow.SetActive(true);
			isShadow = true;
			shadow.transform.position = transform.position + new Vector3(0.15f, 0, 0);
			Camera.main.GetComponent<Follow>().ChangeTarget(shadow.transform);
			GameObject.Find("LightControl").GetComponent<LightControl>().changeTarget(shadow.transform);
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			gameObject.GetComponent<Animator>().enabled = false;

		}
		
	}
	


}
