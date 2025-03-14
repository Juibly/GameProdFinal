using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
    //references
    public Transform camera;
    public Transform handGrab;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    //grapple restraints
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;
    private Vector3 grapplePoint;
    public float travelTime = 4f;

    //grapple cooldown
    public float grapplingCd;
    private float grapplingCdTimer;

    //input
    public KeyCode grappleKey = KeyCode.Q;

    private bool grappling;

    private bool canMove = true;

    public bool activeGrapple;

    //for freezing player when grappling
    public CharacterController controller;

    public bool freeze;

    private bool enableMovementOnNextTouch;

    void Start()
    {
        GetComponent<playerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;

        if (freeze)
        {
            canMove = false;
        }
    }

    void LateUpdate()
    {
        if (grappling)
        {
            lr.SetPosition(0, handGrab.position);
            lr.SetPosition(1, grapplePoint);
        }
    }

    void StartGrapple()
    {
        if (grapplingCdTimer > 0 || grappling) return;

        grappling = true;

        freeze = true;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxGrappleDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = camera.position + camera.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    void ExecuteGrapple()
    {
        freeze = false;

        activeGrapple = true;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = Mathf.Clamp(grapplePointRelativeYPos + overshootYAxis, 0f, 5f);

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;

        JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    void StopGrapple()
    {
        freeze = false;

        grappling = false;

        activeGrapple = false;

        grapplingCdTimer = grapplingCd;

        lr.enabled = false;
    }

    void JumpToPosition(Vector3 targetPosition, float travelTime)
    {
        Invoke(nameof(SetVelocity), 0.1f);

        activeGrapple = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, travelTime);
    }

    private Vector3 velocityToSet;

    private void SetVelocity()
    {
        StartCoroutine(ApplyGrappleMovement());
    }

    private IEnumerator ApplyGrappleMovement()
    {
        float grappleDuration = 1.5f;
        float elapsedTime = 0f;

        Vector3 startPosition = transform.position;

        while (elapsedTime < grappleDuration)
        {
            float t = elapsedTime / grappleDuration;

            float easedT = Mathf.SmoothStep(0f, 1f, t);

            Vector3 newPosition = Vector3.Lerp(startPosition, grapplePoint, easedT);
            controller.Move(newPosition - transform.position);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        activeGrapple = false;
        enableMovementOnNextTouch = true;
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
    }

    void OnCollisionEnter(Collision collsion)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            StopGrapple();
        }
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float travelTime)
    {
        Vector3 direction = (endPoint - startPoint).normalized;
        float distance = Vector3.Distance(startPoint, endPoint);

        return direction * (distance / travelTime);
    }
}