using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightControl : MonoBehaviour
{

	public Image FilledBar;
	Transform[] Lights;
	public Transform target;

    // Start is called before the first frame update
    void Start()
    {
		GameObject[] temp = GameObject.FindGameObjectsWithTag("Light");
		Lights = new Transform[temp.Length];
		for (int i = 0; i < temp.Length; i++)
		{
			Lights[i] = temp[i].transform;
		}
    }

    // Update is called once per frame
    void Update()
    {
		
		float mindis = Vector3.Distance(target.position, Lights[0].position);
		for(int i=1;i<Lights.Length;i++)
		{
			float temp = Vector3.Distance(target.position,Lights[i].position);

			if (temp < mindis)
			{
				mindis = temp;
			}
		}

		FilledBar.fillAmount = 1 - mindis;

    }

	public void changeTarget(Transform newTarget)
	{
		target = newTarget;
	}
}
