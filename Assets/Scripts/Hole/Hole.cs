using UnityEngine;

public class Hole : MonoBehaviour
{
    public Animator animator;
    private static readonly int IsTriggered = Animator.StringToHash("isTriggered");
    private Player _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
