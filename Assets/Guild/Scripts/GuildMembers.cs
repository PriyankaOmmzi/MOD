using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildMembers : MonoBehaviour {

	[SerializeField]
	Transform guildMembersParent;
	[SerializeField]
	Text membersCount;
	[SerializeField]
	GameObject guildMemberPrefab;
	[SerializeField]
	GameObject invitePlayerUI;
	[SerializeField]
	GameObject playerProfile;
	[SerializeField]
	Button invitePlayerButton;
	int members;
	string maxMembers;

	void OnEnable() {
		invitePlayerUI.SetActive (false);
		playerProfile.SetActive (false);
	}

	public void Show (IList membersData, string maxMembers, bool canInvite) {
		invitePlayerButton.interactable = canInvite;
		ResetData ();
		members = membersData.Count;
		this.maxMembers = maxMembers;
		membersCount.text = members  + "/" + maxMembers;
		foreach (IDictionary member in membersData) {
			RectTransform tempMember = Instantiate (guildMemberPrefab).GetComponent<RectTransform> ();
			tempMember.SetParent (guildMembersParent);
			tempMember.localScale = Vector3.one;
//			if (member ["guild_role"].ToString () == "Guild Leader") {
//				tempMember.GetComponent<GuildMember> ().changeRoleButton.interactable = false;
//			} else {
//				tempMember.GetComponent<GuildMember> ().changeRoleButton.interactable = canInvite;
//			}
			tempMember.GetComponent<GuildMember> ().SetData (member);
		}
	}

	public void SetMembers() {
		membersCount.text = --members  + "/" + maxMembers;
	}

	void ResetData() {
		int temp = 0;
		while (temp < guildMembersParent.childCount) {
			Destroy (guildMembersParent.GetChild (temp).gameObject);
			temp++;
		}
	}
}
