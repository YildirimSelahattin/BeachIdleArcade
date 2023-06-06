using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    private void Start()
    {
        LoadMainMenu.Instance.LoadSceneMenu(1);
    }
}
