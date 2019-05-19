using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
	public bool isRewinding;

	List<Vector3> positions;
	List<Vector2> moves;
    // Start is called before the first frame update
    void Start()
    {
		positions = new List<Vector3>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
		if(isRewinding)
		{
			rePlay();
		}
		else
		{
			Record();
		}
    }

	void rePlay()
	{
		if(positions.Count>0)
		{
			transform.position = positions[0];
			positions.RemoveAt(0);
		}
		else
		{
			gameObject.SetActive(false);
			GetComponent<TimeBody>().enabled = false;
		}
		
	}

	void Record()
	{
		positions.Add(transform.position);
	}

	public void StartRewind()
	{
		isRewinding = true;
	}

	public void StopRewind()
	{
		isRewinding = false;
	}
}
