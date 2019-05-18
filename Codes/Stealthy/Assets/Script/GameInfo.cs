using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void StopGuards()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Guard");
		for (int i = 0; i < temp.Length; i++)
		{
			temp[i].GetComponent<Guard>().speed = 0f;
		}
	}

	public void GoGuards()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Guard");
		for (int i = 0; i < temp.Length; i++)
		{
			temp[i].GetComponent<Guard>().speed = 0.006f;
		}
	}
}
