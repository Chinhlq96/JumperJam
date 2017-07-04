using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour
{
	// is not shaking at start
	private bool isShaking = false;


	//original camera's x and y position
	private float baseX,baseY;

	// Do manh. cua rung
	private float intensity = 8f;

	//so lan rung
	private int shakes = 30;


	//get the original camera's x position
	void Start()
	{
		baseX = transform.localPosition.x;
	}

	void Update()
	{
		// if it is shaking
		if (isShaking) 
		{
			//get camera current y position
			baseY = transform.localPosition.y;


			// random shake range ( we dont use Y cause it look weird)
			float randomShakeX = Random.Range (-intensity, intensity);
			float randomShakeY = Random.Range (-intensity, intensity);


			//shake the background by update the background position with addition shake value
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

			// decrease shakes count ( so lan rung)
			shakes--;

			// if shakes <= 0 , stop shaking , reset original X position of backgrounds
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

	//trigger shake update
	public void MinorShake(float _intensity)
	{
		isShaking = true;
		shakes = 50;
		intensity = _intensity;
	}
}