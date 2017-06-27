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
	private MeshRenderer meshRen;
	private Transform camera;
	float lastCameraPos = 0;
	Vector2 offset = new Vector2 (0, 0);
	void Start()
	{
		meshRen = GetComponent<MeshRenderer> ();
		if (GameMgr.Instance.randomValue == 1) 
		{
		
			if (this.name == "Mid") 
			{
				meshRen.material = midJung;
			}
		
		
			if (this.name == "Side") 
			{
				meshRen.material = sideJung;
			}
			if (this.name == "Sub") 
			{
				meshRen.material = subJung;
			}
		}

		if (GameMgr.Instance.randomValue == 2) 
		{

			if (this.name == "Mid") 
			{
				meshRen.material = midIce;
			}

			if (this.name == "Side")
			{
				meshRen.material = sideIce;
			}
			if (this.name == "Sub") {
				
				meshRen.material = subIce;
			}
		}
		if (GameMgr.Instance.randomValue == 3) 
		{

			if (this.name == "Mid") 
			{
				meshRen.material = midBean;
			}


			if (this.name == "Side") 
			{
				meshRen.material = sideBean;
			}
			if (this.name == "Sub") 
			{
				meshRen.material = subBean;
			}
		}
		if (GameMgr.Instance.randomValue == 4) 
		{

			if (this.name == "Mid") 
			{
				meshRen.material = midPoison;
			}


			if (this.name == "Side") 
			{
				meshRen.material = sidePoison;
			}
			if (this.name == "Sub") 
			{
				meshRen.material = subPoison;
			}
		}
		if (GameMgr.Instance.randomValue == 5) 
		{

			if (this.name == "Mid") 
			{
				meshRen.material = midTree;
			}


			if (this.name == "Side") 
			{
				meshRen.material = sideTree;
			}
			if (this.name == "Sub") 
			{
				meshRen.material = subTree;
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
