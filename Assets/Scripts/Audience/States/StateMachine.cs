using UnityEngine;

public class StateMachine : MonoBehaviour {
	public AudienceMoveConfig moveSettings;
	public AudienceBehaveConfig behaviorSettings;
	public Audience member;
	public bool IsPartying => member.isPartying;

	public void Begin() {
		SetState(new Walking(this));
	}
	
	public void SetState(State state) {
		StartCoroutine(state.Run());
	}

	public void OverrideState(State state) {
		StopAllCoroutines();
		StartCoroutine(state.Run());
	}
}