using System;
using UnityEngine;

public class AppMetricaImplemintation : MonoBehaviour
{
    [Serializable]
    private struct LevelStartAppMetricaEvent
    {
        public int level_count;
        public string skin_name;
    }

    [Serializable]
    private struct LevelFinishAppMetricaEvent
    {
        public int level_count;
        public string skin_name;
        public string result;
        public int time;
    }

    [SerializeField] private Level _level;
    [SerializeField] private Player _player;
    [SerializeField] private InputFacade _input;

    private float _startLevelTime;
    private bool _isStarted;

    private int LevelCount
    {
        get => PlayerPrefs.GetInt("LevelCount", 1);
        set => PlayerPrefs.SetInt("LevelCount", value);
    }

    private void Awake()
    {
        _level.Started += SendAppMetricStartEvent;
        _level.Failed += SendAppMetricFinishEvent;
        _input._UIButtons.MenuDowned += SendAppMetricFinishEventRestart;
    }

    public void SendAppMetricStartEvent()
    {
        var levelEvent = new LevelStartAppMetricaEvent
        {
            level_count = LevelCount, 
            skin_name = _player.SkinName
        };

        var json = JsonUtility.ToJson(levelEvent);
        AppMetrica.Instance.ReportEvent("level_start", json);
        AppMetrica.Instance.SendEventsBuffer();

        _startLevelTime = Time.unscaledTime;

        _isStarted = true;
    }

    public void SendAppMetricFinishEvent(string result)
    {
        if(_isStarted == false)
            return;
        
        var levelEvent = new LevelFinishAppMetricaEvent
        {
            level_count = LevelCount,
            skin_name = _player.SkinName,
            result = result,
            time = (int) (Time.unscaledTime - _startLevelTime)
        };

        var json = JsonUtility.ToJson(levelEvent);
        AppMetrica.Instance.ReportEvent("level_finish", json);
        AppMetrica.Instance.SendEventsBuffer();
        LevelCount++;
    }

    private void SendAppMetricFinishEventRestart()
    {
        SendAppMetricFinishEvent("leave");
    }

    private void SendAppMetricFinishEvent()
    {
        SendAppMetricFinishEvent("lose");
    }

    private void SendAppMetricFinishEventClosed()
    {
        SendAppMetricFinishEvent("game_closed");
    }

    private void OnApplicationQuit()
    {
        SendAppMetricFinishEventClosed();
    }
}