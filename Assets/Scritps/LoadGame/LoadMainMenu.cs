using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] GameObject loaderCanvas;
    public static LoadMainMenu Instance;
    [SerializeField] Image progressBar;
    float _target;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    public async void LoadSceneMenu(int sceneID)
    {
        _target = 0;
        progressBar.fillAmount = 0;

        var scene = SceneManager.LoadSceneAsync(sceneID);
        Time.timeScale = 1;
        scene.allowSceneActivation = false;

        loaderCanvas.SetActive(true);

        do
        {
            await Task.Delay(100);
            _target = scene.progress;
        }
        while (scene.progress < 0.9f);

        await Task.Delay(1000);

        scene.allowSceneActivation = true;
    }

    void Update()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 3 * Time.deltaTime);
    }
}