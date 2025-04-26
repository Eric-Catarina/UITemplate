using UnityEngine;
using System.Collections;


public class EmissionController : MonoBehaviour
{
    [SerializeField] private float intensityMultiplier = 1.0f;
    [SerializeField] private bool increaseIntensityOnCollision = false;
    public bool hasEmission;
    float baseIntensity;

    private static readonly int EmissiveColorID = Shader.PropertyToID("_EmissionColor");
    [SerializeField]
    private Material materialInstance;
    public Color initialEmissiveColor;
    [SerializeField]
    private Renderer myRenderer;

    private void Start()
    {
        if (hasEmission)
        {
            TryGetComponent<Renderer>(out myRenderer);

            materialInstance = myRenderer.material;

            initialEmissiveColor = materialInstance.GetColor(EmissiveColorID);
            baseIntensity = initialEmissiveColor.r;
        }
    }

    public void SetIntensity(float intensity)
    {
        intensityMultiplier = intensity;
        UpdateEmission();
    }

    private void UpdateEmission()
    {
        if (hasEmission){
            Color newEmissiveColor = GetColor() * intensityMultiplier;
            myRenderer.material.SetColor(EmissiveColorID, newEmissiveColor);
        }
    }

    public IEnumerator FlashCoroutine(float intensity, Color color = default)
    {
        float flashDuration = 1f;
        float timer = 0f;

        while (timer < flashDuration)
        {
            float lerpValue = Mathf.Lerp(1f, intensity, timer / flashDuration);
            materialInstance.SetColor("_EmissionColor", color * lerpValue * intensity);
            timer += Time.deltaTime;
            yield return null;
        }

        materialInstance.SetColor("_EmissionColor", initialEmissiveColor);
    }

    public void Flash(float intensity, Color color = default)
    {
        StartCoroutine(FlashCoroutine(intensity, color));
    }
    public void SetColor(Color color)
    {
        materialInstance.SetColor("_EmissionColor", color);
    }
    public void SetColorAndIntensity(Color color,float intensity)
    {
        materialInstance.SetColor("_EmissionColor", color * intensity);
    }
    public Color GetColor(){
        return myRenderer.material.GetColor("_EmissionColor");
    }
}
