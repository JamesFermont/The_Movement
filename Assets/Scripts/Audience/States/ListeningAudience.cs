using System.Collections;
using UnityEngine;

public class ListeningAudience : State {
	public ListeningAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		float timeElapsed = 0f;

		while (timeElapsed < StateMachine.behaviorSettings.reactionTime && MockPlayer._isDancing) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		SetNextState();
	}

	public override void SetNextState() {
		if (MockPlayer._isDancing)
			StateMachine.SetState(new React(StateMachine));
		else {
			StateMachine.SetState(new Walking(StateMachine));
		}
	}
}