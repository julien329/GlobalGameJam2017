using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// VARIABLES
    ////////////////////////////////////////////////////////////////////////////////////////////////

    public float speed = 4f;                    // The speed that the player will move at.
    public float gravity = -9.8f;
    public float accelerationTime = 0.05f;
    public float rotationTime = 0.05f;

    private Vector3 movement;                   // The vector to store the direction of the player's movement.
    private Vector3 viewDirection;
    private Animator anim;                      // Reference to the animator component.
	private Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    private Vector3 velocity;
    private float activeVelocityXSmoothing;
    private float activeVelocityYSmoothing;


    ////////////////////////////////////////////////////////////////////////////////////////////////
    /// UNITY
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake() {
		anim = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
    }


	void Update() {
        Move();
        Rotate();
        Animating();
    }


    void FixedUpdate() {
        playerRigidbody.AddForce(new Vector3(0, gravity, 0), ForceMode.Acceleration);
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////
    /// METHODS
    ////////////////////////////////////////////////////////////////////////////////////////////////

    void Move() {
        // Get input value of left joystick
        float h = Input.GetAxis("LeftHorizontal");
        float v = Input.GetAxis("LeftVertical");

        // Set the movement vector based on the player input.
        velocity.x = Mathf.SmoothDamp(velocity.x, h, ref activeVelocityXSmoothing, accelerationTime);
        velocity.z = Mathf.SmoothDamp(velocity.z, v, ref activeVelocityYSmoothing, accelerationTime);
        velocity = Vector3.ClampMagnitude(velocity, 1.0f);

        // Move current position to target position, smoothed and scaled by speed
        playerRigidbody.MovePosition(transform.position + velocity * speed * Time.deltaTime);
	}


    void Rotate() {
        // Get input value of right joystick
        float h = Input.GetAxis("RightHorizontal");
        float v = Input.GetAxis("RightVertical");

        // Set looking direction of the player
        viewDirection.Set(h, 0f, v);
        viewDirection = (viewDirection == Vector3.zero) ? velocity : Vector3.ClampMagnitude(viewDirection, 1.0f);

        // If new direction, change rotation of the player
        if (viewDirection != Vector3.zero) {
            Quaternion targetRotation = Quaternion.LookRotation(viewDirection, Vector3.up);
            Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, rotationTime * Time.deltaTime);
            playerRigidbody.MoveRotation(newRotation);
        }
    }


    void Animating() {
		Vector3 cameraDirection = Vector3.Normalize(Camera.main.transform.forward);
		cameraDirection.y = 0f;

		// Where the player will move
		Vector3 movement_dir = new Vector3 (velocity.x, 0f, velocity.z);

        // Calculate mouvement direction
        float angle = Vector3.Angle(transform.forward, cameraDirection.normalized);
        angle = (Vector3.Angle(transform.right, cameraDirection.normalized) > 90f) ? 360f - angle : angle;
        movement_dir = Quaternion.AngleAxis (angle , Vector3.up) * movement_dir;
	
		//translated movement direction
		anim.SetFloat("Horizontal", movement_dir.x, 0.15f, Time.deltaTime);
		anim.SetFloat("Vertical", movement_dir.z, 0.15f, Time.deltaTime);
	}


	void StartAttackAnim() {
		anim.SetTrigger ("Attack");
	}
}


