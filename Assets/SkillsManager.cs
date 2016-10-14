using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skills{
	public string skillName;
	public SkillEffectingParameter parameterThatSkillEffects;
	public SkillRange skillRange;

	[System.Serializable]
	public enum SkillRange{
		CHARACTER,
		ROW,
		SPECIFIC_ARMY,
		ARMY,
		CHEST_EVENT,
		RAID,
		BATTLE_ROYAL,
		CONQUEROR,
		PUZZLE,
		FLOOR_CLEARING,
		CASTLE_DEFEND,
		PET_BREEDING,
		AVATAR_SKLLS
	};
	[System.Serializable]
	public enum SkillEffectingParameter{
		ATTACK,
		DEFEND,
		LEADERSHIP,
		ATTACK_DEFEND,
		HEAL,
		WEAK_RIVAL_ATTACK,
		WEAK_RIVAL_DEFENSE,
		BOOST_KEY_DROP,
		BOOST_POINT,
		BOOST_ATTACK,
		ENCOUNTER_SPECIAL_BOSS,
		BOOST_HEADCOUNT_LOOT,
		BOOST_SOLDIER_SURRENDER_RATE,
		BOOST_LAND_ACQUIRE,
		BOOST_JIGSAW_POINTS,
		INCREASE_SCOUT_PERCENTAGE,
		BOOST_CLEARANCE,
		BOOST_POINT_PER_ATTACK,
		INCREASE_CASTLE_RECOVERY_RATE,
		BOOST_POINT_PER_FEED,
		BOOST_FOOD_QUALITY,
		STUN,
		POISON,
		FIREBALL,
		THUNDERSTRIKE,
		BLIZZARD,
		LANDSLIDE,
		RIVAL_TAKE_DAMAGE_WITH_ATTACK,
		CREATE_BARRIER,
		AMPLIFY_DAMAGE,
		NEAUTRALISE_SKILL,
		FOCUS,
		GOLD_PRODUCTION,
		FOOD_PRODUCTION,
		BOOST_TOSSER_DAMAGE,
		BOOST_SHOOTER_DAMAGE,
		BOOST_SLICER_DAMAGE
	};
}

public class SkillsManager : MonoBehaviour {
	public Skills []skills;
	public static SkillsManager _instance;
	// Use this for initialization
	void Start () {
		_instance = this;
//		string abc = null;
//		if (abc == null)
//			Debug.Log ("itis null");
//		if (abc == "") {
//			Debug.Log ("it is empty");
//		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float PercentageEffectOfSkill(string skillStrength , int skillLevel)
	{
		float percentageVal = 0;
		switch (skillStrength) {
		case "Weak":
			percentageVal = (0.5f*skillLevel);
			break;
		case "Moderate":
			percentageVal = (1f*skillLevel);
			break;
		case "Strong":
			percentageVal = (1.5f*skillLevel);
			break;
		case "Ultra":
			percentageVal = (2f*skillLevel);
			break;
		}
		return percentageVal;

	}

	public void Skill1OfCard(int cardIdInPlayerList ,bool isPlayer, ref Skills.SkillRange skillRange, ref Skills.SkillEffectingParameter param , ref string skillStrength ,ref int skillLevel)
	{
		for(int i = 0 ; i < skills.Length ; i++)
		{
			if(isPlayer)
			{
				if(skills[i].skillName == CardsManager._instance.mycards[cardIdInPlayerList].skill_1)
				{
					skillRange = skills[i].skillRange;
					param = skills[i].parameterThatSkillEffects;
					skillStrength = CardsManager._instance.mycards[cardIdInPlayerList].skill_1_Strength;
					skillLevel = CardsManager._instance.mycards[cardIdInPlayerList].skill1_level;
					break;
				}
			}
			else
			{
				if(skills[i].skillName == OpponentData._instance.enemyCards[cardIdInPlayerList].skill_1)
				{
					skillRange = skills[i].skillRange;
					param = skills[i].parameterThatSkillEffects;
					skillStrength = OpponentData._instance.enemyCards[cardIdInPlayerList].skill_1_Strength;
					skillLevel = OpponentData._instance.enemyCards[cardIdInPlayerList].skill1_level;
					break;
				}
			}
		}

	}

	public void Skill2OfCard(int cardIdInPlayerList ,bool isPlayer, ref Skills.SkillRange skillRange, ref Skills.SkillEffectingParameter param , ref string skillStrength ,ref int skillLevel)
	{
		for(int i = 0 ; i < skills.Length ; i++)
		{
			if(isPlayer)
			{
				if(skills[i].skillName == CardsManager._instance.mycards[cardIdInPlayerList].skill_2)
				{
					skillRange = skills[i].skillRange;
					param = skills[i].parameterThatSkillEffects;
					skillStrength = CardsManager._instance.mycards[cardIdInPlayerList].skill_2_Strength;
					skillLevel = CardsManager._instance.mycards[cardIdInPlayerList].skill2_level;
					break;
				}
			}
			else
			{
				if(skills[i].skillName == OpponentData._instance.enemyCards[cardIdInPlayerList].skill_2)
				{
					skillRange = skills[i].skillRange;
					param = skills[i].parameterThatSkillEffects;
					skillStrength = OpponentData._instance.enemyCards[cardIdInPlayerList].skill_2_Strength;
					skillLevel = OpponentData._instance.enemyCards[cardIdInPlayerList].skill2_level;
					break;
				}
			}
		}
		
	}

}
