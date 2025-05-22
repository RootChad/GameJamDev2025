using UnityEngine;

namespace Ded
{
    [RequireComponent(typeof(Renderer))]
    public class EmissionOscillator : MonoBehaviour
    {
        [Header("Emission Settings")]
        [ColorUsage(true, true)]
        public Color baseEmissionColor = Color.white;

        [Header("Intensity Range")]
        [Min(0f)] public float minIntensity = 0f;
        [Min(0f)] public float maxIntensity = 1f;

        [Tooltip("Speed of oscillation")]
        public float speed = 1f;

        private Renderer rdr;
        private Material materialInstance;

        private void Start()
        {
            // Get a unique material instance (avoid changing the shared one)
            rdr = GetComponent<Renderer>();
            materialInstance = rdr.material;

            // Enable emission keyword (required in URP)
            materialInstance.EnableKeyword("_EMISSION");
        }

        private void Update()
        {
            // Oscillate between min and max intensity
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * speed, 1f));
            float factor = Mathf.Pow(2, intensity);
            // Apply emission with HDR intensity
            Color emission = baseEmissionColor * intensity;

            materialInstance.SetColor("_EmissionColor", emission);
        }
    }
}
