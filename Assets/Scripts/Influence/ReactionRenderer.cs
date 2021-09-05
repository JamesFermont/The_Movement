using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[DefaultExecutionOrder(-10)]
public class ReactionRenderer : MonoBehaviour {
    [SerializeField] private float fadeInTime;
    [SerializeField] private float playTime;
    [SerializeField] private float fadeOutTime;
    
    [SerializeField] private PlaySlot[] _slots;
    [SerializeField] private ReactionSprites[] _sprites;

    private AudioManager audioManager;

    private List<PlaySlot> _availableSlots;
    private Queue<Reaction> _reactionsToPlay;
    
    private void Start() {
        _availableSlots = new List<PlaySlot>();
        _reactionsToPlay = new Queue<Reaction>();

        foreach (PlaySlot slot in _slots) {
            slot.initialTransform = slot.renderer.gameObject.transform.localPosition;
            slot.animationEnded += AddSlotToList;
            _availableSlots.Add(slot);
        }

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void OnEnable() {
        InfluenceHandler.EmitReaction += HandleReaction;
        _availableSlots = new List<PlaySlot>();
        _reactionsToPlay = new Queue<Reaction>();
        foreach (PlaySlot slot in _slots) {
            _availableSlots.Add(slot);
        }
    }

    private void OnDisable() {
        InfluenceHandler.EmitReaction -= HandleReaction;
        foreach (PlaySlot slot in _slots) {
            slot.renderer.sprite = null;
        }
    }

    private void OnDestroy() {
        foreach (PlaySlot slot in _slots) {
            slot.animationEnded -= AddSlotToList;
        }
    }

    private void HandleReaction(Reaction reaction) {
        if (_availableSlots.Count == 0) {
            if (reaction == Reaction.Complaining) {
                int index = Random.Range(0, _slots.Length);
                StopCoroutine(_slots[index].Coroutine);
                PlayReaction(reaction, _slots[index]);
            }
            else {
                _reactionsToPlay.Enqueue(reaction);
            }
        }
        else {
            PlayReaction(reaction, _availableSlots[Random.Range(0,_availableSlots.Count)]);
        }
    }

    private void AddSlotToList(PlaySlot slot) {
        _availableSlots.Add(slot);
    }

    public void PlayReaction(Reaction reaction, PlaySlot slot) {
        slot.Coroutine = StartCoroutine(FadeIn(reaction, slot));
        _availableSlots.Remove(slot);

        int index = Random.Range(1, 3);
        audioManager.Play(reaction + "_" + index.ToString());
    }

    private IEnumerator FadeIn(Reaction reaction, PlaySlot slot) {
        float timeElapsed = 0f;
        slot.IsPlaying = true;
        Color color = new Color(1, 1, 1, 0);
        slot.renderer.sprite = _sprites[(int) reaction].sprite;
        slot.renderer.color = color;

        while (timeElapsed < fadeInTime) {
            color.a = timeElapsed / fadeInTime;
            slot.renderer.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        slot.Coroutine = StartCoroutine(Play(reaction, slot));
    }
    
    private IEnumerator Play(Reaction reaction, PlaySlot slot) {
        float timeElapsed = 0f;

        while (timeElapsed < playTime) {
            int secondsElapsed = (int) (timeElapsed * 1.2);

            slot.renderer.transform.localPosition =
                slot.initialTransform + Vector3.up * 0.05f * (secondsElapsed % 2 == 0 ? -1 : 1);
            
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        slot.Coroutine =  StartCoroutine(FadeOut(reaction, slot));
    }
    
    private IEnumerator FadeOut(Reaction reaction, PlaySlot slot) {
        float timeElapsed = 0f;
        slot.renderer.sprite = _sprites[(int) reaction].sprite;
        Color color = new Color(1, 1, 1, 1);
        slot.renderer.color = color;
        
        while (timeElapsed < fadeOutTime) {
            color.a = 1 - timeElapsed / fadeInTime;
            slot.renderer.color = color;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        slot.IsPlaying = false;
        slot.renderer.transform.localPosition = slot.initialTransform;
    }
}

[System.Serializable]
public class PlaySlot {
    public SpriteRenderer renderer;
    public Coroutine Coroutine;
    private bool _isPlaying;
    
    public bool IsPlaying {
        get => _isPlaying;
        set {
            _isPlaying = value;
            if (value == false) {
                animationEnded?.Invoke(this);
            }
        }
    }

    public Vector3 initialTransform;

    public event Action<PlaySlot> animationEnded;
}

[System.Serializable]
public class ReactionSprites {
    public Reaction reaction;
    public Sprite sprite;
}
