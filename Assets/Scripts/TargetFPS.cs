using UnityEngine;

namespace Camera
{
    /// <summary>
    /// Sets target fps to higher than 60, otherwise unity will sometimes update at 30 or 15fps 
    /// </summary>
    public class TargetFPS : MonoBehaviour
    {
        void Awake()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 120;
        }
    }
}
