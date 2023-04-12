using UnityEngine;

public class TowerPicker : MonoBehaviour
{
    public static TowerInfo PickedTower { get; set; }

    [SerializeField] TowerInfo[] towers;

    int next = 1;

    void Start()
    {
        PickedTower = towers[0];
    }

    void Update()
    {
        CycleTowers();
    }

    public void PickTower(int tower)
    {
        PickedTower = towers[tower];
    }

    void CycleTowers()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (next == towers.Length)
                next = 0;
            PickedTower = towers[next++];
        }
    }
}
