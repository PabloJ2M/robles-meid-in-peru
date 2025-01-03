using UnityEngine;

public class ScreenShoot : MonoBehaviour
{
    [SerializeField] private string path = "Assets/ScreenCapture/Images/";
    [SerializeField] private string captureName = "ScreenShoot";
    [Space]
    [SerializeField] private int size = 1;
    [SerializeField] private int index = 0;

    #region Singleton
    public static ScreenShoot screenShoot;

    private void Awake()
    {
        if(screenShoot == null)
        {
            screenShoot = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [ContextMenu("Take Screen Shoot")]
    public void TakeScreenShoot()
    {
        ScreenCapture.CaptureScreenshot(path + captureName + $"_{index}.png", size);
        index++;
    }
}