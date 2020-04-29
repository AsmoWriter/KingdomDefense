using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

namespace Views
{
    public class MenuView : MonoBehaviour
    {
        public GameController ViewController;
        public void OnStartButtonClick()
        {
            ViewController.StartGame();
        }
    }
}
