using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapInit : MonoBehaviour
{
    public GameObject spike;
    public GameObject hole;
    
    private void Start()
    {
        for (var i = 0; i <Random.Range(2,6); i++)
        {
            var coordinatesSet = new HashSet<float[]>();
            var xSpike = Random.Range(3, 20)+0.5f;
            var ySpike = Random.Range(-10, -2)+0.5f;
            var xHole = Random.Range(3, 20)+0.5f;
            var yHole = Random.Range(-10, -2)+0.5f;
            if (!coordinatesSet.Contains(new []{xSpike,ySpike}))
            {
                Instantiate(spike,new Vector3(xSpike,ySpike,1f),Quaternion.identity);
            }
            if (!coordinatesSet.Contains(new []{xHole,yHole}))
            {
                Instantiate(hole,new Vector3(xHole,yHole,1f),Quaternion.identity);
            }
            coordinatesSet.Add(new [] {xSpike,ySpike});
            coordinatesSet.Add(new [] {xHole,yHole});
        }
    }
}
