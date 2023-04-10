using UnityEngine;

[System.Serializable]
public class Subwave
{
    public GameObject enemy;
    public int count;
    [Range(0.1f, 15f)] public float rate;
    [Range(0.1f, 5f)] public float waitTime;
}
