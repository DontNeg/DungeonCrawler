using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapInit : MonoBehaviour
{
    public GameObject spike;
    public GameObject hole;
    
    private void Start()
    {
        Physics2D.IgnoreLayerCollision(3,10);
        for (var i = 0; i <Random.Range(2,6); i++)
        {
            var coordinatesSet = new HashSet<float[]>();
            float xHole = Random.Range(3, 20)+0.5f, xSpike = Random.Range(3, 20)+0.5f;
            float yHole = Random.Range(-10, -2)+0.5f, ySpike = Random.Range(-10, -2)+0.5f;
            if (!IsInitialized(coordinatesSet, new []{xHole,yHole,xSpike,ySpike}))
            {
                Instantiate(spike,new Vector3(xSpike,ySpike,1f),Quaternion.identity);
            }
            if (!IsInitialized(coordinatesSet, new []{xHole,yHole,xSpike,ySpike}))
            {
                Instantiate(hole,new Vector3(xHole,yHole,1f),Quaternion.identity);
            }
            coordinatesSet.Add(new [] {xSpike,ySpike});
            coordinatesSet.Add(new [] {xHole,yHole});
        }
    }
    private static bool IsInitialized(ICollection<float[]> coordSet, IReadOnlyList<float> coords)
    {
        return coordSet.Contains(new[] { coords[0], coords[1] }) 
               && coordSet.Contains(new[] { coords[2], coords[3] });
    }
}
