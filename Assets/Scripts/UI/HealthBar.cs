using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{

	Player player;
	Image health;

	void Start () {
		health = GetComponent<Image> ();
	}

	void Update ()
	{
		// If player wasnt set yet try to find it
		if (player == null) {
			player = FindObjectOfType<Player> ();

		}

		// If we have the player calculate the amount of health
		if (player != null) {
			RectTransform rectTransform = health.GetComponent<RectTransform> ();
			Debug.Log (player.GetHealthPercentage ());
			rectTransform.sizeDelta = new Vector2 (-10 - (190 * (1 - player.GetHealthPercentage())), rectTransform.sizeDelta.y);
		}
	}


}

