using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flash : MonoBehaviour
{
    public GameObject Laser;
    public float time;

    public void Start()
    {
		time = 3f;
    }

    private void Update()
    {
		time -= Time.deltaTime;
        

        if (time < 0)
        {
            Laser.GetComponent<Animator>().SetBool("play", false);
        }
   
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag== "Player")
        {
            Laser.GetComponent<Animator>().SetBool("play", true);
            other.GetComponent<PlayerController>().dead();
			time = 3;
          
        }
    }
}
