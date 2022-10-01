using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UIButtons))]
public class InputFacade : MonoBehaviour
{
    public UIButtons _UIButtons; //TODO WTF? _???

    [SerializeField] private float _swipeDuration;
    [SerializeField] private float _swipeDistance = 75f;
    
    public event UnityAction SwipeUped;
    public event UnityAction SwipeDowned;
    public event UnityAction TouchDowned;

    private float _toucheTime;
    private Vector2 _startPosition;
    private Vector2 _endPosition;

    private void Awake()
    {
        _UIButtons = GetComponent<UIButtons>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = (Input.mousePosition);
            _endPosition = _startPosition;
            TouchDowned?.Invoke();
        }
        else if (Input.GetMouseButton(0))
        {
            _toucheTime += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _endPosition = (Input.mousePosition);

            if (_startPosition.y < _endPosition.y - _swipeDistance && _toucheTime < _swipeDuration)
            {
                SwipeUped?.Invoke();
            }
            else if (_startPosition.y > _endPosition.y + _swipeDistance && _toucheTime < _swipeDuration)
            {
                SwipeDowned?.Invoke();
            }

            _toucheTime = 0;
        }
    }
}