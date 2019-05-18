using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
	public GameObject[] sight;
	int count;
	public Transform[] targets;
	public float speed;

	int direction;
	// Start is called before the first frame update
	void Start()
    {
		speed = 0.006f;
		count = 0;
		direction = 1;
		for (int i = 0; i < sight.Length; i++)
		{
			sight[i].SetActive(false);
		}
			sight[direction - 1].SetActive(true);
	}
	
	private void FixedUpdate()
	{
		if(targets.Length>0)
		{
			if (followpatrol(targets[count], speed) == 0)
			{
				count = count + 1;
				if (count == targets.Length)
				{
					count = 0;
				}
			}
		}
	}

	int followpatrol(Transform target, float speed)
	{ 
		transform.position = Vector3.MoveTowards(transform.position, target.position, speed);

		Vector2 temp = target.position - transform.position;

		if ((temp).y < -0.2) 
		{
			turn(2);
		}
		else if (temp.y > 0.2)
		{
			turn(1);
		}
		else if (temp.x > 0.2)
		{
			turn(3);
		}
		else if (temp.x < -0.2)
		{
			turn(4);
		}

		
		if (Vector3.Distance(transform.position, target.position)<0.05f)
		{
			return 0;
		}
		return 1;
	}


	void turn(int dir)
	{

		for (int i = 0; i < sight.Length; i++)
		{
			sight[i].SetActive(false);
		}
		sight[dir - 1].SetActive(true);
	}
}
