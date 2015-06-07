using UnityEngine;
using System.Collections;

public class BombBag : MonoBehaviour {

	public int MaxBombs = 1;
	public float BombPlaceDelay = 1.0f;
	public GameObject Bomb;

	private int _BombsPlaced;
	private float _NextBombTime;

	// Use this for initialization
	void Start () {
		this._BombsPlaced = 0;
		this._NextBombTime = 0.0f;	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButton ("Jump") && this.CanPlaceBomb ()) {
			this.PlaceBomb ();
		}
	}

	private bool CanPlaceBomb()
	{
		return (this._BombsPlaced < this.MaxBombs && Time.time > this._NextBombTime);
	}

	private void PlaceBomb()
	{
		GameObject bombObject = (GameObject)GameObject.Instantiate (Bomb, this.transform.position, Quaternion.identity);
		Bomb bomb = bombObject.GetComponent<Bomb> ();
		bomb.BombBag = this;
		this._BombsPlaced++;
		this._NextBombTime = Time.time + this.BombPlaceDelay;
	}

	public void ReturnBomb()
	{
		this._BombsPlaced--;
	}
}
