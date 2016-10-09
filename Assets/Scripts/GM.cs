using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {

	public float CustomerSpawnTime, barRefillTime, gameLength;
	float last_CSTime, last_BRTime;
	public Transform CustomersList;
	Transform myHero;
	public List<Transform> customersWithoutOrders;
	int CustomerCount;

	public GameObject[] FlavorTanks, FlavorBars;

	// Use this for initialization
	void Start () {
		myHero = GameObject.Find ("Main Hero").transform;
		last_CSTime = last_BRTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= last_BRTime){
			for (int i = 0; i < FlavorBars.Length; i++) {
				if (FlavorBars [i].GetComponent<RectTransform > ().sizeDelta.y < 28 && !myHero.GetComponent<PlayerInput>().pouringDrink) {
					FlavorBars [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (3, FlavorBars [i].GetComponent<RectTransform> ().sizeDelta.y + 1);
				}
			}
			last_BRTime += barRefillTime;
		}

		if (Time.time >= last_CSTime) {
			for (int i = 0; i < CustomersList.childCount; i++) {
				if (!CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent && !customersWithoutOrders.Contains (CustomersList.GetChild (i))) {
					customersWithoutOrders.Add (CustomersList.GetChild (i));
				} else if (CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent){
					customersWithoutOrders.Remove (CustomersList.GetChild (i));
				}
			}

			for (int i = 0; i < customersWithoutOrders.Count; i++) {
				customersWithoutOrders[i].GetComponent<SpriteRenderer>().enabled = false;
				customersWithoutOrders[i].GetComponent<OrderScript> ().orderSent = false;
				customersWithoutOrders[i].GetComponent<OrderScript> ().myBubble.gameObject.SetActive (false);
			}
			if (customersWithoutOrders.Count > 0){
				customersWithoutOrders [Random.Range (0, customersWithoutOrders.Count)].GetComponent<OrderScript> ().SendOrder (10f);
				CustomerCount++;
			}
			last_CSTime += CustomerSpawnTime;
		}
	}
}
