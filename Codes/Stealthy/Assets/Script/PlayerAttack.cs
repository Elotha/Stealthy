using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	float timeToAttack;
	public float timeBtwAttakcs;

	public Transform attackPos;
	public LayerMask enemy;
	public float attackRange;
	public LightControl Lc;
	Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}
	
	private void Update()
	{
		if (timeToAttack <= 0)
		{

			if (Input.GetKey(KeyCode.F))
			{
				anim.SetBool("Wind", true);
				Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

				for (int i = 0; i < enemies.Length; i++)
				{
					Destroy(enemies[i].gameObject);
				}
				Lc.findLights();
				timeToAttack = timeBtwAttakcs;
			}

		}
		else if(timeToAttack <= 0.5 && timeToAttack >0)
		{
			anim.SetBool("Wind", false);
			timeToAttack -= Time.deltaTime;
		}
		else
		{
			timeToAttack -= Time.deltaTime;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPos.position,attackRange);
	}


}
