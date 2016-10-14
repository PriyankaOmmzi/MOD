using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour {

	[SerializeField]
	Text message;
	[SerializeField]
	Image avatar;
	[SerializeField]
	Sprite[] avatars;
	[SerializeField]
	RectTransform background;
	[SerializeField]
	LayoutElement layoutElement;

	public void Set(string message, string playerName, int avatarNumber) {
		this.message.text = playerName + ":\n" + message;
		avatar.sprite = avatars [avatarNumber - 1];
		Invoke ("Show", 0.1f);
	}

	void Show() {
		Vector2 temp = background.sizeDelta;
		temp.y = message.rectTransform.rect.height;
		background.sizeDelta = temp;
		layoutElement.minHeight = message.rectTransform.rect.height;
	}


}