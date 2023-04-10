using UnityEngine;

public class Tower : MonoBehaviour
{
    Tile parentTile;
    void Start() {
        parentTile = GetComponentInParent<Tile>();
    }

    void OnMouseEnter()
    {
        parentTile.SetSelection(true);
    }

    void OnMouseExit()
    {
        parentTile.SetSelection(false);
    }
}
