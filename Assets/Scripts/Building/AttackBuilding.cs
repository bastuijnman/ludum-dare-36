using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackBuilding : MonoBehaviour
{

	BuildingProperties properties;
	SphereCollider attackZone;

	List<GameObject> targets = new List<GameObject> ();

	float cooldown;

	void Start () {
		properties = GetComponent<BuildingProperties> ();

		attackZone = gameObject.AddComponent<SphereCollider> ();
		attackZone.isTrigger = true;
		attackZone.radius = properties.radius;

		cooldown = 0;
	}

	void OnTriggerEnter (Collider col) {

		if (targets.Count < properties.maxAllowedTargets) {
			targets.Add (col.gameObject);
		}
	}

	void Update () {
		
		if (targets.Count > 0 && cooldown <= 0) {
			for (int i = 0; i < targets.Count; i++) {
				Attack (targets[i]);
			}

			cooldown = properties.cooldown;
		} else if (cooldown > 0) {
			cooldown -= Time.deltaTime;
		}

	}

	void Attack (GameObject target) {
		Enemy enemy = target.GetComponent<Enemy> ();
		enemy.Damage (properties.damage);

		if (enemy.IsDead()) {
			targets.Remove (target);
		}
	}

}

