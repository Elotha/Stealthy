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

	private void Update()
	{
		
		if(timeToAttack <= 0)
		{

			if(Input.GetKey(KeyCode.F))
			{
				Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

				for (int i = 0; i < enemies.Length; i++)
				{
					Destroy(enemies[i].gameObject);
				}
				Lc.findLights();
				timeToAttack = timeBtwAttakcs;
				//Debug.Log(enemies.Length);
			}
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
