using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour {

	public float speed = 10;
	private Rigidbody2D rb;
	Vector2 direction;
	public float damage = 5;
	public float _delay = 2;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		direction = rb.position;
	}

	// Update is called once per frame
	void Update()
	{
		direction.Normalize();
		rb.velocity = transform.up * speed;
		
		_delay -= Time.deltaTime;
		if (_delay <= 0)
		{
			Destroy(gameObject);
		}
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.gameObject.tag == "Player")
		{
			//collision.gameObject.GetComponent<SpaceShip>().takeDamage(damage);
		}
		


		Destroy(gameObject);
	}


}
