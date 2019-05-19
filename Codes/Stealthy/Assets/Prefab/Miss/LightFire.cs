using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFire : MonoBehaviour
{
	public GameObject fire;
	
	float time;
	float time_;
	bool fired;
	private void Start()
	{
		fired = false;
		time = 1f;
		time_ = time;
	}

	private void Update()
	{
		
		if(time<0)
		{
			if(fired)
			{
				Instantiate(fire, transform.position, Quaternion.identity);
				fired = false;
				time = time_;
				GetComponent<Collider2D>().enabled = false;
			}
			else
			{
				GetComponent<Collider2D>().enabled = true;
				time -= Time.deltaTime;
			}
			
		}
		else
		{
			time -= Time.deltaTime;
		}


	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			fired = true;
		}

	}
}
