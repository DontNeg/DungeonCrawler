using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Header("Test")]
    public Text text;
    
    [Header("Movement&Inputs")]
    public Animator animator;
    public new Rigidbody2D rigidbody;
    public float moveSpeed;
    public KeyCode attack;
    public KeyCode watering;
    public int health;
    
    [Header("Objects")] 
    public GameObject attackTrigger;
    public GameObject wateringTrigger;

    private bool _triggerActive;
    private bool _isAlive;
    private Vector2 _moveDirection;
    private float _moveX;
    private float _moveY;
    private int _dir;
    private BoxCollider2D _boxCollider;
    private readonly ArrayList _movement = new();
    private readonly ArrayList _action = new();
    private static readonly int SpeedX = Animator.StringToHash("SpeedX");
    private static readonly int SpeedY = Animator.StringToHash("SpeedY");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Direction = Animator.StringToHash("Direction");
    private static readonly int Watering = Animator.StringToHash("Watering");


    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        InstantiateArrays();
    }
    
    private void Update()
    {
        Inputs();
        Animation();
        GetDirection();
        // HitBox();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void HitBox()
    {
        _boxCollider.size = _dir switch
        {
            1 => new Vector2(),
            2 => new Vector2(),
            3 => new Vector2(),
            4 => new Vector2(),
            _ => new Vector2()
        };
    }

    private void Inputs()
    {
        _moveX = Input.GetAxisRaw("Horizontal");
        _moveY = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector2(_moveX, _moveY).normalized;
        if (IsMoving()) return;
        foreach (KeyCode key in _action)
        {
            if (!Input.GetKeyDown(key)) continue;
            switch (key)
            {
                case KeyCode.E:
                    DoAction(attackTrigger);
                    break;
                case KeyCode.F:
                    DoAction(wateringTrigger);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void Animation()
    {
        animator.SetInteger(SpeedX, (int)_moveX);
        animator.SetInteger(SpeedY, (int)_moveY);
        animator.SetBool(Attack,Input.GetKeyDown(attack));
        animator.SetBool(Watering,Input.GetKeyDown(watering));
        animator.SetInteger(Direction,_dir);
    }
    
    private void GetDirection()
    {
        foreach (KeyCode key in _movement)
        {
            if (!Input.GetKey(key)) continue;
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

    private void Move()
    {
        rigidbody.velocity = new Vector2(_moveDirection.x * moveSpeed, _moveDirection.y * moveSpeed);
    }
    
    private void InstantiateArrays()
    {
        //Movement Keys
        _movement.Add(KeyCode.W);
        _movement.Add(KeyCode.A);
        _movement.Add(KeyCode.S);
        _movement.Add(KeyCode.D);
        //Action Keys
        _action.Add(KeyCode.E);
        _action.Add(KeyCode.F);
    }
    
    private ArrayList GetTriggerPlacement(GameObject obj)
    {
        return obj.name switch
        {
            "AttackTrigger" => AttackLogic.GetTriggerPlacement(_dir),
            "WateringTrigger" => WateringLogic.GetTriggerPlacement(_dir),
            _ => new ArrayList()
        };
    }

    private void DoAction(GameObject obj)
    {
        if (_triggerActive) return;
        _triggerActive = true;
        var position = transform.position;
        Instantiate(obj, new Vector2(
                position.x + (float)GetTriggerPlacement(obj)[0],
                position.y - (float)GetTriggerPlacement(obj)[1]),
                Quaternion.identity);
    }
    
    private bool IsMoving()
    {
        var xSpeed = animator.GetInteger(SpeedX);
        var ySpeed = animator.GetInteger(SpeedY);
        return xSpeed != 0 || ySpeed != 0;
    }

    /*
    Public Functions
    */
    public int GetHealth()
    {
        return health;
    }
    
    public void SubtractHealth(int damage)
    {
        health -= damage; 
    }

    public void SetHealth(int amount)
    {
        health = amount;
    }

    public void DeactivateTrigger()
    {
        _triggerActive = false;
    }

    public int GetDir()
    {
        return _dir;
    }

    public Text GetText()
    {
        return text;
    }
}