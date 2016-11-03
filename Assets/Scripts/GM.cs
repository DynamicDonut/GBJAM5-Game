using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public float CustomerSpawnTime, barRefillTime, gameLength;
	float last_CSTime, last_BRTime, startofGame;
	public Transform CustomersList;
	public Transform myHero, myUI;
	public List<Transform> customersWithoutOrders;
	int CustomerCount;
	public bool startingGame = true;
	public static int finalScore;
	public GameObject[] FlavorTanks, FlavorBars;

	void Start(){
		//startofGame = Time.time;
	}

	// Update is called once per frame
	void Update () {
		myUI = GameObject.Find ("Canvas").transform;
		Debug.Log (Time.time);
		if (startingGame){
			startofGame = Time.time;
			startingGame = false;
		}
		if (SceneManager.GetActiveScene ().name == "MainMenuScene") {
			Time.timeScale = 0;

			finalScore = 0;
			last_CSTime = last_BRTime = 0f;

			if (Input.GetButtonUp ("Action")) {
				if (myUI.FindChild("Start Page").gameObject.activeSelf) {
					myUI.FindChild("Start Page").gameObject.SetActive (false);
					myUI.FindChild("Instructions Page").gameObject.SetActive (true);
				} else {
					myUI.FindChild("Instructions Page").gameObject.SetActive (false);
					myUI.FindChild("Start Page").gameObject.SetActive (true);
				}
			}

			if (Input.GetButtonUp ("Start")) {
				SceneManager.LoadScene ("GameplayScene");
			}
		} else if (SceneManager.GetActiveScene ().name == "GameplayScene") {
			Time.timeScale = 1f;
			myHero = GameObject.Find ("Main Hero").transform;
			CustomersList = GameObject.Find ("Customers").transform;
			GameObject.Find ("TableScore").GetComponent<Text> ().text = "Score: " + finalScore.ToString ();
			//Debug.Log (myUI.childCount);

			for (int i = 0; i < myUI.childCount-1; i++) {
				FlavorTanks [i] = GameObject.Find ("FlavorTank" + (i+1));
				FlavorBars [i] = myUI.FindChild("FlavorBar" + (i+1)).FindChild ("Meter").gameObject;
			}

			if (Time.time >= last_BRTime) {
				for (int i = 0; i < FlavorBars.Length; i++) {
					if (FlavorBars [i].GetComponent<RectTransform > ().sizeDelta.y < 28 && !myHero.GetComponent<PlayerInput> ().pouringDrink) {
						FlavorBars [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (3, FlavorBars [i].GetComponent<RectTransform> ().sizeDelta.y + 1);
					}
				}
				last_BRTime += barRefillTime;
			}

			if (Time.time >= last_CSTime) {
				for (int i = 0; i < CustomersList.childCount; i++) {
					if (!CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent && !customersWithoutOrders.Contains (CustomersList.GetChild (i))) {
						customersWithoutOrders.Add (CustomersList.GetChild (i));
					} else if (CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent) {
						customersWithoutOrders.Remove (CustomersList.GetChild (i));
					}
				}

				for (int i = 0; i < customersWithoutOrders.Count; i++) {
					customersWithoutOrders [i].GetComponent<SpriteRenderer> ().enabled = false;
					customersWithoutOrders [i].GetComponent<OrderScript> ().orderSent = false;
					customersWithoutOrders [i].GetComponent<OrderScript> ().myBubble.gameObject.SetActive (false);
				}
				if (customersWithoutOrders.Count > 0) {
					customersWithoutOrders [Random.Range (0, customersWithoutOrders.Count)].GetComponent<OrderScript> ().SendOrder (10f);
					CustomerCount++;
				}
				last_CSTime += CustomerSpawnTime;
			}

			if (Time.time >= startofGame + gameLength) {
				finalScore = myHero.GetComponent<PlayerInput> ().myScore;
				SceneManager.LoadScene ("ScoreScene");
			}
		} else if (SceneManager.GetActiveScene ().name == "ScoreScene"){
			GameObject.Find ("Score").GetComponent<Text> ().text = finalScore.ToString();
			startingGame = true;

			if (Input.GetButtonUp("Start")) {
				SceneManager.LoadScene ("MainMenuScene");
			}
		}
	}
}
