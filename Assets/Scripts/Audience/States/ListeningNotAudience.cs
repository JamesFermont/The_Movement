using System.Collections;
using UnityEngine;

public class ListeningNotAudience : State {
	public ListeningNotAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		Color c = StateMachine.GetComponent<SpriteRenderer>().color;
		c.a = 0.2f;
		StateMachine.GetComponent<SpriteRenderer>().color = c;
		
		while (true)
			yield return null;
	}

	public override void SetNextState() {
	}
}