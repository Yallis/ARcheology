using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractor : MonoBehaviour, IInteractable
{

    private bool isHeld = false;
    private bool isLocked = false;
    private bool isScanned = false;
    public bool IsHidden { get; private set; } = true;

    [SerializeField] private SOObjectInfo objectInfo;
    [SerializeField] private float infoDisplayHeight = 2f;

    [SerializeField] private float digPointerDisplayHeight = 0.5f;

    [SerializeField] private ArtifactCleaner artifactCleaner;

    [SerializeField] private ParticlesController digParticles;
    [SerializeField] private Transform groundTransform;

    public float DigPointerDisplayHeight => digPointerDisplayHeight;

    public void OnInteract()
    {
        //Debug.Log("Interagindo com o cubo!");

        if (isLocked) return;

        if (IsHidden && transform.position.y < 0.15f)
        {
            //Debug.Log("Cavando...");
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);

            if (digParticles != null)
            {
                digParticles.transform.position = new Vector3(transform.position.x, groundTransform.position.y, transform.position.z);
                digParticles.EmitParticles();
            }

            if (transform.position.y > 0.15f)
            {
                WasFound();
            }

            return;
        }

        if (HoldingManager.Instance.TryPickUp(gameObject))
        {
            isHeld = true;
            ShowObjectInfo();
        }
        else if (isHeld)
        {
            HoldingManager.Instance.Drop();
            isHeld = false;
            HideObjectInfo();
        }
    }

    public void StopInteract()
    {
        //Debug.Log("Parando de interagir com o cubo!");
    }

    void Update()
    {
        if (InputHandler.TryRayCastHit(out RaycastHit hitObject))
        {
            if (hitObject.transform == transform)
            {
                OnInteract();
            }
        }

        if (this.gameObject.transform.position.y < -5f)
        {
            this.gameObject.transform.position = new Vector3(0f, 2f, 0f);
            this.gameObject.transform.localRotation = Quaternion.identity;
        }

    }

    private void ShowObjectInfo()
    {
        if (objectInfo == null || isScanned == false) return;

        var infoController = FindObjectOfType<ObjectInfoController>();

        if (infoController != null)
        {
            infoController.SetObjectInfo(objectInfo);
            infoController.SetVisible(true);

            infoController.transform.SetParent(transform);
            infoController.transform.localPosition = new Vector3(0, infoDisplayHeight, 0);
        }
    }

    private void HideObjectInfo()
    {
        var infoController = FindObjectOfType<ObjectInfoController>();

        if (infoController != null)
        {
            infoController.SetVisible(false);
            infoController.transform.SetParent(null);
        }
    }

    public void SetLocked(bool locked = true)
    {
        isLocked = locked;
    }

    public void SetScanned(bool scanned = true)
    {
        isScanned = scanned;

        if (isScanned && artifactCleaner != null)
        {
            artifactCleaner.Clean();
        }
    }

    private void WasFound()
    {
        //Debug.Log("Objeto encontrado!");
        IsHidden = false;
        
        transform.localRotation = Quaternion.identity;

        var digPointerController = FindObjectOfType<DigPointerController>();
        if (digPointerController != null)
            digPointerController.SetVisible(false);

        var body = gameObject.GetComponent<Rigidbody>();
        if (body != null)
            body.isKinematic = false;

        var cleaner = gameObject.GetComponent<ArtifactCleaner>();
        if (cleaner != null)
            cleaner.SetDirtVFX(true);
    }

}
