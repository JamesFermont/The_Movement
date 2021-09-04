using System.Collections;
using UnityEngine;

public class Idle : State {
	public Idle(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		float timeElapsed = 0;

		while (timeElapsed < StateMachine.moveSettings.idleTime) {
			timeElapsed += Time.deltaTime;
			yield return null;
		}

		SetNextState();
	}

	public override void SetNextState() {
		StateMachine.SetState(new Walking(StateMachine));
	}
}