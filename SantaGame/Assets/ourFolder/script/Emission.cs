using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emission : MonoBehaviour
{
    private Material material; // 메테리얼 선언
    private float intensity_value = 0;
    public float max_intensity_value;
    public float speed_intensity_value;
    private bool is_corutine = false;

    public bool SocksHit = false;
    Color oldColor;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material; // 렌더러 컴포넌트의 메테리얼과 연결

        oldColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(SocksHit == false)
        {
            if (is_corutine == false)
            {
                is_corutine = true;
                StartCoroutine(emission());
                //이거함?
                //저거함?
            } 
        }
    }


    public void SetColorToOld()
    {
        material.SetColor("_EmissionColor", oldColor);
    }



    IEnumerator emission()
    {
        if(SocksHit)
        {
            SetColorToOld();

            yield return null;
        }

        for (intensity_value = 0; intensity_value <= max_intensity_value; intensity_value += speed_intensity_value/100)
        {
            material.SetColor("_EmissionColor", Color.grey * intensity_value);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.01f);

        for (intensity_value = max_intensity_value; intensity_value >= 0f; intensity_value -= speed_intensity_value/100)
        {
            material.SetColor("_EmissionColor", Color.grey * intensity_value);
            yield return new WaitForSeconds(0.05f);
        }

        is_corutine = false;
        yield return null;
    }
}
