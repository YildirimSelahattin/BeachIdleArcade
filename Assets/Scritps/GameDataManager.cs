using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{   
    public static GameDataManager Instance;
    float totalMoney;
    public float TotalMoney
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
                PlayerPrefs.SetFloat("TotalMoney", totalMoney);
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
        PlayerPrefs.SetFloat("TotalMoney", TotalMoney);
    }

    public void LoadData()
    {
        TotalMoney = PlayerPrefs.GetFloat("TotalMoney", 80);
    }
}
