using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScannerController : MonoBehaviour
{
    [SerializeField] private SpotController spot;
    [SerializeField] private float scanDuration = 3f;
    [SerializeField] GameObject scanUI;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            TryToPutOnSpot(collision.gameObject);
        }
    }

    private void TryToPutOnSpot(GameObject obj)
    {
        if (!spot.IsOccupied())
        {
            obj.transform.SetParent(spot.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }

            if (obj.TryGetComponent(out ObjectInteractor interactor))
            {
                StartCoroutine(StartScanning(interactor));
            }
        }
    }

    private IEnumerator StartScanning(ObjectInteractor interactor)
    {
        animator.SetBool("b_isScanning", true);
        scanUI.SetActive(false);

        interactor.SetLocked(true);

        yield return new WaitForSeconds(scanDuration);

        animator.SetBool("b_isScanning", false);
        scanUI.SetActive(true);

        interactor.SetLocked(false);
        interactor.SetScanned(true);
    }
}
