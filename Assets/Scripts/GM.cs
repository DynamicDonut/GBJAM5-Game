using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GM : MonoBehaviour {

	public float CustomerSpawnTime, gameLength;
	float last_CSTime = 0f;

	public Transform CustomersList;
	public List<Transform> customersWithoutOrders;
	int CustomerCount;

	public GameObject[] FlavorTanks, FlavorBars;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= last_CSTime) {
			for (int i = 0; i < CustomersList.childCount; i++) {
				if (CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent == false && !customersWithoutOrders.Contains (CustomersList.GetChild (i))) {
					customersWithoutOrders.Add (CustomersList.GetChild (i));
				} else {
					customersWithoutOrders.Remove(CustomersList.GetChild (i));
				}
			}

			for (int i = 0; i < customersWithoutOrders.Count; i++) {
				customersWithoutOrders[i].GetComponent<SpriteRenderer>().enabled = false;
				customersWithoutOrders[i].GetComponent<OrderScript> ().orderSent = false;
				customersWithoutOrders[i].GetComponent<OrderScript> ().myBubble.gameObject.SetActive (false);
			}
			customersWithoutOrders[Random.Range (0, customersWithoutOrders.Count)].GetComponent<OrderScript> ().SendOrder(10f);
			CustomerCount++;
			last_CSTime += CustomerSpawnTime;
		}

		//CustomersList.GetChild (0).GetComponent<OrderScript> ().SendOrder ();
	}
}
