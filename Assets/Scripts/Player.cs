using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Test")]
    public Text text;
    
    [Header("Movement&Inputs")]
    public Animator animator;
    public new Rigidbody2D rigidbody;
    public float moveSpeed;
    public KeyCode attack;
    public int health;
    
    [Header("Objects")] 
    public GameObject trigger;

    private bool _isAlive;
    private Vector2 _moveDirection;
    private float _moveX;
    private float _moveY;
    private int _dir;
    private readonly ArrayList _movement = new();
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Direction = Animator.StringToHash("Direction");


    private void Start()
    {
        InstantiateArray();
    }
    
    private void Update()
    {
        Inputs();
        Animation();
        GetDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Inputs()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector2(_moveX, _moveY).normalized;
        text.text = health.ToString();
        if (!Input.GetKeyDown(attack) || IsMoving()) return;
        DoAttack();
        
    }

    private void Animation()
    {
        animator.SetInteger(SpeedX, (int)_moveX);
        animator.SetInteger(SpeedY, (int)_moveY);
        animator.SetBool(Attack,Input.GetKey(attack));
        animator.SetInteger(Direction,_dir);
    }
    
    private void GetDirection()
    {
        foreach (KeyCode key in _movement)
        {
            if (Input.GetKey(key))
            {
                _dir = key switch
                {
                    KeyCode.W => 1,
                    KeyCode.A => 2,
                    KeyCode.S => 3,
                    KeyCode.D => 4,
                    _ => _dir
                };
            }
        }
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }
    
    private void InstantiateArray()
    {
        _movement.Add(KeyCode.W);
        _movement.Add(KeyCode.A);
        _movement.Add(KeyCode.S);
        _movement.Add(KeyCode.D);
    }

    private void DoAttack()
    {
        var position = transform.position;
        Instantiate(trigger, new Vector2(
            position.x + (float)GetTriggerPlacement()[0],
            position.y - (float)GetTriggerPlacement()[1]), 
              Quaternion.identity);
    }

    private ArrayList GetTriggerPlacement()
    {
        var x = 0f;
        var y = 0f;
        switch (_dir)
        {
            case 1:
                x = 1.5f;
                y = 1f;
                break;
            case 2:
                x = 1f;
                y = 1.5f;
                break;
            case 3:
                x = 1.5f;
                y = 2f;
                break;
            case 4:
                x = 2f;
                y = 1.5f;
                break;
        }
        var triggerPlacement = new ArrayList { x, y };
        return triggerPlacement;
    }

    private bool IsMoving()
    {
        var xSpeed = animator.GetInteger(SpeedX);
        var ySpeed = animator.GetInteger(SpeedY);
        return xSpeed != 0 || ySpeed != 0;
    }
    
    // private Animator GetAnimator()
    // {
    //     return animator;
    // }
    
    /*
    Public Functions
    */

    public int GetHealth()
    {
        return health; 
    }
    
    // ReSharper disable once ParameterHidesMember
    public void SubtractHealth(int damage)
    {
        health -= damage; 
    }

    public void SetHealth(int amount)
    {
        health = amount;
    }
}