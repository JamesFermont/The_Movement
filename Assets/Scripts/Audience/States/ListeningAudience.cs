using System.Collections;

public class ListeningAudience : State {
	public ListeningAudience(StateMachine stateMachine) : base(stateMachine) {
	}

	public override IEnumerator Run() {
		while (true)
			yield return null;
	}

	public override void SetNextState() {
	}
}