using System.Collections;
using UnityEngine;

public class ListeningAudience : State {
	public ListeningAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		float timeElapsed = 0f;

		while (timeElapsed < StateMachine.behaviorSettings.reactionTime && Player.dancing) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		SetNextState();
	}

	public override void SetNextState() {
		if (Player.dancing)
			StateMachine.SetState(new React(StateMachine));
		else {
			StateMachine.SetState(new Walking(StateMachine));
		}
	}
}