using UnityEngine;

public static class MoneyStorage {
    
    private static int count;
    private static bool isInit;

    public delegate void MoneyDelegate(int val);
    public static MoneyDelegate MoneyChanged;
    
    public static int Count {
        get {
            if (!isInit) {
                count = PlayerPrefs.GetInt("Money", 0); 
                isInit = true;
            }
            return count;
        }
        set {
            count = value; 
            PlayerPrefs.SetInt("Money", value);
            if(MoneyChanged != null)
                MoneyChanged(value);
        }
    }
    
    

}
