using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Audience : MonoBehaviour {
    [SerializeField] private Material desaturateMaterial;
    [SerializeField] private Material standardMaterial;

    [Header("Debug Information:")] [SerializeField]
    private AudienceMoveConfig moveConfig;

    [SerializeField] private int startingAlignment;
    [SerializeField] private int mood;
    [SerializeField] public int alignment;
    [SerializeField] public bool isConvinced;

    private bool _isPartying;
    private bool _isComplaining;

    public static event Action<bool> PartyStatusChanged;
    public static event Action<bool> ComplainingStatusChanged;

    public bool Partying {
        get => _isPartying;
        set {
            _isPartying = value;
            PartyStatusChanged?.Invoke(_isPartying);
        }
    }

    public bool Complaining {
        get => _isComplaining;
        set {
            _isComplaining = value;
            ComplainingStatusChanged?.Invoke(_isComplaining);
        }
    }

    private AudienceBehaveConfig _behaviorSettings;
    private StateMachine _stateMachine;

    public void SetSettings(AudienceBehaveConfig settings) {
        _behaviorSettings = settings;
    }

    public void SetMoveConfig(AudienceMoveConfig config) {
        moveConfig = config;
    }
    
    private void Awake() {
        _stateMachine = GetComponent<StateMachine>();
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = Random.Range(1, 6);
        gameObject.GetComponent<SpriteRenderer>().sharedMaterial = desaturateMaterial;
    }

    private void Start() {
        mood = 0;
        startingAlignment = alignment = _behaviorSettings.startingAlignment;
        
        _stateMachine.moveSettings = moveConfig;
        _stateMachine.behaviorSettings = _behaviorSettings;
        _stateMachine.member = this;
        _stateMachine.Begin();

        Vector3 audienceScale = transform.localScale;
        audienceScale.x = _stateMachine.moveSettings.direction * -0.75f;
        transform.localScale = audienceScale;
    }

    private void OnEnable() {
        Player.DancingStateChanged += OnPlayerDancing;
        GameSystem.HasReset += OnReset;
    }

    private void OnReset() {
        mood = 0;
        startingAlignment = alignment = _behaviorSettings.startingAlignment;
        isConvinced = false;

        if (_isPartying)
            Partying = false;
        if (_isComplaining)
            Complaining = false;
        
        _stateMachine.moveSettings = moveConfig;
        _stateMachine.behaviorSettings = _behaviorSettings;
        _stateMachine.member = this;
        _stateMachine.Begin();
    }

    private void OnDisable() {
        Player.DancingStateChanged -= OnPlayerDancing;
        GameSystem.HasReset -= OnReset;
    }

    private void OnPlayerDancing(bool isDancing) {
        if (isDancing) {
            _stateMachine.OverrideState(new BeginListening(_stateMachine));
        }
    }

    public void Convince() {
        isConvinced = true;
        if (_isComplaining)
            Complaining = false;
        mood = startingAlignment * -1;
        alignment = startingAlignment * -1;
    }

    public void UpdateAlignment(int amount) {
        if (isConvinced) amount *= -1;
        alignment += amount;
    }

    public void UpdateMood() {
        mood += alignment;
        if (Partying || Complaining) return;

        if (mood >= InfluenceHandler.GetThresholds()[0].reactionthreshold) {
            Partying = true;
            GetComponent<SpriteRenderer>().sharedMaterial = standardMaterial;
        } else if (mood <= InfluenceHandler.GetThresholds()[4].reactionthreshold) {
           Complaining = true;
        }
    }


    public int GetMood() {
        return mood;
    }
}
