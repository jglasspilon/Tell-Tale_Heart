
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public DungeonCreator dungeon;
    public Camera camera;
    public CameraFade fader;
    public soundVisualizer visualizer;
    private AcidTrip.AcidTrip effect;

    public void Start() {
        effect = camera.GetComponent<AcidTrip.AcidTrip>();
    }

    public void Update()
    {
        if(fader.getAlpha() > 0)
        {
            visualizer.enabled = false;
        }

        else
        {
            visualizer.enabled = true;
        }
    }

	public void LevelComplete()
    {
        StartCoroutine(Regenerate());
    }

    private IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            LevelComplete();
        }
    }

    private IEnumerator Regenerate()
    {
        fader.BeginFade(1);
        Debug.Log(fader.getAlpha());
        while (fader.getAlpha() < 1)
        {   
            yield return null;
        }

        dungeon.numRooms.m_Min++;
        dungeon.numRooms.m_Max++;

        effect.Wavelength += 0.05f;
        effect.DistortionStrength += 0.5f;
        effect.SaturationSpeed += 0.5f;
        effect.SaturationBase++;

        dungeon.ReGenerateDungeon();

        yield return new WaitForSeconds(2);

        fader.BeginFade(-1);
    }
}
