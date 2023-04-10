using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    Bank bank;
    void Awake()
    {
        bank = FindObjectOfType<Bank>();
    }

    /// <summary>
    /// Trys to place a tower at the provided <c>Transform</c>.
    /// </summary>
    /// <param name="tileTransform">The <c>Transform</c> where the tower will be placed at.</param>
    /// <returns>A boolean, representing whether the tower could be built depending on the bank's current baklance.</returns>
    public bool TryPlaceTower(Transform tileTransform)
    {
        if (bank.CurrentBalance < TowerPicker.PickedTower.cost)
        {
            return false;
        }
        PlaceTower(tileTransform);
        bank.Withdraw(TowerPicker.PickedTower.cost);
        return true;
    }

    /// <summary>
    /// Places a tower at the provided <c>Transform</c>.
    /// </summary>
    /// <param name="tileTransform">The <c>Transform</c> where the tower will be placed at.</param>
    public void PlaceTower(Transform tileTransform)
    {
        GameObject tower = TowerPicker.PickedTower.tower;
        Vector3 spawnPos = new Vector3(tileTransform.position.x, tileTransform.position.y + 0.8f, tileTransform.position.z);
        Instantiate(tower, spawnPos, Quaternion.identity, tileTransform);
    }
}
