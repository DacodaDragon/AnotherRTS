using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RENAMETHIS
{
    /// <summary>
    /// This class runs the second scene in the build settings (index 1) at Start.
    /// </summary>
    public class Preloader : MonoBehaviour
    {
        void Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
