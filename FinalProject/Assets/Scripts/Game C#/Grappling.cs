using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    //references
    public GameObject handGrabPrefab;
    public LineRenderer lr;
    public Transform playerObj;
    public Camera camera;
    public CharacterController characterController;
    private GameObject handGrab;

    //constraints
    public float maxDistance = 300f;
    public float grappleSpeed = 200f;
    public float homingRadius = 50f;
    public float coneAngle = 20f;

    //input
    public KeyCode grappleKey = KeyCode.Q;

    //states
    private bool grappling = false;
    public bool freeze = false;

    private Vector3 grappleDirection;

    void Update()
    {
        if (Input.GetKeyDown(grappleKey) && !grappling)
        {
            StartGrapple();
        }

        if (freeze && !grappling)
        {
            freeze = false;
        }
    }

    void StartGrapple()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        grappleDirection = ray.direction;

        Vector3 targetPosition = GetTargetPosition(ray.direction, maxDistance, homingRadius);

        if (targetPosition != Vector3.zero)
        {
            grappling = true;
            freeze = true;
            handGrab = Instantiate(handGrabPrefab, playerObj.position, Quaternion.identity);
            lr.positionCount = 2;
            lr.SetPosition(0, playerObj.position);
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

        Collider[] hitsInCone = Physics.OverlapSphere(playerObj.position, maxDistance);

        closestPoint = Vector3.zero;
        closestDistance = Mathf.Infinity;

        foreach (Collider hitCollider in hitsInCone)
        {
            if (hitCollider.CompareTag("Grappleable"))
            {
                Vector3 point = hitCollider.transform.position;
                Vector3 directionToPoint = (point - playerObj.position).normalized;
                float angle = Vector3.Angle(direction, directionToPoint);

                if (angle < coneAngle)
                {
                    float distance = Vector3.Distance(playerObj.position, point);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPoint = point;
                    }
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
            RaycastHit hit;

            if (Physics.Raycast(playerObj.position, grappleDirection, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Grappleable"))
                {
                    handGrab.transform.position = hit.point;
                    lr.SetPosition(1, handGrab.transform.position);
                    yield return new WaitForSeconds(0.1f);
                    ExecuteGrapple(hit.point);
                    yield break;
                }
                else
                {
                    handGrab.transform.position = Vector3.MoveTowards(handGrab.transform.position, playerObj.position + grappleDirection * maxDistance, grappleSpeed * Time.deltaTime);
                    lr.SetPosition(1, handGrab.transform.position);
                }
            }
            else
            {
                handGrab.transform.position = Vector3.MoveTowards(handGrab.transform.position, playerObj.position + grappleDirection * maxDistance, grappleSpeed * Time.deltaTime);
                lr.SetPosition(1, handGrab.transform.position);
            }

            if (handGrab != null && Vector3.Distance(handGrab.transform.position, playerObj.position) >= maxDistance)
            {
                StopGrapple(handGrab.transform.position);
                yield break;
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
        lr.enabled = false;
        lr.positionCount = 0;
        Destroy(handGrab);

        GetComponent<playerMovement>().movementInput = Vector3.zero;
        GetComponent<playerMovement>().moveDirection = Vector3.zero;

        GetComponent<playerMovement>().moveDirection.y = -2f;

        StartCoroutine(MovePlayerToGrapplePoint(grapplePoint));
    }

    IEnumerator MovePlayerToGrapplePoint(Vector3 grapplePoint)
    {
        while (Vector3.Distance(playerObj.position, grapplePoint) > 0.1f)
        {
            characterController.Move((grapplePoint - playerObj.position).normalized * grappleSpeed * Time.deltaTime);
            yield return null;
        }

        characterController.Move((grapplePoint - playerObj.position).normalized * grappleSpeed * Time.deltaTime);
    }
}