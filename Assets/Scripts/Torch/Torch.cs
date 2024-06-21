using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public Animator animator;
    public GameObject torchLight;

    private float _moveSpeed;
    private bool _triggered;
    private bool _active;
    private int _takeFuel;
    private int _fuel = 20;
    private CapsuleCollider2D _capsuleCollider2D;
    private BoxCollider2D _playerBoxCollider2D;
    private Player _player;
    private Vector2 _moveDirection;
    private static readonly int Active = Animator.StringToHash("Active");


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(),
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>());
        InvokeRepeating(nameof(TakeFuel),0.001f,0.001f);
    }

    private void Update()
    {
        _player.GetText().text = _active + " " + _fuel;
        Animation();
        Lighting();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void TakeFuel()
    {
        if (!_active) return;
        _takeFuel++;
        if (_takeFuel != 1000) return;
        _fuel--;
        _takeFuel = 0;
    }

    private void Animation()
    {
        animator.SetBool(Active, _active);
    }
    

    private void Movement()
    {
        _moveSpeed = (float)Math.Sqrt(
            Math.Pow(_player.transform.position.x - (transform.position.x - 1.5f), 2) +
            Math.Pow(_player.transform.position.y - (transform.position.y + 1.5f), 2)
        );
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.position = Vector3.Lerp(transform.position, VectorDirection(), Time.fixedDeltaTime * _moveSpeed);
    }

    private void Lighting()
    {
        if (!Input.GetKeyDown(KeyCode.G) || _active || _fuel <= 0) return;
        _active = true;
        Instantiate(torchLight,
            new Vector3(transform.position.x,transform.position.y),
            Quaternion.identity, transform
        );
    }

    private Vector3 VectorDirection()
    {
        var xOffset = 0f;
        var yOffset = 0f;
        switch (_player.GetDir())
        {
            case 1:
                xOffset = 1.5f;
                yOffset = 2.5f;
                break;
            case 2:
                xOffset = 2.5f;
                yOffset = 1.5f;
                break;
            case 3:
                xOffset = 1.5f;
                yOffset = 0.5f;
                break;
            case 4:
                xOffset = 0.5f;
                yOffset = 1.5f;
                break;
        }
        return new Vector3(_player.transform.position.x + xOffset, _player.transform.position.y - yOffset);
    }

    public int GetFuel(){return _fuel;}
    public bool GetActive(){return _active;}
    public void SetActive(bool value){_active = value;}
}