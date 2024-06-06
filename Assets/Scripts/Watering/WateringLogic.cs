using System.Collections;
using UnityEngine;

public class WateringLogic : MonoBehaviour
{
    private void Start()
    {
        Ignore();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Seeds":
                Debug.Log("WATERING SEEDS");
                break;
        }
    }

    private static void Ignore()
    {
        for (var i = 0; i < 9; i++)
        {
            Physics2D.IgnoreLayerCollision(6,i);
        }
    }
    
    public static ArrayList GetTriggerPlacement(int dir)
    {
        var x = 0f;
        var y = 0f;
        switch (dir)
        {
            case 1:
                x = 1.5f;
                y = 1f;
                break;
            case 2:
                x = 0.25f;
                y = 1.5f;
                break;
            case 3:
                x = 1.5f;
                y = 2f;
                break;
            case 4:
                x = 2.75f;
                y = 1.5f;
                break;
        }
        return new ArrayList { x, y };
    }
    
}
