using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StartExperience : MonoBehaviour
{

    [SerializeField] private GameObject cube;

    public void OnStartExperience(ARPlane plane)
    {
        var sceme = Instantiate(cube, plane.center, Quaternion.identity);

        for (int i=0; i< sceme.transform.childCount ; i++)
        {
            var child = sceme.transform.GetChild(i).gameObject;
            StartCoroutine(ScaleUp(child, 0.5f));
        }
    }

    private IEnumerator ScaleUp(GameObject obj, float duration)
    {
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = obj.transform.localScale;

        float elapsedTime = 0f;

        obj.transform.localScale = initialScale;

        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
