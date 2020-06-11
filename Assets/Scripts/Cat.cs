using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : Unit
{

	public int maxSatiety = 3;
	public float speed = 3.0f;
	public float jumpForce = 30.0f;
	public float yBorder = -5;
	public int satiety;

	private bool isGrounded = false;

	new private Rigidbody2D rigibody;
	private Animator animator;
	private SpriteRenderer sprite;

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

		satiety = maxSatiety;
	}

	private void Start()
	{
		Stats.startTime = DateTime.Now;
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
		if (transform.position.y < yBorder) Respawn();
		
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.name == "EndTrigger")
		{
			SceneManager.LoadScene(2);
		}
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

	private void Respawn()
	{
		satiety = maxSatiety;
		Stats.dies++;
		var respawn = GameObject.Find("Respawn");
		transform.position = respawn.transform.position;
	}

	public void Eat()
	{
		satiety++;
		Stats.birds++;
		Debug.Log(satiety);
	}

	public override void ReceiveDamage()
	{
		Stats.damage++;
		satiety--;
		if (satiety <= 0) Respawn(); 
	}
}

public enum State
{
	Idle,
	Run,
	Jump
}