using System;
using System.Collections;
using System.Collections.Generic;

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

		bool hasReacted = false;
		
		for (int i = 0; i < thresholds.Count; i++) {
			if (mood >= thresholds[i].reactionthreshold) {
				EmitReaction?.Invoke(thresholds[i].reaction);
				hasReacted = true;
				break;
			}
		}

		if (!hasReacted) {
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