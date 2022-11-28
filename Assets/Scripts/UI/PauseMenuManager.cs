using States;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Class that manages the pause menu UI and the pausing of game time.
    /// </summary>
    public class PauseMenuManager : MonoBehaviour
    {
        /// <summary>
        /// The UI object that will have its activity toggled.
        /// </summary>
        [SerializeField] private GameObject pauseMenu;

        /// <summary>
        /// The current state of the game, useful in determining if the game should be paused or not.
        /// TODO: Replace this with a game state manager singleton that handles time and state logic.
        /// </summary>
        public GameState currentGameState = GameState.Running;

        /// <summary>
        /// The button assosiated with pausing, based on the built-in input manager.
        /// </summary>
        public string pauseButtonInput = "Cancel";

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Start() => pauseMenu.SetActive(false);

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetButtonDown(pauseButtonInput))
            {
                TogglePauseState();
            }
        }

        /// <summary>
        /// Toggle the pause state based on the current game state.
        /// </summary>
        private void TogglePauseState()
        {
            switch (currentGameState)
            {
                case GameState.Running:
                    EnterPauseState();
                    break;

                case GameState.Paused:
                    ExitPauseState();
                    break;

                default:
                    ExitPauseState();
                    break;
            }
        }

        /// <summary>
        /// Pauses game time and toggles pause menu on.
        /// </summary>
        public void EnterPauseState()
        {
            currentGameState = GameState.Paused;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        /// <summary>
        /// Resumes game time and toggles pause menu off.
        /// </summary>
        public void ExitPauseState()
        {
            currentGameState = GameState.Running;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}