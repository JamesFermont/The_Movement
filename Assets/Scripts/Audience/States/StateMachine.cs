using UnityEngine;

public class StateMachine : MonoBehaviour {
	public AudienceMoveConfig moveSettings;
	private Coroutine _currentState;

	public void Begin() {
		SetState(new Walking(this));
	}
	
	public void SetState(State state) {
		_currentState = StartCoroutine(state.Run());
	}

	public void OverrideState(State state) {
		StopAllCoroutines();
		
		Color c = GetComponent<SpriteRenderer>().color;
		c.a = 1f;
		GetComponent<SpriteRenderer>().color = c;
		
		_currentState = StartCoroutine(state.Run());
	}
}