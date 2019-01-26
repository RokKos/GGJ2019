using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{

    public struct FlickeringLight {
        public Light light;
        public float time_on;
        public float time_off;

        public FlickeringLight(Light _light, float _time_on, float _time_off){
            light = _light;
            time_on = _time_on;
            time_off = _time_off;
        }
    }

    [Header("Lights")]
    [SerializeField] List<Light> lights;

    [Range(0f,10f)]
    [SerializeField] float minTime;
    [Range(0f, 10f)]
    [SerializeField] float maxTime;

    private List<FlickeringLight> flickeringLights;

    // Start is called before the first frame update
    void Start()
    {
        flickeringLights = new List<FlickeringLight>();

        foreach (Light light in lights) {
            FlickeringLight fl = new FlickeringLight(light, Random.Range(minTime, maxTime), Random.Range(minTime, maxTime));
            flickeringLights.Add(fl);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < flickeringLights.Count; ++i) {
            FlickeringLight light = flickeringLights[i];
            //Debug.Log(string.Format("Light Info: {0} time on: {1:F4} time off: {2:F4}", light.light.enabled, light.time_on, light.time_off));
            if (light.light.enabled) {
                light.time_on -= Time.deltaTime;
                if (light.time_on < 0) {
                    light.light.enabled = false;
                    light.time_on = Random.Range(minTime, maxTime);
                }

            } else {
                light.time_off -= Time.deltaTime;
                if (light.time_off < 0) {
                    light.light.enabled = true;
                    light.time_off = Random.Range(minTime, maxTime);
                }
            }
            //Debug.Log(string.Format("Light Info after: {0} time on: {1:F4} time off: {2:F4}", light.light.enabled, light.time_on, light.time_off));
            flickeringLights[i] = light;
        }

    }

}
