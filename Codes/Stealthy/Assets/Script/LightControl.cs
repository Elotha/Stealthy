using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightControl : MonoBehaviour
{

	public Image FilledBar;
	Transform[] Lights;
	public GameObject Player;
	public GameObject target;
	int lightcount;
    // Start is called before the first frame update
    void Start()
    {
		findLights();
	}

    // Update is called once per frame
    void Update()
    {
		
		float mindis = 1000;
		for(int i=0;i<Lights.Length;i++)
		{
			if(Lights[i]!=null)
			{
				float temp = Vector3.Distance(target.transform.position, Lights[i].position);

				if (temp < mindis)
				{
					mindis = temp;
				}
			}
			
		}

		FilledBar.fillAmount = 1 - mindis;
		if(mindis>0.3f)
		{
			Player.GetComponent<PlayerController>().canToggle = true;
		}
		else
		{
			Player.GetComponent<PlayerController>().canToggle = false;
		}
    }

	public void findLights()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Light");
		Lights = new Transform[temp.Length];
		for (int i = 0; i < temp.Length; i++)
		{
			Lights[i] = temp[i].transform;
		}
		lightcount = Lights.Length;
	}

	public void changeTarget(GameObject newTarget)
	{
		target = newTarget;
	}
}
