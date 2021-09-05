using System.Collections;
using UnityEngine;

public class ListeningNotAudience : State {
	public ListeningNotAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		Color c = StateMachine.GetComponent<SpriteRenderer>().color;
		c.a = 0.2f;
		StateMachine.GetComponent<SpriteRenderer>().color = c;
		
		while (Player.dancing)
			yield return null;
		
		c.a = 1f;
		StateMachine.GetComponent<SpriteRenderer>().color = c;
		
		SetNextState();
	}

	public override void SetNextState() {
		StateMachine.SetState(new Walking(StateMachine));
	}
}