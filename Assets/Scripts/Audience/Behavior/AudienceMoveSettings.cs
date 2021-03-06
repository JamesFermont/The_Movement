using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "AudienceMovement", menuName = "AudienceConfiguration/AudienceMovement", order = 1)]
public class AudienceMoveSettings : ScriptableObject {
	public List<AudienceMoveConfig> movePatterns;

	private static List<AudienceMoveConfig> _configs;

	private void OnEnable() {
		_configs = movePatterns;
	}

	public AudienceMoveConfig GetRandomMoveConfig() => 
		_configs[Random.Range(0, _configs.Count)];
}

[Serializable]
public struct AudienceMoveConfig {
	public float movementSpeed;
	public float movementDuration;
	public float idleTime;
	public int direction;

	public BehaviorOption canChangeDirection;
	public BehaviorOption canIdle;
}

[Serializable]
public struct BehaviorOption {
	[SerializeField] private bool enabled;
	[SerializeField, Range(1,100)] private int likelihood;

	public bool Trigger() {
		if (!enabled) return false;
		int rng = Random.Range(1, 101);
		return rng <= likelihood;
	}
}