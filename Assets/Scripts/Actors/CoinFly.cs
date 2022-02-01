using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinFly : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMoveY(-0.25f, 2));
        sequence.Append(transform.DOLocalMoveY(0.25f, 2));
        sequence.SetLoops(-1);
    }
}
