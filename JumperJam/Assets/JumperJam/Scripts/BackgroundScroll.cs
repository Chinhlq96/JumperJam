using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
	public Material midIce;
	public Material sideIce;
	public Material subIce;

	public Material midJung;
	public Material sideJung;
	public Material subJung;

	public Material midPoison;
	public Material sidePoison;
	public Material subPoison;

	public Material midBean;
	public Material sideBean;
	public Material subBean;

	public Material midTree;
	public Material sideTree;
	public Material subTree;
	private Transform camera;
	float lastCameraPos = 0;
	Vector2 offset = new Vector2 (0, 0);
	void Start()
	{
		if (GameMgr.Instance.randomValue == 1) 
		{
		
			if (this.name == "Mid") 
			{
				GetComponent<MeshRenderer> ().material = midJung;
			}
		
		
			if (this.name == "Side") 
			{
				GetComponent<MeshRenderer> ().material = sideJung;
			}
			if (this.name == "Sub") 
			{
				GetComponent<MeshRenderer> ().material = subJung;
			}
		}

		if (GameMgr.Instance.randomValue == 2) 
		{

			if (this.name == "Mid") 
			{
				GetComponent<MeshRenderer> ().material = midIce;
			}

			if (this.name == "Side")
			{
				GetComponent<MeshRenderer> ().material = sideIce;
			}
			if (this.name == "Sub") {
				
				GetComponent<MeshRenderer> ().material = subIce;
			}
		}
		if (GameMgr.Instance.randomValue == 3) 
		{

			if (this.name == "Mid") 
			{
				GetComponent<MeshRenderer> ().material = midBean;
			}


			if (this.name == "Side") 
			{
				GetComponent<MeshRenderer> ().material = sideBean;
			}
			if (this.name == "Sub") 
			{
				GetComponent<MeshRenderer> ().material = subBean;
			}
		}
		if (GameMgr.Instance.randomValue == 4) 
		{

			if (this.name == "Mid") 
			{
				GetComponent<MeshRenderer> ().material = midPoison;
			}


			if (this.name == "Side") 
			{
				GetComponent<MeshRenderer> ().material = sidePoison;
			}
			if (this.name == "Sub") 
			{
				GetComponent<MeshRenderer> ().material = subPoison;
			}
		}
		if (GameMgr.Instance.randomValue == 5) 
		{

			if (this.name == "Mid") 
			{
				GetComponent<MeshRenderer> ().material = midTree;
			}


			if (this.name == "Side") 
			{
				GetComponent<MeshRenderer> ().material = sideTree;
			}
			if (this.name == "Sub") 
			{
				GetComponent<MeshRenderer> ().material = subTree;
			}
		}
		camera = Camera.main.transform;
	}

	void Update () {
		//bullshittttttttttttttttttttttttttttttttttttttttttttttttttt
//		if (ScoreMgr.Instance.getScore() == 300) {
//			if (GameMgr.Instance.randomValue == 2) {
//				if (this.name == "Mid") {
//					GetComponent<MeshRenderer> ().material = midJung;
//				}
//
//
//				if (this.name == "Side") {
//					GetComponent<MeshRenderer> ().material = sideJung;
//				}
//				if (this.name == "Sub") {
//					GetComponent<MeshRenderer> ().material = subJung;
//				}
//			}
//			if (GameMgr.Instance.randomValue == 1)
//			{
//
//				if (this.name == "Mid") 
//				{
//					GetComponent<MeshRenderer> ().material = midIce;
//				}
//
//				if (this.name == "Side")
//				{
//					GetComponent<MeshRenderer> ().material = sideIce;
//				}
//				if (this.name == "Sub") {
//
//					GetComponent<MeshRenderer> ().material = subIce;
//				}
//			}
//		}
		//bullshiitttttttttttttttttttttttttttttttttttttttttttttttttt
		float shift = camera.position.y - lastCameraPos;
		lastCameraPos = camera.position.y;
		offset.y += shift*0.05f;
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
