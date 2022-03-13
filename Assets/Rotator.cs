using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rotator : MonoBehaviour, IPointerDownHandler
{
    private Tween _tween;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_tween == null)
        {
            _tween = transform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetRelative(true)
                .SetEase(Ease.Linear).OnComplete(() => _tween = null);
        }
    }
}
