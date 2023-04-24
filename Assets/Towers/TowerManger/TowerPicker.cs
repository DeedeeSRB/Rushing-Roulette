using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class TowerPicker : MonoBehaviour
{
    [SerializeField] GameObject _towerPrefab;
    public static GameObject PickedTower { get; set; }
    public static int PickedTowerCost { get; set; }

    [SerializeField] List<TowerScriptableObject> _towers;

    int _next = 1;

    void Awake()
    {
        PickedTower = _towerPrefab;
        PickedTower.GetComponent<Tower>().towerSO = _towers[0];
        PickedTowerCost = _towers[0].cost;
    }

    // void Update()
    // {
    //     CycleTowers();
    // }

    public void PickTower(int tower)
    {
        PickedTower.GetComponent<Tower>().towerSO = _towers[tower];
        PickedTowerCost = _towers[tower].cost;
    }

    void CycleTowers()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (_next == _towers.Count)
                _next = 0;
            PickedTower.GetComponent<Tower>().towerSO = _towers[_next];
            PickedTowerCost = _towers[_next].cost;
            _next++;
        }
    }
}
