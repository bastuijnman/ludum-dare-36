using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

class Enemy {
	public string type;
	public int number;
}

class Wave {
	public Enemy[] enemies;
	public bool started = false;
	public bool completed = false;
	public float delay = 2.0f;
	public float currentDelay = 2.0f;
	public List<Action> queue = new List<Action> ();
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
						GameObject e = Instantiate (resource) as GameObject;
						e.transform.position = transform.position;

						e.AddComponent<FollowPath> ();
						e.AddComponent<Rigidbody> ();
						e.GetComponent<FollowPath> ().path = path;
						e.GetComponent<Rigidbody> ().isKinematic = true;
					});
				}
			}
		} else if (!current.completed) {

			waves [currentWave].currentDelay -= Time.deltaTime;
			if (waves [currentWave].currentDelay < 0) {
				waves [currentWave].currentDelay = waves[currentWave].delay;

				if (waves[currentWave].queue.Count == 0) {
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

