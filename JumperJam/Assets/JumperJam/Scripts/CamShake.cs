using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{

	private bool isShaking = false;
	private float baseX,baseY;
	private float intensity = 8f;
	private int shakes = 30;


	void Start()
	{
		baseX = transform.localPosition.x;
	}

	void Update()
	{
		if (isShaking) 
		{
			
			baseY = transform.localPosition.y;
			float randomShakeX = Random.Range (-intensity, intensity);
			float randomShakeY = Random.Range (-intensity, intensity);
			if (this.name == "Mid") 
			{
				transform.localPosition = new Vector3 (baseX + randomShakeX, baseY,-4.31f);
			}
			if (this.name == "Sub") 
			{
				transform.localPosition  = new Vector3 (baseX + randomShakeX, baseY,-4.4f);
			}
			if (this.name == "Side") 
			{
				transform.localPosition  = new Vector3 (baseX + randomShakeX, baseY,-4.5f);
			}
			shakes--;
			if (shakes <= 0)
			{
				isShaking = false;
				if (this.name == "Mid") 
				{
					transform.localPosition = new Vector3 (baseX, baseY,-4.31f);
				}
				if (this.name == "Sub") 
				{
					transform.localPosition  = new Vector3 (baseX, baseY,-4.4f);
				}
				if (this.name == "Side") 
				{
					transform.localPosition  = new Vector3 (baseX, baseY,-4.5f);
				}
			}
		}

	}

	public void MinorShake(float _intensity)
	{
		isShaking = true;
		shakes = 50;
		intensity = _intensity;
	}
}