using UnityEngine;
using System.Collections;

public class BuildingRadiusCircle : MonoBehaviour {

	public float thetaScale = 0.01f;

	LineRenderer lineRenderer;

	float theta = 0f;

	void Start () {
		lineRenderer = gameObject.AddComponent<LineRenderer> ();
		lineRenderer.SetWidth (0.25f, 0.25f);
		lineRenderer.SetColors (Color.green, Color.red);
	}

	void Update () {
		int size = (int)((1f / thetaScale) + 1f);
		int radius = GetComponent<BuildingProperties> ().radius;

		lineRenderer.SetVertexCount (size);

		theta = 0f;

		for (int i = 0; i < size; i++) {
			theta += (2.0f * Mathf.PI * thetaScale);

			lineRenderer.SetPosition (i, new Vector3 (
				radius * Mathf.Cos (theta) + transform.position.x,
				0.1f,
				radius * Mathf.Sin (theta) + transform.position.z
			));
		}
	}
}
