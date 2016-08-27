using UnityEngine;
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

	// Spawn delay
	public float delay = 2.0f;

	// Current countdown for spawn delay
	public float currentDelay = 2.0f;

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
	
	void Update ()
	{
		Wave current = waves [currentWave];

		if (!current.started) {

			// Referencing issue?
			waves [currentWave].started = true;

			foreach (Enemy enemy in current.enemies) {

				UnityEngine.Object resource = Resources.Load ("Prefabs/Enemies/" + enemy.type);

				for (int i = 0; i < enemy.number; i++) {
					waves [currentWave].queue.Add (() => {
						waves[currentWave].Spawn(
							Instantiate (resource) as GameObject,
							transform.position,
							path
						);
					});
				}

			}
		} else if (!current.completed) {

			waves [currentWave].currentDelay -= Time.deltaTime;
			if (waves [currentWave].currentDelay < 0) {
				waves [currentWave].currentDelay = waves[currentWave].delay;

				if (waves[currentWave].queue.Count == 0 && waves[currentWave].AllEnemiesDied()) {

					/*
					 * If a wave complete screen is set, activate it and
					 * remove it after the cooldown.
					 */
					if (waveCompleteScreen != null) {
						waveCompleteScreen.SetActive (true);

						// TODO: remove after cooldown
					}

					waves [currentWave].completed = true;
					return;
				}

				waves [currentWave].queue [0] ();
				waves [currentWave].queue.RemoveAt (0);
			}

		} else {
			if (currentWave < waves.Length - 1) {
				currentWave++;
			}
		}
	}

}

