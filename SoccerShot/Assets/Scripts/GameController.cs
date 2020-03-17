using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	public GameObject ballPrefab;

	[SerializeField]
	float ballForce;

	GameObject ballInstance;
	Vector3 mouseStart;
	Vector3 mouseEnd;

	float minDragDistance = 15f;
	float zDepth = 25f;

	// Start is called before the first frame update
	void Start()
	{
		CreateBall();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			mouseStart = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0))
		{
			mouseEnd = Input.mousePosition;

			if(Vector3.Distance(mouseEnd,mouseStart) > minDragDistance)
			{
				//throw the ball if true. need rotation of ball for direction and add force 
				Vector3 hitPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zDepth);
				
				//need to convert screen position to world position if yo want to use the mouse position to change something
				hitPos = Camera.main.ScreenToWorldPoint(hitPos);

				//rotate ball to hitPos.
				ballInstance.transform.LookAt(hitPos);

				//add relative force to make it local rb direction that was just changed
				ballInstance.GetComponent<Rigidbody>().AddForce(Vector3.forward * ballForce, ForceMode.Impulse);
			}

		}


	}

	void CreateBall()
	{
		ballInstance = Instantiate(ballPrefab, ballPrefab.transform.position, Quaternion.identity) as GameObject;
	}
}
