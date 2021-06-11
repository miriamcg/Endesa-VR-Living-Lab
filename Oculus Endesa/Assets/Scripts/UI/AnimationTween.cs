using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class AnimationTween : MonoBehaviour
{
    public Image button;

    // Update is called once per frame
    void Update()
    {
        AnimationButtonIn();
        AnimationButtonOut();
    }

    private void AnimationButtonIn() {
        Tweener scale = button.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.1f).SetEase(Ease.InBounce).SetAutoKill(false);
        scale.OnComplete(() => {
            scale.PlayBackwards();
        });
    }

    private void AnimationButtonOut() {
        Tweener scale = button.transform.DOScale(new Vector3(1.25f, 1.25f, 1.25f), 0.1f).SetEase(Ease.OutBounce).SetAutoKill(false);
        scale.OnComplete(() => {
            scale.PlayBackwards();
        });
    }
}
