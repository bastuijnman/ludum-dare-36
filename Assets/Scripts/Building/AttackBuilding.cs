using UnityEngine;
using System.Collections;

public class AttackBuilding : MonoBehaviour
{

	BuildingProperties properties;
	SphereCollider attackZone;

	void Start () {
		properties = GetComponent<BuildingProperties> ();

		attackZone = gameObject.AddComponent<SphereCollider> ();
		attackZone.isTrigger = true;
		attackZone.radius = properties.radius;
	}

	void OnTriggerEnter (Collider col) {
		// TODO: target enemy
	}

}

