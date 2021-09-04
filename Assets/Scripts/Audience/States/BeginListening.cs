using System;
using System.Collections;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class BeginListening : State {
	public BeginListening(StateMachine stateMachine) : base(stateMachine) {
	}

	public static event Action IsListening;
	private bool _isAudience;

	public override IEnumerator Run() {
		Debug.Assert(Camera.main != null, "Camera.main != null");
		Vector3 posInCamera = Camera.main.WorldToViewportPoint(StateMachine.transform.position);
		_isAudience = posInCamera.x > -0.05f && posInCamera.x < 1.05f;
		
		SetNextState();
		yield break;
	}

	public override void SetNextState() {
		if (!_isAudience) {
			StateMachine.SetState(new ListeningNotAudience(StateMachine));
		}
		else {
			IsListening?.Invoke();
			StateMachine.SetState(new ListeningAudience(StateMachine));
		}
	}
}