using UnityEngine;

public class SetFullscreen : MonoBehaviour
{
    private bool isFullscreen = false;
    
    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        
        if(isFullscreen)
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        else
            Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
