using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public float spd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime * spd);
	}
}
