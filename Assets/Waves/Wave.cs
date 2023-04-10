using UnityEngine;

[System.Serializable]
public class Wave
{
    public Subwave[] subwaves;
    [Range(0.1f, 5f)] public float waitTime;
}