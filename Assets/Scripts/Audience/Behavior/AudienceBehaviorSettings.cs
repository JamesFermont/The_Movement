using UnityEngine;

[CreateAssetMenu(fileName = "AudienceBehavior", menuName = "AudienceConfiguration/AudienceBehavior", order = 0)]
public class AudienceBehaviorSettings : ScriptableObject {
	public float reactionTime;
	public int startingAlignment;
	public int alignmentVelocity;
	[Range(0f, 0.8f)]
	public float outedPercentToConvince;
}