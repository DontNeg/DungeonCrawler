using UnityEngine;

public class Spike : MonoBehaviour
{
    public Animator animator;
    private Movement _player;
    private static readonly int IsPressed = Animator.StringToHash("isPressed");

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool(IsPressed,true);
        if (_player.GetHealth() <= 0) return;
        _player.SubtractHealth(10);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool(IsPressed,false);
    }
}
