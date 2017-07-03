using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager: SingletonMonoBehaviour<ShopManager> {




	public List<Character> charList= new List<Character>();


	public GameObject NotEnoughCoins;


	void Start()
	{
		ChangeCharacter ();
		PlayerPrefs.SetInt ("bought0", 1);
	}
	public void ChangeCharacter() {
		for (int i = 0; i < charList.Count; i++) 
		{
			if (charList [i].characterID == currentCharacterID) 
			{
				PlayerController.Instance.aniSprites [0] = charList [i].characterDieSprite;
				PlayerController.Instance.aniSprites [1] = charList [i].characterIdleSprite;
				PlayerController.Instance.aniSprites [2] = charList [i].characterJumpSprite;
				Debug.Log("id:" + currentCharacterID);

			}
		}
	}
	//  9 item  => 0 to 8 
	// item that is currently selected
	public int currentCharacterID
	{
		get {return PlayerPrefs.GetInt (CURRENt_CHARACTER_ID_KEY, 0);}
		set { PlayerPrefs.SetInt (CURRENt_CHARACTER_ID_KEY, value);} 
	}

	//check if item has been bought , default value is 0 stand for not been bought
	public int CheckIfBoughtID(int ID)
	{
		return PlayerPrefs.GetInt ("bought" + ID, 0);
	}

	// set item as has been bought with value 1
	public void SetIDtoBoughtID(int ID)
	{
		PlayerPrefs.SetInt ("bought" + ID, 1);
	}

	void Update()
	{

	//	PlayerPrefs.DeleteAll();
	}

	//Save previous selected item ( to change from 'selected' to 'select' when choose other character)
	public int previousSelectedID
	{
		get {return PlayerPrefs.GetInt ("previousSelectedID", 0);}
		set { PlayerPrefs.SetInt ("previousSelectedID", value);} 
	}


	const string CURRENt_CHARACTER_ID_KEY = "CurrentCharacterID";





}
