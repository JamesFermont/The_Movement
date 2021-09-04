using System.Collections;

public abstract class State {
	protected StateMachine StateMachine;

	public State(StateMachine stateMachine) {
		StateMachine = stateMachine;
	}

	public abstract IEnumerator Run();

	public abstract void SetNextState();
}