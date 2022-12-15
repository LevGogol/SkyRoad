using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private Animation _animation;

    void Update()
    {
        _cursor.position = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            _animation.Play();
        }
    }
}
