using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject _up;
    [SerializeField] private GameObject _down;
    [SerializeField] private GameObject _default;

    private GameObject _current;

    public void Show(Player player)
    {
        _up.SetActive(false);
        _down.SetActive(false);
        _default.SetActive(false);
        
        if (player.HasSpell<Jumper>())
        {
            ShowUp();
        }
        else if (player.HasSpell<CloudDestroyer>())
        {
            ShowDown();
        }
        else
        {
            ShowDefault();
        }
    }

    public void Hide()
    {
        StartCoroutine(SoftHide());
    }

    private void ShowUp()
    {
        _current = _up;
        _up.SetActive(true);
    }

    private void ShowDown()
    {
        _current = _down;
        _down.SetActive(true);
    }

    private void ShowDefault()
    {
        _current = _default;
        _default.SetActive(true);
    }

    private IEnumerator SoftHide()
    {
        foreach (var sprite in _current.GetComponents<SpriteRenderer>())
        {
            while (sprite.color.a > 0)
            {
                sprite.color -= new Color(0, 0, 0, 2f * Time.deltaTime);
                yield return null;
            }
        }
        
        _current.SetActive(false);
    }
}