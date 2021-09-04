using System;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceHandler : MonoBehaviour {
    [SerializeField, Range(0.5f, 0.9f)] private float winThreshold;
    [SerializeField] private List<MoodThresholds> reactThresholds;

    [SerializeField] private int crowdReactThresholdPer10Audience;
    [SerializeField] private int activatePoliceThresholdPer10Audience;
    
    private static List<MoodThresholds> _thresholds;
    public static List<MoodThresholds> GetThresholds() => _thresholds;

    public static event Action PlayerWon;
    
    [SerializeField] private int listenerCount;
    [SerializeField] private int partyingCount;
    [SerializeField] private int reactCount;

    private static int _partyingCount;
    private static int citizenCount;

    [SerializeField] private int[] currentReactions;

    public static float GetPartyingPercent() {
        return (float)_partyingCount / citizenCount;
    }

    public static void AddCitizens(int amount) {
        citizenCount += amount;
    }
    
    private void Awake() {
        listenerCount = 0;
    }

    void Start() {
        _thresholds = reactThresholds;
    }

    private void OnEnable() {
        MockPlayer.DancingStateChanged += HandleDancing;
        BeginListening.IsListening += HandleListener;
        React.EmitReaction += HandleReaction;
    }

    private void HandleListener() {
        listenerCount++;
    }

    private void HandleDancing(bool isDancing) {
        if (isDancing) {
            listenerCount = 0;
            currentReactions = new int[_thresholds.Count];
        }
    }

    private void HandleReaction(Reaction reaction) {
        if (reaction == Reaction.Partying) {
            Debug.Log("Partying! Partying Percent now: " + GetPartyingPercent());
            partyingCount++;
            _partyingCount++;
            if ((float)partyingCount/citizenCount >= winThreshold)
                PlayerWon?.Invoke();
        }
        else {
            reactCount = Mathf.CeilToInt(listenerCount / 10f);
            currentReactions[(int) reaction]++;
            for (int i = 1; i <= 3; i++) {
                if (currentReactions[i] > crowdReactThresholdPer10Audience * reactCount) {
                    Debug.Log("Emitting Reaction: " +  reaction);
                    currentReactions[i] = 0;
                }
            }

            if (currentReactions[4] > activatePoliceThresholdPer10Audience * reactCount) {
                Debug.Log("Emit Event: Call the cops!");
            }
        }
    }

    private void OnDisable() {
        React.EmitReaction -= HandleReaction;
    }
}

[Serializable]
public struct MoodThresholds {
    public Reaction reaction;
    public int reactionthreshold;
}