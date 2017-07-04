using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour {


	//this object's ID
	[SerializeField]
	private int _characterID;
	public int characterID
	{
		get { return _characterID;}
		set { 
			_characterID = value;
		}
	}

	//this object's sprites
	public Sprite characterDieSprite;

	public Sprite characterIdleSprite;

	public Sprite characterJumpSprite;

	//this object's price
	[SerializeField]
	 int characterPrice;

	//is this bought?
	public bool isBought;


	// Buy/Select/Selected button
	[SerializeField]
	private Text buttonText;
 

	// Set everything at start
	void Awake()
	{	
		

		//if this object ID == current selected character ID ---> button change to 'selected'
		int ID = ShopManager.Instance.currentCharacterID;
		if (characterID == ID) 
		{
			buttonText.text = "Selected";
					
		} 
		else
		{
			//if this object has been bought but not selected -> button change to 'select'
			if (ShopManager.Instance.CheckIfBoughtID(characterID)==1) 
			{
				isBought = true;
				buttonText.text = "Select";
			}
			else
			{
				// this object hast been bought --> button change to 'Buy'
				buttonText.text = "Buy";
			}
		}
	}



	//Update shop UI when a Buy/Select/Selected is clicked
	void UpdateUI(string updateCode)
	{

		//change this object's button to 'select'
		if (updateCode == "changeToSelect")
			buttonText.text = "Select";

		//change this object's button to 'selected'
		if (updateCode == "changeToSelected") 
		{
			//change the button text
			buttonText.text = "Selected";
		
			//update current selected character's ID
			//Change player sprites
			//update previous selected character's ID
			ShopManager.Instance.currentCharacterID = characterID;
			ShopManager.Instance.ChangeCharacter();
			ShopManager.Instance.previousSelectedID = characterID;

		}
		
	}


	//When press 'Buy' button
	public void OnPressItem()
	{


		int ID = ShopManager.Instance.currentCharacterID;

		// if current char's ID  != this object's ID ----> object nay hoac chua duoc mua, hoac da duoc mua 
		if (characterID != ID) 
		{
			//If Object hasnt been bought ---> if click 'Buy' then
			// --> if have enough money   ---> change button to 'Select'
			// --> if dont have enough money ---> pop up canvas
			if (ShopManager.Instance.CheckIfBoughtID(characterID)==0) 
			{
				if (PlayerPrefs.GetInt ("TotalCoin") >= characterPrice) 
				{
					
				ShopManager.Instance.SetIDtoBoughtID(characterID);


				//If item hasnt been bought then change the text to 'Bought' when tap if player have enough coin
					UpdateUI ("changeToSelect");

				ScoreMgr.Instance.SubCoin (characterPrice);
				} else
					ShopManager.Instance.NotEnoughCoins.SetActive(true);
			} 
			else 
			{
				//If the object has been bought , then change the previous choosen button to "select", and change the new one to "selected"
				if (characterID != ShopManager.Instance.previousSelectedID ) 
				{
					ShopManager.Instance.charList [ShopManager.Instance.previousSelectedID].buttonText.text = "Select";

					//change clicked button text to "selected" 
					//update current character
					//change sprite
					//update previous choosen character
					UpdateUI ("changeToSelected");




				}



			}
		}


	}
}
