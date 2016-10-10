using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public float deadZoneVal;
	public int selectedCol, currDrinkFill, currDrinkType, currDrinkBoba;
	public int maxDrinkFill, myScore;
	public bool pouringDrink, drinkNotSet, tankEmptied;
	Vector3 defaultSpot;

	Transform myGM;

	// Use this for initialization
	void Start () {
		myGM = GameObject.Find ("GameManager").transform;
		selectedCol = 1;
		currDrinkFill = currDrinkType = currDrinkBoba = 0;
		drinkNotSet = true;
		defaultSpot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GM.finalScore = myScore;
		if (!pouringDrink) {
			if (Input.GetAxis ("Horizontal") <= deadZoneVal * -1) {
				transform.position = defaultSpot + (Vector3.left * 51);
				selectedCol = 0;
			} else if (Input.GetAxis ("Horizontal") >= deadZoneVal) {
				transform.position = defaultSpot + (Vector3.right * 51);
				selectedCol = 2;
			} else {
				transform.position = defaultSpot;
				selectedCol = 1;
			}
		}

		if (Input.GetAxis ("Vertical") <= deadZoneVal) {
			pouringDrink = false;
		} else {
			pouringDrink = true;
			if (currDrinkFill != maxDrinkFill) {
				PourBobaDrink ();
			}
		}

		if (Input.GetButtonUp ("AButton") || Input.GetButtonUp ("BButton") ) {
			Transform myCustomer = myGM.GetComponent<GM> ().CustomersList.GetChild (selectedCol);
			if (currDrinkFill == maxDrinkFill && myCustomer.GetComponent<OrderScript>().orderSent) {
				myScore++;
				if (System.Convert.ToInt32(myCustomer.GetComponent<OrderScript> ().myFlavor.GetComponent<SpriteRenderer>().sprite.name.Substring(12)) == currDrinkType + 1) {
					myScore++;
				}
				if (System.Convert.ToInt32(myCustomer.GetComponent<OrderScript> ().myBoba.GetComponent<SpriteRenderer>().sprite.name.Substring(4)) == currDrinkBoba + 1) {
					myScore++;
				}
			}

			currDrinkFill = 0;
			drinkNotSet = true;

			myCustomer.GetComponent<SpriteRenderer>().enabled = false;
			myCustomer.GetComponent<OrderScript> ().orderSent = false;
			myCustomer.GetComponent<OrderScript> ().myBubble.gameObject.SetActive (false);
		}
	}

	void PourBobaDrink(){
		if (drinkNotSet) {
			currDrinkType = selectedCol;
			drinkNotSet = false;
		}

		if (currDrinkType == selectedCol) {
			if (currDrinkFill < maxDrinkFill) {
				if (!tankEmptied) {				
					StartCoroutine ("FillMath");
				}
			} else {
				currDrinkFill = maxDrinkFill;
			}
		}
	}

	IEnumerator FillMath(){
		currDrinkFill += 2;
		tankEmptied = true;
		yield return new WaitForSeconds (0.5f);
		tankEmptied = false;
		myGM.GetComponent<GM> ().FlavorBars [selectedCol].GetComponent<RectTransform> ().sizeDelta = new Vector2 (3, myGM.GetComponent<GM> ().FlavorBars [selectedCol].GetComponent<RectTransform> ().sizeDelta.y - 2);
	}
}
