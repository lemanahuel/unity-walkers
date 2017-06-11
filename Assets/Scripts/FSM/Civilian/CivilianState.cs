using UnityEngine;
using System.Collections;

public class CivilianState : State {

	protected Civilian civilian;

	public CivilianState(StateMachine sm, Civilian c) : base(sm)	{
		civilian = c;
	}
}
