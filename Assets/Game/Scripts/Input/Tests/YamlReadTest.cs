using UnityEngine;


namespace AnotherRTS.Management.RemappableInput.IO
{
    public class YamlReadTest : MonoBehaviour
    {
        [SerializeField]
        TextAsset YamlDocument;
        public void Start()
        {
            YamlControlSchemeReader s = new YamlControlSchemeReader();
            s.FromString(YamlDocument.text);
        }
    }
}
