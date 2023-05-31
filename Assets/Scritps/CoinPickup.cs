using DG.Tweening;
using TMPro;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine.UI;
using System.Collections;


public class CoinPickup : MonoBehaviour
{
    public static CoinPickup Instance;
    public int scoreIncrement = 10; // Amount to increment the score
    public float moveDuration = 1f; // Duration of the movement animation
    public Ease ease;
    private Vector3 targetPosition; // Target position for the money in UI space
    private bool isMoving = false; // Flag to track if the money is currently moving
    public GameObject Canvas;
    public Image imagePrefab;

    public int counter = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && !isMoving)
        {

            // Increase the score
            GameDataManager.Instance.TotalMoney += scoreIncrement;

            // Update the score display
            UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();

            Image image = Instantiate(imagePrefab, Canvas.transform.position, quaternion.identity, Canvas.transform.parent);
            Destroy(other.gameObject);


            // Animate and move the collected money

            image.transform.DOMove(UIManager.Instance.totalMoneyText.transform.position, moveDuration).SetEase(ease).OnComplete((() => Destroy(image.gameObject)));
        }
    }

    public IEnumerator UIMoneySpawner()
    {
        Image image = Instantiate(imagePrefab, Canvas.transform.position, quaternion.identity, Canvas.transform.parent);

        // Animate and move the collected money

        image.transform.DOMove(UIManager.Instance.totalMoneyText.transform.position, moveDuration).SetEase(ease).OnComplete(() =>
        {
            // Increase the score
            GameDataManager.Instance.TotalMoney += scoreIncrement;

            // Update the score display
            UIManager.Instance.totalMoneyText.text = GameDataManager.Instance.TotalMoney.ToString();

            Destroy(image.gameObject);
        });

        counter++;
        yield return new WaitForSeconds(0.1f);
        if (counter < 11)
        {
            StartCoroutine(UIMoneySpawner());
        }
    }



}
