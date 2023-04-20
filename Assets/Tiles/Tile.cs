using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour, IPlot
{
    [field: SerializeField] public bool IsOccupied { get; set; }

    [SerializeField] Color _hoverColor;
    [SerializeField] public Mesh placedMesh;
    [HideInInspector] public Transform selectionBoarder;

    [HideInInspector] public Renderer rend;
    [HideInInspector] public bool hovering = false;
    [HideInInspector] public Color originalColor;
    float _colorPct = 0;

    TowerBuilder _towerBuilder;

    void Awake()
    {
        selectionBoarder = transform.Find("SelectionBoarder");
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        _towerBuilder = FindObjectOfType<TowerBuilder>();
    }

    IEnumerator ColorTile()
    {
        _colorPct = 0;
        while (hovering && !IsOccupied)
        {
            rend.material.color = Color.Lerp(originalColor, _hoverColor, _colorPct);
            if (_colorPct < 1)
                _colorPct += Time.deltaTime / 0.2f;
            yield return new WaitForEndOfFrame();
        }
    }

    void OnMouseEnter()
    {
        SetSelected(true);
        StartCoroutine(ColorTile());
    }

    void OnMouseExit()
    {
        SetSelected(false);
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        TryPlaceTower();
    }

    public void TryPlaceTower()
    {
        _towerBuilder.TryPlaceTower(this);
    }

    public void SetSelected(bool active)
    {
        hovering = active;
        if (selectionBoarder != null) selectionBoarder.gameObject.SetActive(active);
    }
}
