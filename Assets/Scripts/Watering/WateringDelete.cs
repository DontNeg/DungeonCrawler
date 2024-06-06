using System.Collections.Generic;
using UnityEngine;

public class WateringDelete : MonoBehaviour
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
        _names.Add(266124911);
        _names.Add(1628177891);
        _names.Add(-1160871143);
        _names.Add(-598281142);
    }
}
