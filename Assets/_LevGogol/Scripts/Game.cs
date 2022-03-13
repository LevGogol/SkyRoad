using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Screens _screens;
    [SerializeField] private InputFacade _input;
    [SerializeField] private Level _level;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _level.gameObject.SetActive(true);
        _level.OnFail += OnFail;
        _screens.Get<StartScreen>().Show();

        _input._UIButtons.OnPlayButton += EnableLevel;
        _input._UIButtons.OnShopButton += EnableShop;
    }

    private void EnableLevel()
    {
        _level.StartLevel();
        _screens.Get<StartScreen>().Hide();
    }

    private void EnableShop()
    {
        _screens.Get<StartScreen>().Hide();
        _screens.Get<LevelScreen>().Hide();
        _screens.Get<ShopScreen>().Show();
    }

    private void OnFail()
    {
        _screens.Get<StartScreen>().Show();

        _input.TouchDowned += () => SceneManager.LoadScene(0); //TODO make levelSystem
    }
}