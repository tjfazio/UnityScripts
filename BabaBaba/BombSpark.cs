using UnityEngine;
using System.Collections;

public class BombSpark : MonoBehaviour {

	public Bomb ParentBomb;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag ("Indestructible")) {
			Debug.Log ("BombSpark hit indestructible " + collider.gameObject.name);
			this.Fizzle();
		} else {
			Debug.Log ("BombSpark hit " + collider.gameObject.name);			
			collider.gameObject.SendMessage ("OnBombHit");
		}
	}

	private void Fizzle()
	{
		Debug.Log ("BombSpark: Fizzle");
		this.ParentBomb.Fizzle (this);
	}
}
