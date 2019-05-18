using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 0.13f;
	public Vector3 offset;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 desiredPosition = Vector3.zero;
		desiredPosition = desiredPosition + target.position;
		Vector3 CameraZ = transform.position - target.position;
		float distance = Mathf.Abs(CameraZ.x) + Mathf.Abs(CameraZ.y);
	

		desiredPosition = new Vector3(desiredPosition.x , desiredPosition.y, 0) + offset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
		
		transform.position = smoothedPosition;

	}

	public void ChangeTarget(Transform newTarget)
	{
		target = newTarget;
	}
}
