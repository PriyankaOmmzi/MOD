using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {

//	public List <string> allCardsInGame;
	public List <int> baseAttack;
	public List <int> baseDefense;
	public List <int> baseLeadership;
	public List <ChapterAI> encounterAmbushAI;
	public List <BossChapterAI> bossAI;

	public static EnemyAI _instance;
	// Use this for initialization
	void Start () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

[System.Serializable]
public class BossAI
{
	public int bossStats;
	public int bossHealth; // leadership!
}


[System.Serializable]
public class AI
{
	public int []cardOfRarity;
}

[System.Serializable]
public class ChapterAI
{
	public List <AI> areaAI;
}

[System.Serializable]
public class BossAreaAI
{
	public List <BossAI> areaAI;
}

[System.Serializable]
public class BossChapterAI
{
	public List <BossAreaAI> chapterAI;
}
