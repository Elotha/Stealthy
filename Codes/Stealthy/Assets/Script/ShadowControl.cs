using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowControl : MonoBehaviour
{
	public Animator anim;
	Rigidbody2D rb;
	public float speed;
	Vector3 lastdir;

	List<Vector2> moves;
	List<bool> attack;

	bool repeat;

	public Vector3 repeatstartpos;

	//------------------------------------
	
	float timeToAttack;
	public float timeBtwAttakcs;
	public Transform attackPos;
	public LayerMask enemy;
	public float attackRange;
	public LightControl Lc;

	//------------------------------------

	public float timetorepeat;
	public Image bar;
	public GameObject player;

	
	// Start is called before the first frame update
	void Start()
	{
		repeat = false;
		moves = new List<Vector2>();
		attack = new List<bool>();
		rb = GetComponent<Rigidbody2D>();
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if(repeat)
		{
			handlemove2();
			repeatattack();
		}
		else
		{
			HandleMove();
			attackControl();

			if (timetorepeat < 0)
			{
				player.GetComponent<PlayerController>().repeat();
			}
			else
			{
				timetorepeat -= Time.deltaTime;
				bar.fillAmount = timetorepeat / 5;
			}
		}
		
	}

	void attackControl()
	{
		int count = attack.Count;
		if (timeToAttack <= 0)
		{
			if (Input.GetKey(KeyCode.F))
			{
				
				timeToAttack = timeBtwAttakcs;
				attack.Add(true);
			}

			if(attack.Count == count)
			{
				attack.Add(false);
			}
		}
		else
		{
			attack.Add(false);
			timeToAttack -= Time.deltaTime;
		}
	}

	void repeatattack()
	{
		if (attack.Count > 0)
		{
			if (attack[0])
			{
				Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

				for (int i = 0; i < enemies.Length; i++)
				{
					enemies[i].GetComponent<Guard>().dead_();
				}
				Lc.findLights();
				timeToAttack = timeBtwAttakcs;

				anim.SetBool("Wind", true);

				attack.RemoveAt(0);
			}
			else
			{
				timeToAttack -= Time.deltaTime;
				attack.RemoveAt(0);
			}

			if (timeToAttack < 0.5)
			{
				anim.SetBool("Wind", false);
			}
		}
	}



	void HandleMove()
	{
		float moveX = 0;
		float moveY = 0;

		if(Lc.FilledBar.fillAmount>0.8)
		{
			git();
		}

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
		
		moves.Add(new Vector2(moveX,moveY));

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



	void handlemove2()
	{
		if(moves.Count>0)
		{
			float moveX = 0;
			float moveY = 0;

			moveX = moves[0].x;
			moveY = moves[0].y;

			moves.RemoveAt(0);

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
		else
		{
			gameObject.SetActive(false);
			repeat = false;

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
		
		if (dir.x > 0.5f && dir.x < 0.75f && dir.y < -0.65f && dir.y > -0.75f)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		
		anim.SetBool("isIdle", false);
		anim.SetFloat("moveX", Mathf.Abs(dir.x));
		anim.SetFloat("moveY", dir.y);
	}


	public void startrepeat()
	{
		transform.position = repeatstartpos;
		gameObject.SetActive(true);
		repeat = true;

	}

	public void git()
	{
		player.GetComponent<PlayerController>().repeat();
		moves = new List<Vector2>();
		attack = new List<bool>();
	
	}

}
