using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float Speed = 2.0f;
	public LayerMask BlockingLayer;

	private BoxCollider2D _BoxCollider;
	private Rigidbody2D _Rigidbody;
	private Animator _Animator;

	// Use this for initialization
	void Start () 
	{
		this._BoxCollider = this.GetComponent<BoxCollider2D>();
		this._Rigidbody = this.GetComponent<Rigidbody2D>();
		this._Animator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		int horizontal = (int) Input.GetAxisRaw("Horizontal");
		int vertical = (int) Input.GetAxisRaw ("Vertical");

		if (vertical != 0)
		{
			horizontal = 0;
		}
		if (horizontal != 0 || vertical != 0)
		{
			this.Move (horizontal, vertical);
		}
		else
		{
			this.Idle();
		}
	}

	private void Move(int horizontal, int vertical)
	{		
		this.AnimateMovement(horizontal, vertical);
		this.Move(new Vector2(horizontal, vertical));
	}

	private void Move(Vector2 direction)
	{		
		/*if (this._BoxCollider.IsTouchingLayers(this.BlockingLayer))
		{
			return;
		}*/
		
		//this._Rigidbody.velocity = direction * this.Speed * Time.deltaTime;

		Vector2 start = this.transform.position;
		Vector2 end = start + direction * this.Speed * Time.deltaTime;
		RaycastHit2D hit = Physics2D.Linecast (start, end, BlockingLayer);

		if (this._BoxCollider.IsTouchingLayers(this.BlockingLayer) && null != hit)
		{
			return;
		}


		Vector3 newPosition = new Vector3(end.x, end.y, 0f);
		this._Rigidbody.MovePosition (newPosition);
		/*if (this._BoxCollider.IsTouchingLayers(this.BlockingLayer))
		{
			this._Rigidbody.MovePosition (start);
		}*/
	}

	void OnCollision2D(Collision2D collision)
	{
		/*if (collision.gameObject.layer == this.BlockingLayer)
		{
			this._Rigidbody.velocity = Vector2.zero;
		}*/
	}

	void OnTriggerEnter(Collider other)
	{
		//if (collision.gameObject.layer != this.BlockingLayer)
		//{
		//	return;
		//}
		//this._Rigidbody.velocity = Vector2.zero;
	}

	void OnTriggerStay(Collider collider)
	{
		//this._Rigidbody.velocity = Vector2.zero;
	}


	private void AnimateMovement(int horizontal, int vertical)
	{
		int direction = 0;
		if (vertical > 0)
		{
			direction = 2;
		}
		else if (vertical < 0)
		{
			direction = 4;
		}
		else if (horizontal > 0)
		{
			direction = 1;
		}
		else if (horizontal < 0)
		{
			direction = 3;
		}
		this._Animator.SetInteger ("Direction", direction);
	}

	private void Idle()
	{
		this._Rigidbody.velocity = Vector2.zero;
		this._Animator.SetInteger("Direction", 0);
	}
}
