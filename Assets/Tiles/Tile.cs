using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject tower;
    [SerializeField] Color hoverColor;
    [SerializeField] Mesh placedMesh;

    [SerializeField] bool isPlacable;
    public bool IsPlaceable { get { return isPlacable; } }

    GameObject selectionBoarder;
    GameObject towerObjectPool;

    Renderer rend;
    Color originalColor;

    float t = 0;
    bool hovering = false;

    TowerBuilder towerBuilder;

    void Awake()
    {
        if (isPlacable) selectionBoarder = gameObject.transform.GetChild(0).gameObject;
        towerObjectPool = GameObject.Find("TowerObjectPool");
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        towerBuilder = FindObjectOfType<TowerBuilder>();
    }

    IEnumerator ColorTile()
    {
        t = 0;
        while (hovering && isPlacable)
        {
            rend.material.color = Color.Lerp(originalColor, hoverColor, t);
            if (t < 1)
                t += Time.deltaTime / 0.2f;
            yield return new WaitForEndOfFrame();
        }
    }

    void OnMouseEnter()
    {
        SetSelection(true);
        StartCoroutine(ColorTile());
    }

    void OnMouseExit()
    {
        SetSelection(false);
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        if (isPlacable)
        {
            if (towerBuilder.TryPlaceTower(transform))
            {
                PlaceTower();
            }
            else
            {
                // TODO: Inform user of not enough money in bank
            }
        }
    }

    void PlaceTower()
    {
        isPlacable = hovering = false;
        rend.material.color = originalColor;
        selectionBoarder.GetComponent<MeshFilter>().mesh = placedMesh;
    }

    public void SetSelection(bool active)
    {
        hovering = active;
        if (selectionBoarder != null) selectionBoarder.SetActive(active);
    }
}
