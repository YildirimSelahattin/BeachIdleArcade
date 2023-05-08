using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSunbedAtt : MonoBehaviour, ISunbed
{
    public static NewSunbedAtt Instance;

    [SerializeField]
    float price;
    public float Price
    {
        get =>
            price;
        set =>
            price = value;
    }

    [SerializeField]
    float givenMoney;
    public float GivenMoney
    {
        get =>
            givenMoney;
        set =>
            givenMoney = value;
    }

    [SerializeField]
    float fillAmount;
    public float FillAmount
    {
        get =>
            fillAmount;
        set =>
            fillAmount = value;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ITrigger()
    {

    }
}
