using UnityEngine;
using Random = UnityEngine.Random;

public class Audience : MonoBehaviour {
    [Header("Debug Information:")] [SerializeField]
    private AudienceMoveConfig moveConfig;

    [SerializeField] private int startingAlignment;
    [SerializeField] private int mood;
    [SerializeField] public int alignment;
    [SerializeField] public bool isConvinced;
    [SerializeField] public bool isPartying;

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
    }

    private void OnDisable() {
        Player.DancingStateChanged -= OnPlayerDancing;
    }

    private void OnPlayerDancing(bool isDancing) {
        if (isDancing) {
            _stateMachine.OverrideState(new BeginListening(_stateMachine));
        }
    }

    public void Convince() {
        isConvinced = true;
        mood = startingAlignment * -1;
    }

    public void UpdateAlignment(int amount) {
        if (isConvinced) amount *= -1;
        alignment += amount;
    }

    public void UpdateMood() {
        mood += alignment;
        if (mood >= InfluenceHandler.GetThresholds()[0].reactionthreshold) {
            isPartying = true;
        }
    }

    public int GetMood() {
        return mood;
    }
}
