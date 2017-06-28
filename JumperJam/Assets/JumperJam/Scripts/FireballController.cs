using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour 
{
	[SerializeField]
	private float duration;
	void Update() {
	}
	IEnumerator DestroyAfter() {
		yield return new WaitForSeconds(duration);
		Destroy ();
	}
	public void Shoot (float speed) {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(speed, GetComponent<Rigidbody2D> ().velocity.y);
		StartCoroutine ("DestroyAfter");

	}

	public void Shoot (float speed, float height) {
		GetComponent<Rigidbody2D> ().velocity = new Vector2(speed, height);
		StartCoroutine ("DestroyAfter");

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
