using UnityEngine;
using System.Collections;

public class soundVisualizer : MonoBehaviour
{
    public int detail = 500;
    public float amplitude = 0.1f;
    private Light targetLight;
    public GameObject heart;

    // Use this for initialization
    void Start()
    {
        targetLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update() {
	    var info = new float[detail];

        //Can't put this into Start because the instance of Heart does not exist yet so it cannot find it
        if(heart == null)
            heart = GameObject.FindGameObjectsWithTag("Heart")[0];

        //Get sound info from the heart only
        heart.GetComponent<AudioSource>().GetOutputData(info, 0); 

        var packagedData = 0.0f;
        for(int x = 0; x < info.Length; x++)
        {
            packagedData += System.Math.Abs(info[x]);
        }

        targetLight.intensity = 1.0f * packagedData * amplitude;
	}
}
