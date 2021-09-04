using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Audience : MonoBehaviour {
    [Header("Configurations:")]
    [SerializeField] private AudienceBehaviorSettings audienceSettings;

    [Header("Debug Information:")] [SerializeField]
    private AudienceMoveConfig moveConfig;
    
    private StateMachine _stateMachine;

    private void Awake() {
        _stateMachine = GetComponent<StateMachine>();
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(1, 6);
        moveConfig = AudienceMoveSettings.GetRandomMoveConfig();
    }

    private void Start() {
        _stateMachine.moveSettings = moveConfig;
        _stateMachine.Begin();
    }

    private void OnEnable() {
        MockPlayer.DancingStateChanged += OnPlayerDancing;
    }

    private void OnDisable() {
        MockPlayer.DancingStateChanged -= OnPlayerDancing;
    }

    private void OnPlayerDancing(bool isDancing) {
        if (isDancing) {
            _stateMachine.OverrideState(new BeginListening(_stateMachine));
        }
        else {
            _stateMachine.OverrideState(new Walking(_stateMachine));
        }
    }
}
