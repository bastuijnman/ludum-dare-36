using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Does a player have fire?
    bool hasFire = true;

    // Player flint inventory
    int flint = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public bool PlayerHasFire () {
        return hasFire;
    }
}
