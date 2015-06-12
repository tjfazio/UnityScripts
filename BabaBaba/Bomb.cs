using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BabaBaba;

public class Bomb : MonoBehaviour {

	public BombBag BombBag;
	public GameObject BombSpark;
	public int Magnitude = 1;
	public float ExplosionDelay = 3.0f;
	public float ExplosionDuration = 3.0f;

	private float _TimeToExplode;
	private bool _IsExploding;
	private float _TimeToDestroy;
	private List<GameObject> _Sparks = new List<GameObject>();

	void Start () 
	{
		this._TimeToExplode = Time.time + this.ExplosionDelay;
		this._TimeToDestroy = this._TimeToExplode + this.ExplosionDuration;
		this._IsExploding = false;
	}

	void Update () 
	{
		if (!this._IsExploding && Time.time >= this._TimeToExplode) 
		{
			this.Explode ();
		}
		if (Time.time >= this._TimeToDestroy) 
		{
			this.EndExplosion();
		}	
	}

	private void Explode()
	{
		if (this._IsExploding) 
		{
			return;
		}
		this._IsExploding = true;
		this.SpawnSparkLine (new Vector2 (1f, 0f));
		this.SpawnSparkLine (new Vector2 (0f, 1f));
		this.SpawnSparkLine (new Vector2 (-1f, 0f));
		this.SpawnSparkLine (new Vector2 (0f, -1f));
	}

	private void SpawnSparkLine(Vector2 direction)
	{
		for (int i = 0; i < this.Magnitude; i++) 
		{
			Vector3 displacement = direction * (i + 1) * Constants.BlockSize;
			this.SpawnSpark(this.transform.position + displacement);
		}
	}

	private void SpawnSpark(Vector2 position)
	{
		GameObject sparkObject = (GameObject)Instantiate (this.BombSpark, position, Quaternion.identity);
		sparkObject.transform.parent = this.transform;
		BombSpark spark = sparkObject.GetComponent<BombSpark> ();
		spark.ParentBomb = this;
		this._Sparks.Add (sparkObject);
	}

	private void EndExplosion()
	{
		this.BombBag.ReturnBomb ();
		foreach (GameObject spark in this._Sparks) 
		{
			Destroy (spark);
		}
		Destroy (this.gameObject);
	}

	public void Fizzle(BombSpark spark)
	{
		Debug.Log ("Bomb: Fizzle");
		this._Sparks.Remove (spark.gameObject);
		Destroy (spark.gameObject);
	}

	public void OnBombHit()
	{
		Debug.Log ("Bomb: OnBombHit");
		this.Explode ();
	}
}
