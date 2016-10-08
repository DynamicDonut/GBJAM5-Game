using UnityEngine;
using System.Collections;

public class OrderScript : MonoBehaviour {
	public Sprite[] DrinkFlavors, BobaFlavors;
	public bool orderSent, orderDone;
	public Transform myBubble, myFlavor, myBoba;

	// Use this for initialization
	void Start () {
		myBubble = transform.FindChild (transform.name + " Bubble");
		myFlavor = myBubble.FindChild ("Flavor");
		myBoba = myBubble.FindChild ("Boba");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendOrder(float reqLength){
		if (!orderSent) {
			myFlavor.GetComponent<SpriteRenderer> ().sprite = DrinkFlavors [Random.Range (0, DrinkFlavors.Length)];
			myBoba.GetComponent<SpriteRenderer> ().sprite = BobaFlavors [Random.Range (0, BobaFlavors.Length)];
			orderSent = true;
		}
		StartCoroutine ("ActivateObjects");
	}

	IEnumerator ActivateObjects(){
		if (GetComponent<SpriteRenderer> ().enabled == false) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
		yield return new WaitForSeconds (2f);
		if (myBubble.gameObject.activeSelf == false) {
			myBubble.gameObject.SetActive (true);
		}
	}
}
