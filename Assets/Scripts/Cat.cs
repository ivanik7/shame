using UnityEngine;
using System.Collections;

public class Cat : Unit
{

	int lives = 9;
	float speed = 3.0f;
	float jumpForce = 30.0f;

	new private Rigidbody2D rigibody;
	private Animator animator;
	private SpriteRenderer sprite;

	private bool isGrounded = false;
	private State state
	{
		get { return (State)animator.GetInteger("state"); }
		set { animator.SetInteger("state", (int)value); }
	}

	private void Awake()
	{
		rigibody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		sprite = GetComponentInChildren<SpriteRenderer>();

	}

	private void FixedUpdate()
	{
		CheckGround();
		if (!isGrounded) state = State.Jump;
	}

	private void Update()
	{
		if (isGrounded) state = State.Idle;
		if (Input.GetButton("Horizontal")) Run();
		if (isGrounded && Input.GetButtonDown("Jump")) jump();
		
	}
	private void Run()
	{
		if (isGrounded) state = State.Run;
		Vector3 direction = transform.right * Input.GetAxis("Horizontal");
		transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
		sprite.flipX = direction.x > 0;
	}

	private void jump()
	{
		rigibody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
	}

	private void CheckGround()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
		isGrounded = colliders.Length > 1;
	}
}

public enum State
{
	Idle,
	Run,
	Jump
}