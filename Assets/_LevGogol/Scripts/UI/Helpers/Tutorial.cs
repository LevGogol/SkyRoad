using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private float _hideSpeed;
    [SerializeField] private GameObject _up;
    [SerializeField] private GameObject _down;
    [SerializeField] private GameObject _default;

    public void Hide()
    {
        StartCoroutine(SoftHide());
    }

    public void ShowUp()
    {
        HideAll();
        _up.SetActive(true);
    }

    public void ShowDown()
    {
        HideAll();
        _down.SetActive(true);
    }

    public void ShowDefault()
    {
        HideAll();
        _default.SetActive(true);
    }

    private void HideAll()
    {
        _up.SetActive(false);
        _down.SetActive(false);
        _default.SetActive(false);
    }

    private IEnumerator SoftHide()
    {
        var alpha = 1f;
        while (alpha > 0)
        {
            foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
            {
                sprite.color -= new Color(0, 0, 0, _hideSpeed * Time.deltaTime);
                alpha = sprite.color.a;
                yield return null;
            }
        }

        gameObject.SetActive(false);
    }
}