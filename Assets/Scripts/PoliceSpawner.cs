using System;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour {
	[SerializeField] private Police _police;
	[SerializeField] private Transform _player;

	private void OnEnable() {
		InfluenceHandler.DispatchPolice += DispatchPolice;
	}

	private void OnDisable() {
		InfluenceHandler.DispatchPolice -= DispatchPolice;
	}

	private void DispatchPolice() {
		Vector3 direction = _player.position.x > 0 ? new Vector3(1,0.5f,0) : new Vector3(0,0.5f,0);
		Vector3 location = Camera.main.ViewportToWorldPoint(direction);
		location.z = 0;
		location.y = _player.position.y + 0.2f;
		for (int i = 0; i < 3; i++) {
			var police = Instantiate(_police, location, Quaternion.identity, transform);
			var localScale = police.transform.localScale;
			localScale.x = _player.position.x > 0 ? -1 : 1;
			police.transform.localScale = localScale;
			location.x += (_player.position.x > 0 ? 1 : -1) * 0.04f;
		}
	}
}
