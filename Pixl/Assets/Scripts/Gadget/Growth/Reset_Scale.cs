using System.Collections;
using UnityEngine;

public class Reset_Scale : MonoBehaviour
{
    private int speed = 5;
    private float divise = 1.2f;
    private float sleep = 10f;
    private void Start()
    {
        StartCoroutine(Smooth_scale(speed));
    }
    
    IEnumerator Smooth_scale(float time)
    {
        yield return new WaitForSeconds(time);
        float finish = 0;
        Vector3 to = transform.localScale / divise;
        while (finish < time)
        {
            finish += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, to, finish / time);
            yield return null;
        }
        transform.localScale = to;
        Destroy(this);
    }   
}
