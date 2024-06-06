using System.Collections.Generic;
using UnityEngine;

public class AttackDelete : MonoBehaviour
{
    private Player _player;
    private readonly HashSet<int> _names = new();

    private void Start()
    {
        AddToNames();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        Uninstantiate();
    }

    private void Uninstantiate()
    {
        if (_names.Contains(_player.animator.GetCurrentAnimatorStateInfo(0).shortNameHash)) return;
        _player.DeactivateTrigger();
        Destroy(gameObject);
    }

    private void AddToNames()
    {
        _names.Add(1782521546);
        _names.Add(-638551315);
        _names.Add(-1083699778);
        _names.Add(700159270);
    }
}