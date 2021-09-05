using System;
using System.Collections.Generic;
using UnityEngine;

public class InfluenceHandler : MonoBehaviour {
    public Material desaturateShader;
    
    [SerializeField, Range(0.5f, 0.9f)] private float winThreshold;
    [SerializeField] private List<MoodThresholds> reactThresholds;

    [SerializeField] private int crowdReactThresholdPer10Audience;
    [SerializeField] private int activatePoliceThresholdPer10Audience;
    
    private static List<MoodThresholds> _thresholds;
    public static List<MoodThresholds> GetThresholds() => _thresholds;

    public static event Action DispatchPolice;
    public static event Action<Reaction> EmitReaction;
    public static event Action<float> EmitWinPercent;
    public static event Action<float> EmitNotorietyPercent;
    public static event Action PlayerWon;
    
    [SerializeField] private int listenerCount;
    [SerializeField] private int complainCount;
    [SerializeField] private int audienceSize;

    private static int _partyingCount;
    private static int _citizenCount;

    [SerializeField] private int[] currentReactions;

    public static float GetPartyingPercent() {
        return (float)_partyingCount / _citizenCount;
    }

    public static void AddCitizens(int amount) {
        _citizenCount += amount;
    }
    
    private void Awake() {
        listenerCount = 0;
    }

    void Start() {
        _thresholds = reactThresholds;
        desaturateShader.SetFloat("_WinSaturation", GetPartyingPercent() / winThreshold);
    }

    private void OnEnable() {
        Player.DancingStateChanged += HandleDancing;
        BeginListening.IsListening += HandleListener;
        React.EmitReaction += HandleReaction;
        Audience.PartyStatusChanged += SetPartyCount;
        Audience.ComplainingStatusChanged += SetComplainCount;
    }

    private void SetComplainCount(bool isComplaining) {
        if (isComplaining)
            complainCount++;
        else {
            complainCount--;
        }

        float complainPct = complainCount / (_citizenCount / 2f);
        EmitNotorietyPercent?.Invoke(complainPct);
    }

    private void SetPartyCount(bool isPartying) {
        if (isPartying)
            _partyingCount++;
        else {
            _partyingCount--;
        }
        
        float winPct = GetPartyingPercent() / winThreshold;
        desaturateShader.SetFloat("_WinSaturation", winPct);
        EmitWinPercent?.Invoke(winPct);
        if (winPct >= 1f)
            PlayerWon?.Invoke();
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
            EmitReaction?.Invoke(Reaction.Partying);
        }
        else {
            audienceSize = Mathf.CeilToInt(listenerCount / 10f);
            currentReactions[(int) reaction]++;
            for (int i = 1; i <= 4; i++) {
                if (currentReactions[i] > crowdReactThresholdPer10Audience * audienceSize) {
                    EmitReaction?.Invoke(_thresholds[i].reaction);
                    currentReactions[i] = 0;
                }
            }

            if (currentReactions[4] > activatePoliceThresholdPer10Audience * audienceSize * 1.5) {
                DispatchPolice?.Invoke();
                currentReactions[4] = 0;
            }
        }
    }

    private void OnDisable() {
        Player.DancingStateChanged -= HandleDancing;
        BeginListening.IsListening -= HandleListener;
        React.EmitReaction -= HandleReaction;
        Audience.PartyStatusChanged -= SetPartyCount;
        Audience.ComplainingStatusChanged -= SetComplainCount;
    }
}

[Serializable]
public struct MoodThresholds {
    public Reaction reaction;
    public int reactionthreshold;
}