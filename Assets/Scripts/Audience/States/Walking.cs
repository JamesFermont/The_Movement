using System.Collections;
using UnityEngine;

public class Walking : State {
	public Walking(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		float timeElapsed = 0f;
		
		Vector3 direction = Vector3.right * StateMachine.moveSettings.movementSpeed
		                                                  * StateMachine.moveSettings.direction;

		while (timeElapsed < StateMachine.moveSettings.movementDuration) {
			StateMachine.transform.position += direction * Time.deltaTime;
			timeElapsed += Time.deltaTime;
			yield return null;
		}
		
		SetNextState();
	}

	public override void SetNextState() {
		if (StateMachine.moveSettings.canIdle.Trigger()) {
			if (StateMachine.moveSettings.canChangeDirection.Trigger())
				StateMachine.moveSettings.direction *= -1;
			StateMachine.SetState(new Idle(StateMachine));
		}
		else {
			StateMachine.SetState(new Walking(StateMachine));
		}
	}
}