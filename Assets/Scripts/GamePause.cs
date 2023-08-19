using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    public void OnPause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            LevelManager.Instance.Pause();
        }
        else
        {
            LevelManager.Instance.Resume();
        }
    }

    

}
