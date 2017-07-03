using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour {
	PlayerController currentSprite;
	[SerializeField]
	private int _characterID;
	public int characterID
	{
		get { return _characterID;}
		set { 
			_characterID = value;
		}
	}

	public Sprite characterDieSprite;

	public Sprite characterIdleSprite;

	public Sprite characterJumpSprite;
	[SerializeField]
	 int characterPrice;
	public bool isBought;
	//enum
	[SerializeField]
	private Text buttonText;
 

	void Awake()
	{	PlayerPrefs.SetInt ("bought0", 1);
		int ID = ShopManager.Instance.currentCharacterID;
		if (characterID == ID) 
		{
			buttonText.text = "Selected";
//			currentSprite.aniSprites [0] =  characterDieSprite;
//			currentSprite.aniSprites [1] =  characterIdleSprite;
//			currentSprite.aniSprites [2] =  characterJumpSprite;
//			ShopManager.Instance.ChangeCharacter();
//				Debug.Log("id:" + ID);
					
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
			//update current character 
			ShopManager.Instance.currentCharacterID = characterID;
//			PlayerController.Instance.aniSprites [0] =  characterDieSprite;
//			PlayerController.Instance.aniSprites [1] =  characterIdleSprite;
//			PlayerController.Instance.aniSprites [2] =  characterJumpSprite;
			ShopManager.Instance.ChangeCharacter();
			//update previous choosen character


			ShopManager.Instance.previousSelectedID = characterID;

		}
		
	}



	public void OnPressItem()
	{
		int ID = ShopManager.Instance.currentCharacterID;

		if (characterID != ID) 
		{
			if (ShopManager.Instance.CheckIfBoughtID(characterID)==0) 
			{
				if (PlayerPrefs.GetInt ("TotalCoin") >= characterPrice) {
				ShopManager.Instance.SetIDtoBoughtID(characterID);


				//If item hasnt been bought then change the text to 'Bought' when tap if player have enough coin
				UpdateUI (1);

				ScoreMgr.Instance.SubCoin (characterPrice);
				} else
					ShopManager.Instance.NotEnoughCoins.SetActive(true);
			} 
			else 
			{
				//If the item has been bought , then change the previous choosen button to "select", and change the new one to "selected"
				if (characterID != ShopManager.Instance.previousSelectedID ) 
				{
					ShopManager.Instance.charList [ShopManager.Instance.previousSelectedID].buttonText.text = "Select";

					//change clicked button text to "selected" 
					//update current character
					//change sprite
					//update previous choosen character
					UpdateUI (2);




				}



			}
		}


	}
}
