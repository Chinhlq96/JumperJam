using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour 
{
	[SerializeField]
	private float duration;

	private bool isDespawed = false;
	void Update() {
	}
	IEnumerator DestroyAfter() {
		yield return new WaitForSeconds(duration);
		Destroy ();
		isDespawed = true;
	}
	public void Shoot (float speed) {
		isDespawed = false;
		GetComponent<Rigidbody2D> ().velocity = new Vector2(speed, GetComponent<Rigidbody2D> ().velocity.y);
		StartCoroutine ("DestroyAfter");

	}

	public void Shoot (float speed, float height) {
		isDespawed = false;
		GetComponent<Rigidbody2D> ().velocity = new Vector2(speed, height);
		StartCoroutine ("DestroyAfter");

	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			Destroy ();
			isDespawed = true;
		} 
	}

	public void Destroy() {
		if (!isDespawed)
			ContentMgr.Instance.Despaw (gameObject);
	}
}
