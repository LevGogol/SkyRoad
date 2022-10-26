using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SwipeContent : MonoBehaviour
{
    [SerializeField] private float _maxDeltaToSwipe;
    [SerializeField] private float _swipeDuration;
    [SerializeField] private Color _enabledPage;
    [SerializeField] private Color _disabledPage;
    [SerializeField] private float _tintTime;
    [SerializeField] private RectTransform _content;
    [SerializeField] private Image[] _pages;
    [SerializeField] private float _offsetToSwipe;

    private int _currentPage;
    private bool _lock;
    private Vector3 _startTouch;
    private Vector3 _startContent;
    private Vector3 _delta;
    private const int _charactersOnPage = 9;

    public void Initialize(int selectedCharacterID)
    {
        _currentPage = selectedCharacterID / _charactersOnPage;

        for (int i = 0; i < _pages.Length; i++)
        {
            if (_currentPage == i)
            {
                _pages[i].color = _enabledPage;
            }
            else
            {
                _pages[i].color = _disabledPage;
            }
        }

        _content.anchoredPosition = new Vector2(_currentPage * _offsetToSwipe, _content.anchoredPosition.y);
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startTouch = Input.mousePosition;
            _startContent = _content.position;
        }

        if (_lock)
            return;

        if (Input.GetMouseButton(0))
        {
            _delta = Input.mousePosition - _startTouch;
            _content.position = new Vector3(_startContent.x + _delta.x, _content.position.y, _content.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_delta.x > _maxDeltaToSwipe)
            {
                SwipeLeft();
            }
            else if (_delta.x < -_maxDeltaToSwipe)
            {
                SwipeRight();
            }
            else
            {
                ReturnToBegin();
            }
        }
    }

    private void ReturnToBegin()
    {
        _content.DOAnchorPosX(_currentPage * _offsetToSwipe, 0.2f);
    }

    private void SwipeRight()
    {
        if (_currentPage >= _pages.Length - 1)
        {
            ReturnToBegin();
            return;
        }

        _currentPage++;
        _lock = true;
        _content.DOAnchorPosX(_currentPage * _offsetToSwipe, _swipeDuration).OnComplete(() => { _lock = false;});
        _pages[_currentPage - 1].DOColor(_disabledPage, _tintTime);
        _pages[_currentPage].DOColor(_enabledPage, _tintTime);
    }

    private void SwipeLeft()
    {
        if (_currentPage <= 0)
        {
            ReturnToBegin();
            return;
        }

        _currentPage--;
        _lock = true;
        _content.DOAnchorPosX(_currentPage * _offsetToSwipe, _swipeDuration).OnComplete(() => { _lock = false;});
        _pages[_currentPage + 1].DOColor(_disabledPage, _tintTime);
        _pages[_currentPage].DOColor(_enabledPage, _tintTime);
    }
}