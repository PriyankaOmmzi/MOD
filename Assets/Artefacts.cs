using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ArtefactType
{
	public string name;
	public string descriptiion;
	public Sprite itemImage;
}
[System.Serializable]
public class ChestArtefactType
{
	public ArtefactType artefactType;
	public int eventPointsFetcehd;

}
public class Artefacts : MonoBehaviour {

	public ArtefactType[] gameArtifacts;
	public ChestArtefactType[] chestEventArtefact;
	public static Artefacts _instance;
	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
