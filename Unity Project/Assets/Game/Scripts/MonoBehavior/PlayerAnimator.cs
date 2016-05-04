using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{
	public Animator anim;

	private CharacterMotor chrMotor;

	void Start()
	{
		chrMotor = GetComponent<CharacterMotor>();
	}

	void Update()
	{
		anim.SetFloat("velocity",
			Mathf.Sqrt(chrMotor.movement.velocity.z * chrMotor.movement.velocity.z + chrMotor.movement.velocity.x * chrMotor.movement.velocity.x));
		anim.SetFloat("JumpVelocity", chrMotor.movement.velocity.y);
	}
}
