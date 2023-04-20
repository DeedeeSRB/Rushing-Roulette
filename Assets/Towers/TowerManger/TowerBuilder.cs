using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    Bank bank;
    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    /// <summary>
    /// Trys to place a tower at the provided <c>Tile</c>.
    /// </summary>
    /// <param name="tile">The <c>Tile</c> where the tower will be placed at.</param>
    /// <returns>A boolean, representing whether the tower could be built depending on the tile's status and bank's current baklance.</returns>
    public bool TryPlaceTower(Tile tile)
    {
        if (tile.IsOccupied) return false;
        // TODO: Inform user of the tile being occupied

        Debug.Log($"Current balance: {bank.CurrentBalance}, Tower cost: {TowerPicker.PickedTowerCost}");
        if (bank.CurrentBalance < TowerPicker.PickedTowerCost) return false;
        // TODO: Inform user of not enough money in bank   

        PlaceTower(tile);
        bank.Withdraw(TowerPicker.PickedTowerCost);
        return true;
    }

    /// <summary>
    /// Places a tower at the provided <c>Tile</c>.
    /// </summary>
    /// <param name="tile">The <c>Tile</c> where the tower will be placed at.</param>
    public void PlaceTower(Tile tile)
    {
        tile.IsOccupied = true;
        tile.hovering = false;
        tile.rend.material.color = tile.originalColor;
        tile.selectionBoarder.GetComponent<MeshFilter>().mesh = tile.placedMesh;

        GameObject tower = TowerPicker.PickedTower;
        Vector3 spawnPos = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.8f, tile.transform.position.z);
        Instantiate(tower, spawnPos, Quaternion.identity, tile.transform);
    }
}
