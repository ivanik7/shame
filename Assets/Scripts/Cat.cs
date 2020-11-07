using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cat : Unit
{

    public int maxSatiety = 3;
    public float speed = 10;
    public float jumpForce = 30.0f;
    public float fallMultiplyer = 2.5f;
    public float jumpMultiplyer = 2.0f;
    public float yBorder = -5;
    public int satiety;
    private bool isGrounded = false;

    private Rigidbody2D rigibody;
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
        else if (Math.Abs(rigibody.velocity.x) > 0.1f) state = State.Run;
        else state = State.Idle;
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) jump();
        if (transform.position.y < yBorder) Respawn();

        if (rigibody.velocity.y < 0)
        {
            rigibody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplyer - 1) * Time.deltaTime;
        }
        else if (rigibody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rigibody.velocity += Vector2.up * Physics2D.gravity.y * (2f - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "EndTrigger")
        {
            SceneManager.LoadScene(2);
        }
    }

    private void Run()
    {
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
        // FIXME
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        isGrounded = colliders.Length > 1;
    }

    private void Respawn()
    {
        // TODO: Maybe gameover
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
    Jump,
    Damaged
}