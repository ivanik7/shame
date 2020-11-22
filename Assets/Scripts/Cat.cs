using System;
using UnityEngine;

public class Cat : Unit
{

    public int maxSatiety = 3;
    public float speed = 10f;
    public float waterSpeed = 3f;
    private float currentSpeed = 0f;

    public float yBorder = -5;
    public int satiety;
    private bool isDamaged = false;
    private float damageTime = 0f;

    public Transform end;
    private Rigidbody2D rigibody;
    private Animator animator;
    private SpriteRenderer sprite;
    private Jump jump;

    private State state
    {
        get { return (State)animator.GetInteger("state"); }
        set
        {
            if (!isDamaged) animator.SetInteger("state", (int)value);
        }
    }

    private void Awake()
    {
        rigibody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        jump = GetComponentInChildren<Jump>();

        currentSpeed = speed;
        satiety = maxSatiety;
    }

    private void Start()
    {
        Result.startTime = DateTime.Now;
    }

    private void FixedUpdate()
    {
        // TODD: Возможно перенести это в Update() 
        if (!jump.isGrounded) state = State.Jump;
        else if (Math.Abs(rigibody.velocity.x) > 0.1f) state = State.Run;
        else state = State.Idle;
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal")) Run();
        if (Input.GetButtonDown("Jump")) jump.DoJump();
        jump.holdJump = Input.GetButton("Jump");
        if (transform.position.y < yBorder) Die();

        if (Time.time - damageTime > 5f)
        {
            isDamaged = false;
        }

        if (transform.position.x > end.position.x)
        {
            Result.Psss();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, currentSpeed * Time.deltaTime);
        sprite.flipX = direction.x > 0;
    }

    protected override void Die()
    {
        Result.Fail();
    }

    public void Eat()
    {
        satiety++;
        Result.birds++;
    }

    public override void ReceiveDamage()
    {
        if (!isDamaged)
        {
            Result.damage++;
            satiety--;
            state = State.Die;
            isDamaged = true;
            damageTime = Time.time;
            if (satiety <= 0) Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        // FIXME
        if (collisionInfo.gameObject.name == "Water")
        {
            currentSpeed = waterSpeed;
        }

    }
    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        // FIXME
        if (collisionInfo.gameObject.name == "Water")
        {
            currentSpeed = speed;
        }
    }

}

public enum State
{
    Idle,
    Run,
    Jump,
    Die
}