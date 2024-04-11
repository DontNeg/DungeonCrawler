using UnityEngine;

public class Logic : MonoBehaviour
{
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6,7);
    }

    
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("AHH");
    }
}
