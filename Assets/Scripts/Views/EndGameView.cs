using Controllers;
using UnityEngine;

namespace Views
{
    public class EndGameView : MonoBehaviour
    {
        public GameController ViewController;
        public void OnRestartButtonClick()
        {
            ViewController.StartGame();
        }
        public void OnMenuButtonClick()
        {
            ViewController.ExitToMenu();
        }
    }
}
