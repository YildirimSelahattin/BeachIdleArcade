using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI totalMoneyText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();
    }
}
