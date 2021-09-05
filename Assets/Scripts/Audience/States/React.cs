using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class React : State {
	public React(StateMachine stateMachine) : base(stateMachine) {
	}

	public static event Action<Reaction> EmitReaction; 

	public override IEnumerator Run() {
		if (StateMachine.Partying) {
			SetNextState();
			yield break;
		}

		float convincedPct = InfluenceHandler.GetPartyingPercent();

		if (StateMachine.behaviorSettings.alignmentVelocity < 0) {
			if (convincedPct > StateMachine.behaviorSettings.outedPercentToConvince) {
				StateMachine.member.Convince();
			}
		}

		StateMachine.member.UpdateAlignment(StateMachine.behaviorSettings.alignmentVelocity);
		StateMachine.member.UpdateMood();

		int mood = StateMachine.member.GetMood();
		List<MoodThresholds> thresholds = InfluenceHandler.GetThresholds();

		if (mood >= thresholds[0].reactionthreshold) {
			EmitReaction?.Invoke(Reaction.Partying);
		} else if (mood <= thresholds[0].reactionthreshold && mood > thresholds[1].reactionthreshold) {
			EmitReaction?.Invoke(Reaction.Enjoyment);
		} else if (mood <= thresholds[1].reactionthreshold && mood > thresholds[3].reactionthreshold) {
			EmitReaction?.Invoke(Reaction.Wondering);
		} else if (mood <= thresholds[3].reactionthreshold && mood > thresholds[4].reactionthreshold) {
			EmitReaction?.Invoke(Reaction.Annoyed);
		}
		else {
			EmitReaction?.Invoke(Reaction.Complaining);
		}
		
		SetNextState();
	}

	public override void SetNextState() {
		if (StateMachine.Partying)
			StateMachine.SetState(new Partying(StateMachine));
		else
			StateMachine.SetState(new ListeningAudience(StateMachine));
	}
}

public enum Reaction {
	Partying = 0,
	Enjoyment = 1,
	Wondering = 2,
	Annoyed = 3,
	Complaining = 4
}