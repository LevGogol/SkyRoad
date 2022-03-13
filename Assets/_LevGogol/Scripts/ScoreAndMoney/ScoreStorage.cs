using UnityEngine;

public static class ScoreStorage {

    private static int maxCount;
    private static int count = 0;
    private static bool isInit;

    public delegate void ScoreDelegate(int val);
    public static ScoreDelegate ScoreChanged;
    public static bool maxChanged;
    
    public static int Count {
        get {
            return count;
        }
        set {
            count = value;
            if(ScoreChanged != null)
                ScoreChanged(value);
        }
    }

    public static int MaxCount {
        get { return PlayerPrefs.GetInt("MaxScore", 0); }
        set {
            maxChanged = false;
            if (value > MaxCount) {
                PlayerPrefs.SetInt("MaxScore", value);
                maxChanged = true;
            }
        }
    }



}