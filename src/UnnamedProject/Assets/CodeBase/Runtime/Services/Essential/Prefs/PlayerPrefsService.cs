using System.Collections;
using UnityEngine;

namespace Services.Essential.Prefs
{
    public class PlayerPrefsService : IPrefsService
    {
        private const int SaveFrequency = 2;
        
        private int _version = 1;
        
        public PlayerPrefsService(ICoroutineRunner coroutineRunner)
        {
            coroutineRunner.StartCoroutine(SaveOverTime());
        }

        private IEnumerator SaveOverTime()
        {
            int currentVersion = _version;
            while (true)
            {
                yield return new WaitForSeconds(SaveFrequency);
                if (_version != currentVersion)
                {
                    PlayerPrefs.Save();
                    currentVersion = _version;
                }
            }
        }
        
        public int GetInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }

        public void SetInt(string key, int value)
        {
            _version++;
            PlayerPrefs.SetInt(key, value);
        }

        public bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public string GetString(string key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }

        public void SetString(string key, string value)
        {
            _version++;
            PlayerPrefs.SetString(key, value);
        }
    }
}