using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public Transform[] Waypoints { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        FindWaypointTransforms();
    }

    private void FindWaypointTransforms()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform child in transform)
        {
            //child is your child transform
            waypoints.Add(child);
        }

        Waypoints = waypoints.ToArray();
    }

}
