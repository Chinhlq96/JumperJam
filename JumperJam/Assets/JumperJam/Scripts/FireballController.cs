using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour 
{
	[SerializeField]
	private float duration;

	public void Shoot (float speed) {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(speed, GetComponent<Rigidbody2D> ().velocity.y);
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Destroy ();
		} 
	}

	public void Destroy() {
		ContentMgr.Instance.Despaw (gameObject);
	}
}
