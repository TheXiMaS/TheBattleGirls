using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimeCycle : MonoBehaviour
{
    [SerializeField] private float cycleSpeed;
    [SerializeField] private Color dayColor = new Color(115,195,210);
    [SerializeField] private Color nightColor = new Color(15,15,15);

    private UniversalRenderPipelineAsset _urpAsset;
    private float _initialSunIntensity;

    private void Awake()
    {
        _urpAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
        //_initialSunIntensity = _urpAsset.additionalLights[0].intensity;
    }

    private void Update()
    {
        float time = Mathf.PingPong(cycleSpeed * Time.time, 1f);
        RenderSettings.ambientLight = Color.Lerp(nightColor, dayColor, time);
        //_urpAsset.additionalLights[0].intensity = _initialSunIntensity * (1 - time);
    }
    
}
