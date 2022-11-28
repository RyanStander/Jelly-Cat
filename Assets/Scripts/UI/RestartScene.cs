using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartScene : MonoBehaviour
    {
        public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}