using UnityEngine;
using System.Collections;

public class DropOnContact : MonoBehaviour {

    public GameObject dropObject;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.name == "Joe") {
            Player p = col.gameObject.GetComponent("Player") as Player;

            if (p.PlayerHasFire() == true && dropObject.GetComponent("Rigidbody") as Rigidbody == null) {
                dropObject.AddComponent<Rigidbody>();
            }
        }
    }
}
