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
	public GameInfo gameinfo;
	public bool canToggle;
	Vector3 lastdir;


	float moveX = 0;
	float moveY = 0;


	bool isdead;
	float deadtime;

	float shadowtoggletime;

    void Start()
    {
		shadowtoggletime = 0f;
		deadtime = 0.6f;
		isdead = false;
		canToggle = true;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		bc = GetComponent<BoxCollider2D>();
		shadow.SetActive(false);
    }
	
    void FixedUpdate()
    {

		if(!isdead)
		{
			shadowtoggletime -= Time.deltaTime;
			if (!isShadow)
			{
				HandleMove();
			}
			if (Input.GetKey("c"))
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
			if (Input.GetKeyDown("space"))
			{

				if(shadowtoggletime<0)
				{
					ToggleShadow();
					
				}
				

			}
		}
		else
		{

			anim.SetBool("dead", true);
			anim.SetBool("isIdle", true);
			deadtime -= Time.deltaTime;
			rb.velocity =Vector2.zero;
			anim.SetBool("isIdle", true);
			if (deadtime<0)
			{
				deadtime = 0.6f;
				isdead = false;
				anim.SetBool("dead", false);
				transform.position = GameObject.FindGameObjectWithTag("Spawn").transform.position;
			}


		}
	}

		
	
	public void repeat()
	{
		GameObject[] a = GameObject.FindGameObjectsWithTag("mermi");
		for (int i = 0; i < a.Length; i++)
		{
			a[i].GetComponent<MissControl>().changespeed();
		}


		isShadow = false;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		Camera.main.GetComponent<Follow>().ChangeTarget(transform);
		GameObject.Find("LightControl").GetComponent<LightControl>().changeTarget(this.gameObject);
		gameObject.GetComponent<Animator>().enabled = true;
		gameinfo.GoGuards();
		shadow.GetComponent<ShadowControl>().startrepeat();
	}


	void ToggleShadow()
	{
		if(canToggle)
		{
			if (isShadow)
			{
				repeat();
				
			}
			else
			{
				shadow.SetActive(true);
				shadow.GetComponent<ShadowControl>().enabled = true;
				isShadow = true;

				GameObject[] a = GameObject.FindGameObjectsWithTag("mermi");
				for(int i=0;i<a.Length;i++)
				{
					a[i].GetComponent<MissControl>().changespeed();
				}
				
				shadow.GetComponent<ShadowControl>().repeatstartpos = transform.position + new Vector3(0.15f, 0, 0);
				shadow.GetComponent<ShadowControl>().timetorepeat = 5f;

				shadow.transform.position = transform.position + new Vector3(0.15f, 0, 0);
				Camera.main.GetComponent<Follow>().ChangeTarget(shadow.transform);
				GameObject.Find("LightControl").GetComponent<LightControl>().changeTarget(shadow);
				rb.constraints = RigidbodyConstraints2D.FreezeAll;
				gameObject.GetComponent<Animator>().enabled = false;
				gameinfo.StopGuards();

			}
		}
		
	}


	void HandleMove()
	{
		moveX = 0;
		moveY = 0;

		if(Input.GetKey(KeyCode.W))
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


		if (dir.x + 0.7f < 0.01f && dir.y - 0.7f < 0.01f && dir.y - 0.7f > -0.01f)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}





		if (dir.x>0.5f && dir.x <0.75f && dir.y < -0.65f && dir.y > -0.75f)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		

		anim.SetBool("isIdle", false);
		anim.SetFloat("moveX", Mathf.Abs(dir.x));
		anim.SetFloat("moveY", dir.y);
	}
	
	public void dead()
	{
		isdead = true;
	}

}
