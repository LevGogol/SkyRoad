using UnityEngine;

public class PauseScreen : ScreenBase
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}
