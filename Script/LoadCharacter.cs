using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadCharacter : MonoBehaviour
{
	public GameObject[] characterPrefabs;
	public Transform spawnPoint;
	//public TMP_Text label;

	void Start()
	{
		int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
		GameObject Prefab = characterPrefabs[selectedCharacter];
		GameObject clone = Instantiate(Prefab, spawnPoint.position, Quaternion.identity);
	//	label.text = Prefab.name;
	}

}
