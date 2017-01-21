using UnityEngine;
using System.Collections;

public class AttachToHand : MonoBehaviour {

	public GameObject weapon;
	Transform hand;
	// Use this for initialization

	void Start () {
		hand = transform.Find ("Armature/hipsCtrl/Root/Spine1/spine2/shoulder_R/upArm_R/lowArm_R/hand_R");
		weapon.transform.localPosition = new Vector3 (-0.87f, -0.12f, 0.33f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 handRotation = hand.rotation.eulerAngles;
		weapon.transform.rotation = Quaternion.Euler(handRotation.x,handRotation.y,180 + handRotation.z);
	}
}
