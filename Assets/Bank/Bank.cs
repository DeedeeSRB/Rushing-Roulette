using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int initialBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    [SerializeField] TextMeshProUGUI textMeshPro;

    void Awake()
    {
        currentBalance = initialBalance;
    }
//yes
    public void Deposit(int amount) {
        currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }
    
    public void Withdraw(int amount) {
        currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        textMeshPro.text = "Moone " + currentBalance;
    }

}
