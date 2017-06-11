using UnityEngine;
using System.Collections;

public class WalkerState : State {

	protected Walker walker;

	public WalkerState(StateMachine sm, Walker w) : base(sm)	{
		walker = w;
	}
}
