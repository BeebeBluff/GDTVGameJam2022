using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHelper : MonoBehaviour
{
    Transform[] waypointTransforms;
    Waypoint[] waypointArray;
    Transform playerTransform;

    // This script is to help the Enemy object find references in the scene at spawn time.
    void Start()
    {
        waypointArray = FindObjectsOfType<Waypoint>();



    }

    public void FindWaypointTransforms()
    {

        for (int i = 0; i < waypointArray.Length; i++)
        {

        }
    }

}
