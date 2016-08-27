using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private int money = 500;

	public int GetMoney () {
		return money;
	}

	public bool Buy (int price) {
		if (price > money) {
			return false;
		}

		money -= price;

		return true;
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			// TODO: place tower
		}

	}
}
