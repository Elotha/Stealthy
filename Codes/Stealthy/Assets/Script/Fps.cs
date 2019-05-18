using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
	float deltaTime = 0.0f;
	float fps;

	// Update is called once per frame
	void Update()
    {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		fps = 1.0f / deltaTime;
		GetComponent<Text>().text = fps.ToString();
    }
}
