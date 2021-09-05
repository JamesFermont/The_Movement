using System.Collections;
using UnityEngine;

public class ListeningNotAudience : State {
	public ListeningNotAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {

		
		while (Player.dancing)
			yield return null;

		
		SetNextState();
	}

	public override void SetNextState() {
		StateMachine.SetState(new Walking(StateMachine));
	}
}