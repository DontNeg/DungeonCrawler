using System;
using UnityEngine;

public class Delete : MonoBehaviour
{
    private Movement _player;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    private void Update()
    {
        Uninstantiate();
    }

    private void Uninstantiate()
    {
        if (!_player.animator.GetBool(Attack))
        {
            Destroy(gameObject);
        }
    }
}
