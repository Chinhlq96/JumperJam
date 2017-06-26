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
		camera = Camera.main.transform;
	}

	void Update () {
		
		float shift = camera.position.y - lastCameraPos;
		lastCameraPos = camera.position.y;
		offset.y += shift*0.05f;
		GetComponent<Renderer> ().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}
