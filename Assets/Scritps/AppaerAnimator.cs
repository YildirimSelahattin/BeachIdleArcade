using UnityEngine;
using DG.Tweening;

public class AppaerAnimator : MonoBehaviour
{
    public float scale;

    void Start()
    {
      gameObject.transform.DOScale(scale, 1f).SetEase(Ease.OutElastic);
    }
}
