using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Grappling : MonoBehaviour
{
    //references
    public GameObject handGrabPrefab;
    public LineRenderer lr;
    public Transform playerObj;
    public Camera camera;
    public CharacterController characterController;
    private GameObject handGrab;
    public TextMeshProUGUI grappleText;
    public Transform grappleGun;

    //constraints
    public float maxDistance = 310f;
    public float grappleSpeed = 200f;
    public float homingRadius = 55f;
    private bool grappleOnCooldown = false;
    public float grappleCooldown = 4f;

    //input
    public KeyCode grappleKey = KeyCode.Q;

    //states
    private bool grappling = false;
    public bool freeze = false;

    private Vector3 grappleDirection;

    public AudioSource GrappleSource;

    void Update()
    {
        if (Input.GetButtonDown("Grapple") && !grappling && !grappleOnCooldown)
        {
            StartCoroutine(GrappleCooldown());
            StartGrapple();
        }

        bool grappleableObjectInRange = IsGrappleableObjectInRange();

        grappleText.enabled = grappleableObjectInRange;

        if (freeze && !grappling)
        {
            freeze = false;
        }
    }

    bool IsGrappleableObjectInRange()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 direction = ray.direction;

        Collider[] hits = Physics.OverlapSphere(playerObj.position + direction * maxDistance, homingRadius);

        foreach (Collider hitCollider in hits)
        {
            if (hitCollider.CompareTag("Grappleable"))
            {
                return true;
            }
        }

        Vector3 boxCenter = playerObj.position + direction * (maxDistance / 2f);
        Vector3 boxHalfExtents = new Vector3(homingRadius, homingRadius, maxDistance / 2f);
        Quaternion boxRotation = Quaternion.LookRotation(direction);

        Collider[] boxHits = Physics.OverlapBox(boxCenter, boxHalfExtents, boxRotation);
        foreach (Collider hit in boxHits)
        {
            if (hit.CompareTag("Grappleable"))
            {
                return true;
            }
        }

        return false;
    }

    IEnumerator GrappleCooldown()
    {
        grappleOnCooldown = true;
        yield return new WaitForSeconds(grappleCooldown);
        grappleOnCooldown = false;
    }

    void StartGrapple()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        grappleDirection = ray.direction;

        Vector3 targetPosition = GetTargetPosition(ray.direction, maxDistance, homingRadius);

        if (targetPosition != Vector3.zero)
        {
            grappling = true;
            freeze = false;
            handGrab = Instantiate(handGrabPrefab, grappleGun.position, Quaternion.identity);
            lr.positionCount = 2;
            lr.SetPosition(0, grappleGun.position);
            lr.enabled = true;

            grappleDirection = (targetPosition - playerObj.position).normalized;

            StartCoroutine(MoveHandGrab());
        }
    }

    Vector3 GetTargetPosition(Vector3 direction, float maxDistance, float homingRadius)
    {
        RaycastHit hit;

        if (Physics.Raycast(playerObj.position, direction, out hit, maxDistance))
        {
            if (hit.transform.CompareTag("Grappleable"))
            {
                return hit.point;
            }
        }

        Collider[] hits = Physics.OverlapSphere(playerObj.position + direction * maxDistance, homingRadius);

        Vector3 closestPoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hits)
        {
            if (hitCollider.CompareTag("Grappleable"))
            {
                Vector3 point = hitCollider.transform.position;
                float distance = Vector3.Distance(playerObj.position, point);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
        }

        if (closestPoint != Vector3.zero)
        {
            return closestPoint;
        }

        Vector3 boxCenter = playerObj.position + direction * (maxDistance / 2f);
        Vector3 boxHalfExtents = new Vector3(homingRadius, homingRadius, maxDistance / 2f);
        Quaternion boxRotation = Quaternion.LookRotation(direction);

        Collider[] hitsInBox = Physics.OverlapBox(boxCenter, boxHalfExtents, boxRotation);

        foreach (Collider hitCollider in hitsInBox)
        {
            if (hitCollider.CompareTag("Grappleable"))
            {
                Vector3 point = hitCollider.transform.position;
                float distance = Vector3.Distance(playerObj.position, point);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
        }

        return closestPoint;
    }


    IEnumerator MoveHandGrab()
    {
        float timeSinceMaxDistance = 0f;

        while (true)
        {
            Vector3 nextPosition = handGrab.transform.position + grappleDirection * grappleSpeed * Time.deltaTime;
            RaycastHit obstacleHit;

            if (Physics.Raycast(handGrab.transform.position, grappleDirection, out obstacleHit, grappleSpeed * Time.deltaTime))
            {
                if (!obstacleHit.transform.CompareTag("Grappleable"))
                {
                    StopGrapple(handGrab.transform.position);
                    yield break;
                }
            }

            handGrab.transform.position = nextPosition;
            lr.SetPosition(1, handGrab.transform.position);

            RaycastHit hit;
            if (Physics.Raycast(playerObj.position, grappleDirection, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Grappleable"))
                {
                    handGrab.transform.position = hit.point;
                    lr.SetPosition(0, grappleGun.position);
                    lr.SetPosition(1, handGrab.transform.position);
                    yield return new WaitForSeconds(0.1f);
                    ExecuteGrapple(hit.point);
                    yield break;
                }
            }

            if (Vector3.Distance(handGrab.transform.position, playerObj.position) >= maxDistance)
            {
                timeSinceMaxDistance += Time.deltaTime;
                if (timeSinceMaxDistance >= 0.25f)
                {
                    StopGrapple(handGrab.transform.position);
                    yield break;
                }
            }

            if (!grappling)
            {
                yield break;
            }

            yield return null;
        }
    }

    void StopGrapple(Vector3 grapplePoint)
    {
        grappling = false;
        freeze = false;
        lr.enabled = false;
        lr.positionCount = 0;
        Destroy(handGrab);

        GetComponent<playerMovement>().movementInput = Vector3.zero;
        GetComponent<playerMovement>().moveDirection = Vector3.zero;

        GetComponent<playerMovement>().moveDirection.y = -2f;
    }

    void ExecuteGrapple(Vector3 grapplePoint)
    {
        grappling = false;
        freeze = false;

        GetComponent<playerMovement>().movementInput = Vector3.zero;
        GetComponent<playerMovement>().moveDirection = Vector3.zero;

        GetComponent<playerMovement>().moveDirection.y = -2f;

        StartCoroutine(MovePlayerToGrapplePoint(grapplePoint));
    }

    IEnumerator MovePlayerToGrapplePoint(Vector3 grapplePoint)
    {
        GrappleSource.Play();

        while (Vector3.Distance(playerObj.position, grapplePoint) > 0.1f)
        {
            characterController.Move((grapplePoint - playerObj.position).normalized * grappleSpeed * Time.deltaTime);
            yield return null;
        }

        characterController.Move((grapplePoint - playerObj.position).normalized * grappleSpeed * Time.deltaTime);

        Destroy(handGrab);
        lr.enabled = false;
        lr.positionCount = 0;
    }
}