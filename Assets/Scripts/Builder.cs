using UnityEngine;
using System.Collections;

public class Builder : MonoBehaviour {

	bool placing = false;

	GameObject ghost;

	public void CreateBuilding (GameObject building) {
		if (placing) {
			return;
		}

		ghost = Instantiate (building);
		ghost.AddComponent<BuildingRadiusCircle> ();

		placing = true;
	}

	void Update () {
		
		if (placing) {
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast(ray, out hit, 10000)) {
				if (hit.collider.name == "Terrain") {
					ghost.transform.position = hit.point;
				}
			}

			if (Input.GetMouseButton(0)) {

				// TODO: attach correct building behaviours?
				ghost.AddComponent<AttackBuilding>();

				// Place down our building
				placing = false;
				ghost = null;
			}
		}

	}

}
