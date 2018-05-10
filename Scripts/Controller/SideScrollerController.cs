using System;
using UnityEngine;

namespace Commons
{
	public abstract class SideScrollerController : MonoBehaviour
	{

		private bool _moving = false;
		private bool _jumping = false;

		private bool _facingRight = true;

		private float _prevYVelocity = 0;

		private Animator animator;

		protected virtual void Start() {
			setAnimator(gameObject.GetComponent<Animator> ());
		}

		protected virtual void Update() {
			if (Input.GetKey (KeyCode.RightArrow)) {
				_facingRight = true;
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = !_facingRight;
				move (true);
			} else if (Input.GetKey (KeyCode.LeftArrow)) {
				_facingRight = false;
				this.gameObject.GetComponent<SpriteRenderer> ().flipX = !_facingRight;
				move (true);
			}

			if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.LeftArrow)) {
				move (false);
			}

			if (Input.GetKeyDown (KeyCode.UpArrow)) {
				jump ();
			}

			if (Math.Abs(_prevYVelocity) < 0.5 && Math.Abs(gameObject.GetComponent<Rigidbody2D> ().velocity.y) < 0.5) {
				_jumping = false;
				animator.SetBool ("jumping", _jumping);
			}

			_prevYVelocity = gameObject.GetComponent<Rigidbody2D> ().velocity.y;
		}

		public bool isMoving() {
			return _moving;
		}

		public void setMoving(bool moving) {
			this._moving = moving;
		}

		public bool isJumping() {
			return _jumping;
		}

		public void setJumping(bool jumping) {
			this._jumping = jumping;
		}

		public bool isFacingRight() {
			return _facingRight;
		}

		public void setFacingRight(bool facingRight) {
			this._facingRight = facingRight;
		}

		public Animator getAnimator() {
			return animator;
		}

		public void setAnimator(Animator animator) {
			this.animator = animator;
		}

		protected void move(bool yes) {
			if (_moving != yes) {
				animator.SetBool ("walking", yes);
			}
			_moving = yes;
			gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (yes ? 3 * (_facingRight ? 1 : -1) : 0, gameObject.GetComponent<Rigidbody2D> ().velocity.y);
		}

		protected void jump() {
			animator.SetTrigger ("jump");
			if (!_jumping) {
				_jumping = true;
				animator.SetBool ("jumping", _jumping);
			}
			gameObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.up * 5, ForceMode2D.Impulse);
		}

	}
}

