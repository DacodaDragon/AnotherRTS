using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RENAMETHIS
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
