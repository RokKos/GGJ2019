using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsController : MonoBehaviour
{

    public struct FlickeringLight {
        public GameObject light;
        public float time_on;
        public float time_off;

        public FlickeringLight(GameObject _light, float _time_on, float _time_off){
            light = _light;
            time_on = _time_on;
            time_off = _time_off;
        }
    }

    [Header("Lights")]
    [SerializeField] List<GameObject> lights;

    private List<FlickeringLight> flickeringLights;

    // Start is called before the first frame update
    void Start()
    {
        flickeringLights = new List<FlickeringLight>();

        foreach (GameObject light in lights) {
            FlickeringLight fl = new FlickeringLight(light, Random.Range(0.25f, 1.5f), Random.Range(0.25f, 2));
            flickeringLights.Add(fl);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < flickeringLights.Count; ++i) {
            FlickeringLight light = flickeringLights[i];
            Debug.Log(string.Format("Light Info: {0} time on: {1:F4} time off: {2:F4}", light.light.activeSelf, light.time_on, light.time_off));
            if (light.light.activeSelf) {
                light.time_on -= Time.deltaTime;
                if (light.time_on < 0) {
                    light.light.SetActive(false);
                    light.time_on = Random.Range(0.25f, 1.5f);
                }

            } else {
                light.time_off -= Time.deltaTime;
                if (light.time_off < 0) {
                    light.light.SetActive(true);
                    light.time_off = Random.Range(0.5f, 2);
                }
            }
            Debug.Log(string.Format("Light Info after: {0} time on: {1:F4} time off: {2:F4}", light.light.activeSelf, light.time_on, light.time_off));
            flickeringLights[i] = light;
        }

    }

}
