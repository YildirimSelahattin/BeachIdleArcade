using DG.Tweening;
using TMPro;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine.UI;

public class CoinPickup : MonoBehaviour
{
    public int scoreIncrement = 10; // Amount to increment the score
    public float moveDuration = 1f; // Duration of the movement animation
    public Ease ease;
    
   
    
    private Vector3 targetPosition; // Target position for the money in UI space
    
    private bool isMoving = false; // Flag to track if the money is currently moving
    public GameObject Canvas;
    public Image imagePrefab;

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin") && !isMoving)
        {
            
            // Increase the score
            GameDataManager.Instance.TotalMoney += scoreIncrement;

            // Update the score display
            UIManager.Instance.totalMoneyText.text =  GameDataManager.Instance.TotalMoney.ToString();

            Image image = Instantiate(imagePrefab,Canvas.transform.position,quaternion.identity,Canvas.transform.parent);
            Destroy(other.gameObject);
           
          
            // Animate and move the collected money
            
            image.transform.DOMove(UIManager.Instance.totalMoneyText.transform.position, moveDuration).SetEase(ease).OnComplete((() => Destroy(image.gameObject)));
            
            
            
        }
    }

    
}
