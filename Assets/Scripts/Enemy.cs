using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	EnemyProperties properties;

	void Start () {
		properties = GetComponent<EnemyProperties> ();
	}

	public void Damage (int damage) {
		properties.health -= (damage - properties.armor);

		if (properties.health <= 0) {
			Death ();
		}
	}

	public bool IsDead () {
		return properties.health <= 0;
	}

	public void Death () {
		Destroy (gameObject);
	}
}

