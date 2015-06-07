using UnityEngine;
using System.Collections;
using BabaBaba;

public class PlayerMovement : MonoBehaviour {

	public float MoveTime = 0.1f;
	public LayerMask BlockingLayer;

	private Rigidbody2D _Rigidbody;
	private Animator _Animator;
	private float _InverseMoveTime;
	private bool _IsMoving;

	void Start () 
	{
		this._Rigidbody = this.GetComponent<Rigidbody2D>();
		this._Animator = this.GetComponent<Animator> ();
		this._InverseMoveTime = 1f / this.MoveTime;
		this._IsMoving = false;
	}
	
	void Update () 
	{		
		if (this._IsMoving)
		{
			return;
		}

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
			this.Idle ();
		}
	}
	
	private void Move(int horizontal, int vertical)
	{		
		Vector2 direction = new Vector2(horizontal, vertical) * Constants.BlockSize;
		Vector2 start = this.transform.position;
		Vector2 end = start + direction;

		RaycastHit2D hit = Physics2D.Linecast (start,end, this.BlockingLayer);

		if (null == hit.transform)
		{
			this._IsMoving = true;
			this.AnimateMovement(horizontal, vertical);
			StartCoroutine(SmoothMovement(end));
		}
		else
		{
			this._IsMoving = false;
		}
	}

	private IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
		
		while (sqrRemainingDistance > float.Epsilon) 
		{
			Vector3 newPosition = Vector3.MoveTowards(this._Rigidbody.position, end, this._InverseMoveTime * Time.deltaTime);
			this._Rigidbody.MovePosition(newPosition);
			sqrRemainingDistance = (transform.position - end).sqrMagnitude;
			yield return null;
		}
		this._IsMoving = false;
		yield return null;
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
		this._Animator.SetInteger("Direction", 0);
	}
}
