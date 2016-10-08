using UnityEngine;
using System.Collections;

public class GM : MonoBehaviour {

	public float CustomerSpawnTime;
	float last_CSTime = 0f;

	public Transform CustomersList;
	int CustomerCount;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= last_CSTime) {
			CustomerCount++;
			for (int i = 0; i < CustomersList.childCount; i++) {
				CustomersList.GetChild (i).GetComponent<SpriteRenderer> ().enabled = false;
				CustomersList.GetChild (i).GetComponent<OrderScript> ().orderSent = false;
				CustomersList.GetChild (i).GetComponent<OrderScript> ().myBubble.gameObject.SetActive (false);
			}
			CustomersList.GetChild (Random.Range (0, CustomersList.childCount)).GetComponent<OrderScript> ().SendOrder(10f);
			last_CSTime += CustomerSpawnTime;
		}

		//CustomersList.GetChild (0).GetComponent<OrderScript> ().SendOrder ();
	}
}
