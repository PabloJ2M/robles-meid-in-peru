namespace UnityEngine.SceneManagement
{
    [AddComponentMenu("System/SceneManagement/SceneLoader")]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] protected string _scenePath;
        public string ScenePath { get => _scenePath; set => _scenePath = value; }


        [ContextMenu("Cut Scene")]
        public void CutScene() => SceneController.Instance?.CutScene(_scenePath);

        [ContextMenu("Add Scene")]
        public void AddScene() => StartCoroutine(SceneController.Instance?.AddScene(_scenePath));

        [ContextMenu("Swipe Scene")]
        public void SwipeScene() => SceneController.Instance?.SwipeScene(_scenePath);

        [ContextMenu("Remove Scene")]
        public void RemoveScene() => SceneController.Instance?.RemoveScene(_scenePath);
    }
}