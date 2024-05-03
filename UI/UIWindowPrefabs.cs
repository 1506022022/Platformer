using UnityEngine;

[CreateAssetMenu(fileName = "UIWindows", menuName = "Custom/UIWindows")]
public class UIWindowPrefabs : ScriptableObject
{
    [SerializeField] LoadingWindow mLoadingWindowPrefab;
    public LoadingWindow LoadingWindowPrefab => mLoadingWindowPrefab;
}

public static class UIWindowContainer
{
    static UIWindowPrefabs mPrefabs = Resources.Load<UIWindowPrefabs>("UIWindows");
    static LoadingWindow mLoadingWindowInstance;
    public static LoadingWindow GetLoadingWindow()
    {
        Debug.Assert(mPrefabs);
        if (mLoadingWindowInstance == null)
        {
            mLoadingWindowInstance = GameObject.Instantiate(mPrefabs.LoadingWindowPrefab);
        }
        return mLoadingWindowInstance;
    }
}