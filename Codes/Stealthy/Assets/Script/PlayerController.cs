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

    void Start()
    {
		canToggle = true;
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		bc = GetComponent<BoxCollider2D>();
		shadow.SetActive(false);
    }
	
    void FixedUpdate()
    {
		if(!isShadow)
		{
			HandleMove();
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
		if(canToggle)
		{
			if (isShadow)
			{
				isShadow = false;
				rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				Camera.main.GetComponent<Follow>().ChangeTarget(transform);
				GameObject.Find("LightControl").GetComponent<LightControl>().changeTarget(this.gameObject);
				gameObject.GetComponent<Animator>().enabled = true;
				gameinfo.GoGuards();
				shadow.GetComponent<ShadowControl>().enabled = false;
				shadow.GetComponent<TimeBody>().StartRewind();
			}
			else
			{
				shadow.SetActive(true);
				shadow.GetComponent<ShadowControl>().enabled = true;
				isShadow = true;
				shadow.GetComponent<TimeBody>().enabled = true;
				shadow.GetComponent<TimeBody>().StopRewind();
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
		float moveX = 0;
		float moveY = 0;

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
		if(dir.x>0.8f)
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
