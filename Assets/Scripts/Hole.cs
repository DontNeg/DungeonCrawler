using UnityEngine;

public class Hole : MonoBehaviour
{
    public Animator animator;
    private Movement _player;
    private static readonly int IsTriggered = Animator.StringToHash("isTriggered");

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        animator.SetBool(IsTriggered,true);
        if (_player.GetHealth() <= 0) return;
        _player.SetHealth(0);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        animator.SetBool(IsTriggered,false);
    }
}
