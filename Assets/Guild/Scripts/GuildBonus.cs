using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildBonus : MonoBehaviour {

	[SerializeField]
	Text guildName;
	[SerializeField]
	Text level;
	[SerializeField]
	Text membersCount;
	[SerializeField]
	Text guildMembers;

	void OnEnable() {
		guildName.text = PlayerParameters._instance.myPlayerParameter.guildName;
		level.text = PlayerParameters._instance.myPlayerParameter.guildLevel + "";
		membersCount.text = guildMembers.text;
	}

}
