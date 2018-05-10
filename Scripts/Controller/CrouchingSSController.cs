using System;
using UnityEngine;

namespace Commons
{
	public class CrouchingSSController : SideScrollerController
	{

		private bool _crouching = false;

		override protected void Update() {
			base.Update ();
			if (Input.GetKeyDown (KeyCode.DownArrow) && !isJumping()) {
				move (false);
				crouch (true);
			}

			if (Input.GetKeyUp (KeyCode.DownArrow)) {
				crouch (false);
			}
		}

		private void crouch(bool yes) {
			if (_crouching != yes) {
				getAnimator().SetBool ("crouching", yes);
			}
			_crouching = yes;
		}
	}
}

