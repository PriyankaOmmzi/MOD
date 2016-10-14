using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GuildApplicants : MonoBehaviour {

	[SerializeField]
	Transform guildApplicantsParent;
	[SerializeField]
	Text applicantsCount;
	[SerializeField]
	GameObject guildApplicantPrefab;
	int applicants;

	public void Show (IList applicantsData) {
		ResetData ();
		applicants = applicantsData.Count;
		applicantsCount.text = "" + applicants;
		foreach (IDictionary applicant in applicantsData) {
			RectTransform tempGuildApplicant = Instantiate (guildApplicantPrefab).GetComponent<RectTransform> ();
			tempGuildApplicant.SetParent (guildApplicantsParent);
			tempGuildApplicant.localScale = Vector3.one;
			tempGuildApplicant.GetComponent<GuildApplicant> ().SetData (applicant);
		}
	}

	public void DecrementApplicantsCount() {
		applicantsCount.text = "" + --applicants;
	}

	void ResetData() {
		int temp = 0;
		while (temp < guildApplicantsParent.childCount) {
			Destroy (guildApplicantsParent.GetChild (temp).gameObject);
			temp++;
		}
	}

}
