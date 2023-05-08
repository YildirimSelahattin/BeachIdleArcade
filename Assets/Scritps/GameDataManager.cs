using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{   
    public static GameDataManager Instance;
    int totalMoney;
    public int TotalMoney
    {
        get
        {
            return totalMoney;
        }
        set
        {
            this.totalMoney = value;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.totalMoneyText.text = value.ToString() + " $";
                PlayerPrefs.SetInt("TotalMoney", totalMoney);
            }
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("TotalMoney", TotalMoney);
    }

    public void LoadData()
    {
        TotalMoney = PlayerPrefs.GetInt("TotalMoney", 0);
    }
}
