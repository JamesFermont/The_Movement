using System.Collections;
using UnityEngine;

public class Partying : State {
	public Partying(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		Vector3 originalPos = StateMachine.transform.position; 
		
		while (MockPlayer._isDancing) {
			StateMachine.transform.position = originalPos + Vector3.up * Mathf.PingPong(Time.time, 0.15f);
			yield return null;
		}

		StateMachine.transform.position = originalPos;
		SetNextState();
	}

	public override void SetNextState() {
		StateMachine.SetState(new Walking(StateMachine));
	}
}