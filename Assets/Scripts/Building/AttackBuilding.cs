using UnityEngine;
using System.Collections;

public class AttackBuilding : MonoBehaviour
{

	BuildingProperties properties;
	SphereCollider collider;

	void Start () {
		properties = GetComponent<BuildingProperties> ();

		collider = gameObject.AddComponent<SphereCollider> ();
		collider.isTrigger = true;
		collider.radius = properties.radius;
	}

	void OnTriggerEnter (Collider col) {
		// TODO: target enemy
	}

}

