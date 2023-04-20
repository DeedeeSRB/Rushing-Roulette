using UnityEngine;
using TMPro;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.grey;

    TextMeshPro label;
    Vector2Int coordinates;
    Tile tile;

    string prevName;
    
    void Awake()
    {
        tile = GetComponentInParent<Tile>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        if (Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }

        ToggleLabel();
        ColorCooridantes();
    }

    void ColorCooridantes()
    {
        if (tile.IsPlaceable)
            label.color = defaultColor;
        else
            label.color = blockedColor;
    }

    void ToggleLabel()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            label.enabled = !label.IsActive();
        }
    }

    void UpdateObjectName()
    {
        prevName = transform.parent.name;
        if (!prevName.Equals(coordinates.ToString()))
        {
            transform.parent.name = coordinates.ToString();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }
}
