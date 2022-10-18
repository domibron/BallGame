using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	[SerializeField]
	private float minimumDistance = .2f;
	[SerializeField]
	private float maximumTime = 1f;
	[SerializeField, Range(0f, 1f)]
	private float directionThreshold = .9f;
	[SerializeField]
	private float rotationAmmount;

	public Transform vertiualCamera;
	public Transform orientation;
	public Transform mainCamera;


	private InputManager inputManager;

	private Rigidbody rb;

	private Vector2 startPosision;
	private float startTime;
	private Vector2 endPosision;
	private float endTime;

	private float yRotation;

	private Coroutine coroutine;

	void Awake()
	{
		inputManager = InputManager.Instance;
		rb = GetComponent<Rigidbody>();
	}

	void OnEnable()
	{
		inputManager.OnStartPrimaryTouch += SwipeStart;
		inputManager.OnEndPrimaryTouch += SwipeEnd;
	}

	void OnDisable()
	{
		inputManager.OnStartPrimaryTouch -= SwipeStart;
		inputManager.OnEndPrimaryTouch -= SwipeEnd;
	}

	private void SwipeStart(Vector2 pos, float time)
	{
		startPosision = pos;
		startTime = time;
	}

	private void SwipeEnd(Vector2 pos, float time)
	{

		endPosision = pos;
		endTime = time;
		DetectSwipe();
	}



	private void DetectSwipe()
	{
		if (Vector3.Distance(startPosision, endPosision) >= minimumDistance && (endTime - startTime) <= maximumTime)
		{
			Vector3 direction = endPosision - startPosision;
			Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
			SwipeDirection(direction2D);
		}
	}

	private void SwipeDirection(Vector2 direction)
	{
		//print(direction);
		float percentOfSwipe = Vector2.Dot(Vector2.up, direction);
		float rotationY = 180 * vertiualCamera.rotation.y; // try local with main camera

		if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
			orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, rotationY, orientation.rotation.z);
			rb.AddForce(orientation.forward, ForceMode.Impulse);
		}
		else if (direction.x > 0) // right
		{
			
			print("right " + percentOfSwipe);

			if (direction.y > 0) // can use the whole 90 to -90 / 1 to -1
			{
				// top right 90 to 0
				percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to 90

				float ninetyPercent = 90 * percentOfSwipe;

				float ammountToRotate = rotationY + ninetyPercent;

				orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, ammountToRotate, orientation.rotation.z);
				rb.AddForce(orientation.forward, ForceMode.Impulse);

			}
			else
			{
				// bottom right 90 to 180

				float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

				float ammountToRotate = (rotationY + 90) + ninetyPercent;

				orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, ammountToRotate, orientation.rotation.z);
				rb.AddForce(orientation.forward, ForceMode.Impulse);

			}
			
		}
		else // left
		{
			print("Left " + percentOfSwipe);

			if (direction.y > 0)
			{
				// top left, -90 to 0

				percentOfSwipe = 1 - percentOfSwipe; // inverts the percent so is 0 to -90

				float ninetyPercent = 90 * -percentOfSwipe; // look at this -Var might not work

				float ammountToRotate = rotationY + ninetyPercent;

				orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, ammountToRotate, orientation.rotation.z);
				rb.AddForce(orientation.forward, ForceMode.Impulse);


			}
			else
			{

				// bottom left -90 to -180
				float ninetyPercent = 90 * percentOfSwipe;

				float ammountToRotate = (rotationY - 90) + ninetyPercent;

				orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, ammountToRotate, orientation.rotation.z);
				rb.AddForce(orientation.forward, ForceMode.Impulse);
				
			}
		}


		//print(Vector2.Dot(Vector2.up, direction));
		
		//if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
		//{
		//	//print("swiped up");
		//	rb.AddForce(orientation.forward, ForceMode.Impulse);
		//}
		//else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
		//{
		//	//print("swiped right");
		//	yRotation += rotationAmmount;
		//	//transform.rotation = Quaternion.Euler(transform.rotation.x + rotationAmmount, transform.rotation.y, transform.rotation.z);
		//	orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, yRotation, orientation.rotation.z);

		//}
		//else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
		//{
		//	//print("swiped down");
		//	rb.AddForce(-orientation.forward, ForceMode.Impulse);
		//}
		//else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
		//{
		//	//print("swiped left");
		//	yRotation -= rotationAmmount;
		//	//transform.rotation = Quaternion.Euler(transform.rotation.x - rotationAmmount, transform.rotation.y, transform.rotation.z);
		//	orientation.transform.rotation = Quaternion.Euler(orientation.rotation.x, yRotation, orientation.rotation.z);
		//}
	}
}
