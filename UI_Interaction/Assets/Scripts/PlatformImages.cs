using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformImages : MonoBehaviour
{
    public RawImage platformImage;
    public Texture androidTexture;
    public Texture WindowsTexture;

    void Start()
    {
    #if UNITY_ANDROID
            platformImage.texture = androidTexture;
    #elif UNITY_STANDALONE
            platformImage.texture = WindowsTexture;
    #endif
    }
}
