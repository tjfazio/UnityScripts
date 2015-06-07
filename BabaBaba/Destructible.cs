using UnityEngine;
using System.Collections;

public class Destructible : MonoBehaviour {

	void OnBombHit()
	{
		Destroy (this.gameObject);
	}
}
