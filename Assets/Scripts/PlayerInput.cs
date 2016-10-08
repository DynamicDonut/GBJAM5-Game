using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public float deadZoneVal;
	Vector3 defaultSpot;

	// Use this for initialization
	void Start () {
		defaultSpot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") <= deadZoneVal * -1) {
			transform.position = defaultSpot + (Vector3.left * 51);
		} else if (Input.GetAxis ("Horizontal") >= deadZoneVal) {
			transform.position = defaultSpot + (Vector3.right * 51);
		} else {
			transform.position = defaultSpot;
		}
	}
}
