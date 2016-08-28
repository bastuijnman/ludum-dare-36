using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

class Enemy {
	public string type;
	public int number;
}

class Wave {

	// Enemy definitions
	public Enemy[] enemies;

	// Wave has started
	public bool started = false;

	// Wave has completed
	public bool completed = false;

	// Current countdown for spawn delay
	public float currentSpawnDelay = SPAWN_DELAY;

	public float currentCooldownDelay = COOLDOWN_DELAY;

	// Spawn queue
	public List<Action> queue = new List<Action> ();

	// List of all spawns
	public List<GameObject> spawns = new List<GameObject>();

	public bool AllEnemiesDied() {
		for (int i = 0; i < spawns.Count; i++) {
			if (!spawns[i].GetComponent<EnemyProperties>().IsDead()) {
				return false;
			}
		}
		return true;
	}

	public void Spawn (GameObject enemy, Vector3 position, GameObject path) {
		enemy.transform.position = position;

		enemy.AddComponent<FollowPath> ();
		enemy.AddComponent<Rigidbody> ();
		enemy.GetComponent<FollowPath> ().path = path;
		enemy.GetComponent<Rigidbody> ().isKinematic = true;
	}

	public static float SPAWN_DELAY = 2.0f;
	public static float COOLDOWN_DELAY = 30.0f;
}

public class WaveManager : MonoBehaviour
{

	int currentWave = 0;

	Wave[] waves = new Wave[] {
		// Wave 1
		new Wave { 
			enemies = new Enemy[] {
				new Enemy { type = "Test", number = 5 }
			}
		},

		// Wave 2
		new Wave { 
			enemies = new Enemy[] {
				new Enemy { type = "Test", number = 5 }
			}
		}
	};

	public GameObject path;
	public GameObject waveCompleteScreen;
	public GameObject waveCooldownScreen;
	
	void Update ()
	{
		Wave current = waves [currentWave];

		if (current.currentCooldownDelay > 0) {
			CooldownCountdown ();
			return;
		} else {
			waveCooldownScreen.SetActive (false);
		}

		if (!current.started) {

			// Referencing issue?
			current.started = true;

			foreach (Enemy enemy in current.enemies) {

				UnityEngine.Object resource = Resources.Load ("Prefabs/Enemies/" + enemy.type);

				for (int i = 0; i < enemy.number; i++) {
					current.queue.Add (() => {
						current.Spawn(
							Instantiate (resource) as GameObject,
							transform.position,
							path
						);
					});
				}

			}
		} else if (!current.completed) {

			current.currentSpawnDelay -= Time.deltaTime;
			if (current.currentSpawnDelay < 0) {
				current.currentSpawnDelay = Wave.SPAWN_DELAY;

				if (current.queue.Count == 0 && current.AllEnemiesDied()) {

					/*
					 * If a wave complete screen is set, activate it and
					 * remove it after the cooldown.
					 */
					if (waveCompleteScreen != null) {
						waveCompleteScreen.SetActive (true);

						// TODO: remove after cooldown
					}

					current.completed = true;
					return;
				}

				current.queue [0] ();
				current.queue.RemoveAt (0);
			}

		} else {
			if (currentWave < waves.Length - 1) {
				currentWave++;
			}
		}
	}

	void CooldownCountdown () {
		Wave current = waves [currentWave];

		current.currentCooldownDelay -= Time.deltaTime;
		waveCooldownScreen.SetActive (true);


		Text text = waveCooldownScreen.GetComponentInChildren<Text> ();
		text.text = "Next Wave in: " + Mathf.CeilToInt(current.currentCooldownDelay) + "s";
	}

}

