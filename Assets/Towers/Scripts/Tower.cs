using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerScriptableObject towerSO;
    [SerializeField] public bool alive;
    Tile _parentTile { get; set; }

    void Start()
    {
        _parentTile = GetComponentInParent<Tile>();
    }

    void OnMouseEnter()
    {
        _parentTile.SetSelected(true);
    }

    void OnMouseExit()
    {
        _parentTile.SetSelected(false);
    }
}
