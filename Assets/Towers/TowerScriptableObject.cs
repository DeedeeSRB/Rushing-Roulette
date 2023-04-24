using UnityEngine;

[CreateAssetMenu(fileName = "RushingRoulette", menuName = "Tower", order = 0)]
public class TowerScriptableObject : ScriptableObject
{
    public new string name;
    public int cost;
    public GameObject towerModel;
    public Sprite icon;
    public int damage;
    public float range;
    public float turnSpeed;
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectilePrefab;
}
