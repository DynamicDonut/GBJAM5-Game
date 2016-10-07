using UnityEngine;
using System.Collections;

public class SunDialScript : MonoBehaviour {
	
	Vector3 sunPosition;
	float currTime;
	Transform sunIcon;

	[Range(0f, 2.0f)]
	public float gameTime;
	public float maxGameTime;

	// Use this for initialization
	void Start () {
		gameTime = 0f;
		sunIcon = transform.FindChild ("Sun-Icon");
		sunPosition = sunIcon.position;
	}
	
	// Update is called once per frame
	void Update () {
		currTime = Mathf.Clamp (Time.time, 0f, 60f);
		gameTime = mapVal (currTime, 0f, 60f, 0f, 2f);

		sunPosition.x = (gameTime - 1f) * 69;
		sunIcon.position = sunPosition;
	}

	public static float mapVal(float value, float lMin, float lMax, float rMin, float rMax){
		return rMin + (value - lMin) * (rMax - rMin) / (lMax - lMin);
	}
}
