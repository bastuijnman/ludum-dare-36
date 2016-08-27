using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour
{

	public int health;

	public int armor;

	public void Damage (int damage) {
		health -= (damage - armor);

		if (health <= 0) {
			Death ();
		}
	}

	public bool IsDead () {
		return health <= 0;
	}

	public void Death () {
		Destroy (gameObject);
	}

}

