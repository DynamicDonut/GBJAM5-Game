using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {
	public float deadZoneVal;
	public int selectedFTank, currDrinkFill, currDrinkType, currDrinkBoba;
	public int maxDrinkFill;
	public bool pouringDrink, drinkNotSet, tankEmptied;
	Vector3 defaultSpot;

	Transform myGM;

	// Use this for initialization
	void Start () {
		myGM = GameObject.Find ("GameManager").transform;
		selectedFTank = 1;
		currDrinkFill = currDrinkType = currDrinkBoba = 0;
		drinkNotSet = true;
		defaultSpot = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!pouringDrink) {
			if (Input.GetAxis ("Horizontal") <= deadZoneVal * -1) {
				transform.position = defaultSpot + (Vector3.left * 51);
				selectedFTank = 0;
			} else if (Input.GetAxis ("Horizontal") >= deadZoneVal) {
				transform.position = defaultSpot + (Vector3.right * 51);
				selectedFTank = 2;
			} else {
				transform.position = defaultSpot;
				selectedFTank = 1;
			}
		}

		if (Input.GetAxis ("Vertical") <= deadZoneVal) {
			pouringDrink = false;
		} else {
			pouringDrink = true;
			PourBobaDrink ();
		}

		//Debug.Log (pouringDrink + ", " + selectedFTank);
	}

	void PourBobaDrink(){
		if (drinkNotSet) {
			currDrinkType = selectedFTank;
			drinkNotSet = false;
		}

		if (currDrinkType == selectedFTank) {
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
		yield return new WaitForSeconds (1f);
		tankEmptied = false;
		myGM.GetComponent<GM> ().FlavorBars [selectedFTank].GetComponent<RectTransform> ().sizeDelta = new Vector2 (3, myGM.GetComponent<GM> ().FlavorBars [selectedFTank].GetComponent<RectTransform> ().sizeDelta.y - 2);
	}
}
