using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class TowerPicker : MonoBehaviour
{
    public static GameObject PickedTower { get; set; }
    public static int PickedTowerCost { get; set; }

    [SerializeField] List<GameObject> _towers;

    int _next = 1;

    void Start()
    {
        PickedTower = _towers[0];
        PickedTowerCost = _towers[0].GetComponent<Tower>().Cost;
    }

    void Update()
    {
        CycleTowers();
    }

    public void PickTower(int tower)
    {
        PickedTower = _towers[tower];
        PickedTowerCost = _towers[tower].GetComponent<Tower>().Cost;
    }

    void CycleTowers()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_next == _towers.Count)
                _next = 0;
            PickedTower = _towers[_next];
            PickedTowerCost = _towers[_next].GetComponent<Tower>().Cost;
            _next++;
        }
    }
}
