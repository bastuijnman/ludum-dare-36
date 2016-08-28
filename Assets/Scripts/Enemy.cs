using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

	EnemyProperties properties;

	void Start () {
		FollowPath pathFinder = GetComponent<FollowPath> ();
		pathFinder.OnDestinationReached += OnDestinationReached;

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

	void OnDestinationReached () {
		Player player = FindObjectOfType<Player> ();

		Destroy (gameObject);
		if (player) {
			player.Damage (properties.damage);
		}
	}
}

