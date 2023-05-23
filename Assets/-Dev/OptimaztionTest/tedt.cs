using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tedt : MonoBehaviour
{
    public GameObject girls;

    public void OnOpenWomen()
    {
        girls.SetActive(true);
    }

    public void OnCloseWomen()
    {
        girls.SetActive(false);
    }
}
