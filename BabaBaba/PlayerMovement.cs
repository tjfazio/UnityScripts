using UnityEngine;
using System.Collections;
using BabaBaba;

public class PlayerMovement : MonoBehaviour {

	private static readonly Vector2 IdleDirection = new Vector2 (0f, -1f);

	public float Speed = 2.0f;
	public LayerMask BlockingLayer;

	public Vector2 Direction { get { return this._Direction; } }
	
	private BoxCollider2D _Collider;
	private Animator _Animator;
	private int _LastAnimationDirection;
	private Vector2 _Direction;
	
	void Start () 
	{
		this._Collider = this.GetComponent<BoxCollider2D>();
		this._Animator = this.GetComponent<Animator> ();
		this._LastAnimationDirection = 0;
		this._Direction = IdleDirection;
	}
	
	void Update () 
	{		
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw ("Vertical");
		
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
	
	private void Move(float horizontal, float vertical)
	{		
		Vector2 direction = new Vector2 (horizontal, vertical);
		float distance = this.Speed * Time.deltaTime;

		Vector2 start = (Vector2)this._Collider.transform.position + this._Collider.offset;
		RaycastHit2D hit = Physics2D.BoxCast (start, this._Collider.size, 0, direction, distance, this.BlockingLayer);
		
		this.AnimateMovement (horizontal, vertical);
		if (null != hit.transform) {
			return;
		}
		
		Vector2 displacement = direction * distance;
		this.transform.position += (Vector3)displacement;
	}
	
	private void AnimateMovement(float horizontal, float vertical)
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
		if (this._LastAnimationDirection == direction) {
			return;
		}
		this._LastAnimationDirection = direction;
		this._Direction = new Vector2 (horizontal, vertical);
		this._Animator.SetInteger ("Direction", direction);
	}
	
	private void Idle()
	{	
		this._LastAnimationDirection = 0;
		this._Direction = IdleDirection;
		this._Animator.SetInteger("Direction", 0);
	}
}
