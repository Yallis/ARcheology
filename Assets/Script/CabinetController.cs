using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetController : MonoBehaviour
{
    [SerializeField] private List<SpotController> spots;

    private void OnCollisionEnter(Collision collision) {
        
        if (collision.transform.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            TryToPutOnCabinet(collision.gameObject);
        }
    }

    private void TryToPutOnCabinet(GameObject obj)
    {
        if (GetAvailablesSpot() is SpotController availableSpot)
        {
            obj.transform.SetParent(availableSpot.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            if (obj.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.isKinematic = true;
            }
        }
    }

    private SpotController GetAvailablesSpot()
    {
        foreach(SpotController spot in spots)
        {
            if (!spot.IsOccupied())
            {
                return spot;
            }
        }

        return null;
    }
}
