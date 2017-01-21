using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 4f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.

	float currentOrientation = 0;
	Transform hips;


    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
		hips = transform.Find("Armature/hipsCtrl");
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
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
	}

	// General Turning method
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
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
		// Where the player will move
		Vector3 movement_dir = new Vector3 (h, 0f, v); 

		Quaternion rotation = Quaternion.FromToRotation(transform.forward,Vector3.forward);
		// translated movement diretion
		movement_dir = rotation * movement_dir;

		anim.SetFloat("Horizontal", movement_dir.x, 0.15f, Time.deltaTime);
		anim.SetFloat("Vertical", movement_dir.z, 0.15f, Time.deltaTime);
    }
		
}
