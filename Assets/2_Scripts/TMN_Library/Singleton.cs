using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static volatile T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = FindObjectOfType(typeof(T)) as T;
#if UNITY_EDITOR
            if (_instance == null) SceneManager.LoadScene(1);
#endif
            DontDestroyOnLoad(_instance);
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance != null) return;
        _instance = this as T;
        DontDestroyOnLoad(gameObject);
    }
}