using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static List<Transform> Path { get; set; }

    void Awake()
    {
        FindPath();
    }

    void FindPath()
    {
        Path = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Path.Add(transform.GetChild(i));
        }
    }
}
