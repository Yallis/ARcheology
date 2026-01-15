using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DigPointerController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] artifacts;

    private void Awake()
    {
        if (artifacts[0] != null)
            FindNextArtifact(artifacts[0]);
    }

    private void Update()
    {
        SelectArtifact();
    }

    public void SetVisible(bool isVisible = true)
    {
        panel.SetActive(isVisible);
    }

    private void SelectArtifact()
    {
        foreach (var obj in artifacts)
        {
            //FindNextArtifact(obj);

            var artifact = obj.GetComponent<ObjectInteractor>();
            if (artifact.IsHidden)
            {
                SetVisible(true);
                transform.SetParent(obj.transform);
                float offset = artifact.DigPointerDisplayHeight;
                transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + offset, obj.transform.position.z);

                return;
            }
        }
    }

    private void FindNextArtifact(GameObject _obj)
    {
        var artifact = _obj.GetComponent<ObjectInteractor>();
        if (artifact.IsHidden)
        {
            SetVisible(true);
            transform.SetParent(_obj.transform);
            float offset = artifact.DigPointerDisplayHeight;
            transform.position = new Vector3(_obj.transform.position.x, _obj.transform.position.y + offset, _obj.transform.position.z);
        }
    }
}
