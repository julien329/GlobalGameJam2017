using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 4f;            // The speed that the player will move at.

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

	public LayerMask floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.

	public Plane ground;
	//Transform hips;


	void Awake()
	{
		//floorMask = LayerMask.GetMask("Floor");
		Debug.Log (floorMask.value);
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
		//hips = transform.Find("Armature/hipsCtrl");
	}


	void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		Turning ();
		Animating (h, v);
	}

	void LateUpdate()
	{

	}

	//--------------------------- Methods ----------------------------//

	// General Movement
	void Move(float h, float v)
	{
		Vector3 cameraDirection = Vector3.Normalize(Camera.main.transform.forward);
		cameraDirection.y = 0f;

		float angle = Vector3.Angle (Vector3.forward, cameraDirection.normalized);
		movement = Vector3.Normalize(new Vector3(h, 0f, v));

		movement = Quaternion.AngleAxis(-angle, Vector3.up) * movement;

		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + movement);
	}

	// General Turning method
	void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, Mathf.Infinity, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			Quaternion rotation = Quaternion.LookRotation (playerToMouse);
			transform.rotation = rotation;
			//playerRigidbody.MoveRotation (rotation);

		}
	}

	// Animate the character with the right animation clip
	void Animating(float h, float v)
	{

		Vector3 cameraDirection = Vector3.Normalize(Camera.main.transform.forward);
		cameraDirection.y = 0f;

		// Where the player will move
		Vector3 movement_dir = new Vector3 (h, 0f, v); 

		movement_dir = Quaternion.AngleAxis (angle360 (transform.forward, cameraDirection.normalized, transform.right), Vector3.up) * movement_dir;
	
		//translated movement diretion
		anim.SetFloat("Horizontal", movement_dir.x, 0.15f, Time.deltaTime);
		anim.SetFloat("Vertical", movement_dir.z, 0.15f, Time.deltaTime);
	}


	float angle360(Vector3 from, Vector3 to, Vector3 right) {
		float angle = Vector3.Angle (from, to);
		return (Vector3.Angle(right,to) > 90f) ? 360f - angle : angle;
	}
}


