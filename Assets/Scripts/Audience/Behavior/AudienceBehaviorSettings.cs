using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "AudienceBehavior", menuName = "AudienceConfiguration/AudienceBehavior", order = 0), DefaultExecutionOrder(-100)]
public class AudienceBehaviorSettings : ScriptableObject {
	public List<AudienceBehaveConfig> behavePatterns;

	private static List<AudienceBehaveConfig> _configs;

	private void OnEnable() {
		_configs = new List<AudienceBehaveConfig>();
		foreach (AudienceBehaveConfig behaveConfig in behavePatterns) {
			_configs.Add(behaveConfig);
		}
	}

	public static int GetBehaveCount() => _configs.Count;

	public AudienceBehaveConfig GetBehaveConfig() {
		var config = _configs[Random.Range(0, _configs.Count)];
		_configs.Remove(config);
		return config;
	}
}

[Serializable]
public struct AudienceBehaveConfig {
	public float reactionTime;
	public int startingAlignment;
	public int alignmentVelocity;
	[Range(0f, 0.8f)]
	public float outedPercentToConvince;
	public int quantity;
}