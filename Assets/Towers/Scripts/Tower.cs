using UnityEngine;

public class Tower : MonoBehaviour, IBuyable, IPlaceable
{
    [SerializeField][ReadOnly] public bool alive = true;
    [field: SerializeField] public int Cost { get; set; } = 100;

    public Tile ParentTile { get; set; }

    void Start()
    {
        ParentTile = GetComponentInParent<Tile>();
    }

    void OnMouseEnter()
    {
        ParentTile.SetSelected(true);
    }

    void OnMouseExit()
    {
        ParentTile.SetSelected(false);
    }
}
