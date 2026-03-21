using UnityEngine;

public class LightingScenarrio : MonoBehaviour
{
    public Material gradientSkybox;

    // Update is called once per frame
    private void Start()
    {

        Stage3();
        
    }
    void Stage1()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        RenderSettings.ambientSkyColor = new Color(0.915f, 0.898f, 0.894f, 1.000f);
        RenderSettings.ambientEquatorColor = new Color(0.566f, 0.159f, 0.120f, 1.000f);
        RenderSettings.ambientGroundColor = new Color(0.858f, 0.400f, 0.239f, 1.000f);

        DynamicGI.UpdateEnvironment();



    }
    void Stage2()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        RenderSettings.ambientSkyColor = new Color(0.514f, 0.656f, 1.000f, 1.000f);
        RenderSettings.ambientEquatorColor = new Color(0.429f, 0.696f, 0.849f, 1.000f);
        RenderSettings.ambientGroundColor = new Color(0.118f, 0.158f, 0.189f, 1.000f);

        DynamicGI.UpdateEnvironment();
    }
    void Stage3()
    {
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;

        RenderSettings.ambientSkyColor = new Color(0.807f, 0.479f, 0.847f, 1.0f);
        RenderSettings.ambientEquatorColor = new Color(0.301f, 0.087f, 0.209f, 1.0f);
        RenderSettings.ambientGroundColor = new Color(0.708f, 0.546f, 0.871f, 1.0f);

        DynamicGI.UpdateEnvironment();
    }
}
