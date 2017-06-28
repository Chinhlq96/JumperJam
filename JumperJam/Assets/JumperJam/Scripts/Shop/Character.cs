using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour {
	PlayerController currentSprite;
	[SerializeField]
	 int characterID;
	[SerializeField]
	 Sprite characterDieSprite;
	[SerializeField]
	 Sprite characterIdleSprite;
	[SerializeField]
	 Sprite characterJumpSprite;
	[SerializeField]
	 int characterPrice;
	public bool isBought;
	//enum
	[SerializeField]
	private Text buttonText;
 


	void OnEnable()
	{	
		currentSprite = ShopManager.Instance.player.GetComponent<PlayerController> ();
		int ID = ShopManager.Instance.currentCharacterID;
		if (characterID == ID) 
		{
			buttonText.text = "Selected";
			currentSprite.aniSprites [0] = Resources.Load ("Die" + (ID + 1), typeof(Sprite)) as Sprite;
			currentSprite.aniSprites [1] = Resources.Load ("Idle" + (ID + 1) , typeof(Sprite)) as Sprite;
			currentSprite.aniSprites [2] = Resources.Load ("Jump" + (ID + 1), typeof(Sprite)) as Sprite;

				Debug.Log(ID);
					
		} 
		else
		{
			if (ShopManager.Instance.CheckIfBoughtID(characterID)==1) 
			{
				isBought = true;
				buttonText.text = "Select";
			}
			else
			{
				buttonText.text = "Buy";
			}
		}
	}

	void UpdateUI(int updateCode)
	{
		if (updateCode == 1)
			buttonText.text = "Select";
		if (updateCode == 2) 
		{

			buttonText.text = "Selected";
			ShopManager.Instance.currentCharacterID = characterID;
			currentSprite.aniSprites [0] = Resources.Load ("Die" + (characterID + 1), typeof(Sprite)) as Sprite;
			currentSprite.aniSprites [1] = Resources.Load ("Idle" + (characterID + 1), typeof(Sprite)) as Sprite;
			currentSprite.aniSprites [2] = Resources.Load ("Jump" + (characterID + 1), typeof(Sprite)) as Sprite;
			Debug.Log (characterID);
		}
		
	}



	public void OnPressItem()
	{
		int ID = ShopManager.Instance.currentCharacterID;

		if (characterID != ID) 
		{
			if (ShopManager.Instance.CheckIfBoughtID(characterID)==0) 
			{
				//if (PlayerPrefs.GetInt ("TotalCoin") >= characterPrice) {
				ShopManager.Instance.SetIDtoBoughtID(characterID);


				//If item hasnt been bought then change the text to 'Bought' when tap if player have enough coin
				UpdateUI (1);

				ScoreMgr.Instance.SubCoin (characterPrice);
				//} else
				//	Debug.Log ("khong du tien");
			} 
			else 
			{
				if (characterID != ShopManager.Instance.previousSelectedID ) 
				{
					ShopManager.Instance.charList [ShopManager.Instance.previousSelectedID].buttonText.text = "Select";
					UpdateUI (2);
					ShopManager.Instance.previousSelectedID = characterID;

				}



			}
		}


	}
}
