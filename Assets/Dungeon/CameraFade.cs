using UnityEngine;
using System.Collections;

public class CameraFade: MonoBehaviour
{
    //texture to use in fade in and out
    public float fadeSpeed = 0.8f;
    public float blackSlugSpeed = 10.0f;
    float fadeSpeedMemory;

    //sets the drawing layer to the top
    //private int drawDepth = -1000;
    private float alpha;

    //fade in: -1  fade out: 1
    private int fadeDirection = -1;

    public float preLoadTimer = 5.0f;
    public float exitTimer = 2.0f;
    public int levelIndex = 0;

    private bool fadingToNextScene = false;
    public bool preLoad = false;

    // Use this for initialization
    void Start()
    {
        //speeds up to prepare the particle systems
        if (preLoad)
        {
            Time.timeScale = 10f;
        }
        alpha = GetComponent<Renderer>().material.color.a;
        fadeSpeedMemory = fadeSpeed;
    }

    //affects GUI elements
    void Update()
    {
        if (preLoadTimer > 0)
        {
            preLoadTimer = preLoadTimer - Time.deltaTime;
        }

        else
        {
            if (preLoad)
            {
                Time.timeScale = 1.0f;
                preLoad = false;
            }

            if (!fadingToNextScene)
            {
                //reduce the transparency according to the speed and reset the speed of game
                alpha += fadeDirection * fadeSpeed * Time.deltaTime;
            }
            else
            {
                //reduce the transparency according to the speed and reset the speed of game
                alpha += fadeDirection * blackSlugSpeed * Time.deltaTime;
            }

            alpha = Mathf.Clamp01(alpha);

            this.GetComponent<Renderer>().material.color = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, alpha);

            
        }
    }


    public void BeginFade(int direction)
    {
        fadeDirection = direction;
    }

    void OnLevelWasLoaded()
    {
        BeginFade(-1);
    }

    public void fadeOutOfLevel()
    {
        BeginFade(1);
        fadingToNextScene = true;
    }

    public float getAlpha()
    {
        return alpha;
    }
}
