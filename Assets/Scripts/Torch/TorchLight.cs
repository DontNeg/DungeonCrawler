using UnityEngine;

public class TorchLight : MonoBehaviour
{
    private Torch _torch;

    private void Start()
    {
        _torch = GameObject.FindGameObjectWithTag("Torch").GetComponent<Torch>();
        
    }

    private void Update()
    {
        if (_torch.GetActive() && _torch.GetFuel() > 0 && !Input.GetKeyDown(KeyCode.G)) return;
        _torch.SetActive(false);
        Destroy(gameObject);
    }




}
