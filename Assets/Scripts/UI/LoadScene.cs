using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;

        public void LoadSceneByName()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
