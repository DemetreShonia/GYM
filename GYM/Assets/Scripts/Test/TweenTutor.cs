using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTutor : MonoBehaviour
{
    void Start()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOScale(50, 2f).SetEase(Ease.InSine).SetLoops(2, LoopType.Yoyo));
        seq.Insert(2f,transform.DOMove(transform.position + Vector3.right * 10f, 1f));

      //  seq.OnComplete
    }
}
