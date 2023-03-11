using UnityEngine;

namespace Scripts.SaveSystem
{
    public static class SaveSystem
    {
        const string DEFAULT_APP_KEY = "com.mauxin.recyclecannon_";
        const string ERROR_MESSAGE = "ERROR GETTING KEY:";

        public static void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(PrefsKey(key), value);
            PlayerPrefs.Save();
        } 

        public static string GetSavedString(string key)
        {
            if (!PlayerPrefs.HasKey(PrefsKey(key)))
            {
                Debug.LogError(ERROR_MESSAGE + key);
                return null;
            }

            string result = PlayerPrefs.GetString(PrefsKey(key), ERROR_MESSAGE);

            if (result == ERROR_MESSAGE) {
                Debug.LogError(ERROR_MESSAGE+key);
                return null;
            }

            return result;
        }

        public static void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(PrefsKey(key), value);
            PlayerPrefs.Save();
        }

        public static int GetSavedInt(string key)
        {
            if (!PlayerPrefs.HasKey(PrefsKey(key)))
            {
                Debug.LogError(ERROR_MESSAGE + key);
                return 0;
            }

            return PlayerPrefs.GetInt(PrefsKey(key), 0);
        }

        public static void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetInt(PrefsKey(key), value ? 1 : 0);
            PlayerPrefs.Save();
        }

        public static bool GetSavedBool(string key)
        {
            if (!PlayerPrefs.HasKey(PrefsKey(key))) return false;

            return PlayerPrefs.GetInt(PrefsKey(key), 0) == 1;
        }

        static string PrefsKey(string key)
        {
            return DEFAULT_APP_KEY + key;
        }

    }
}
