using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class NavTeleport : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    float agentPathToHitThreshold;

    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    Transform head;
    [SerializeField]
    Transform playerOrigin;
    [SerializeField]
    float maxSlope;

    public float maxDistance;    

    private NavMeshPath path;
    private Vector3[] corners;
    private void Awake()
    {
        path = new NavMeshPath();
        corners = new Vector3[1024];
        agentPathToHitThreshold = agent.radius;
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var laserEventData = eventData as Wacki.LaserPointerEventData;
        if (laserEventData == null)
            return;

        agent.Warp(head.position);

        var hit = laserEventData.pointerCurrentRaycast.worldPosition;
        var normal = laserEventData.pointerCurrentRaycast.worldNormal;
        var offset = head.position - playerOrigin.position;
        offset.y = 0;

        if (Vector3.Dot(Vector3.up, normal) < Mathf.Cos(Mathf.Deg2Rad*maxSlope))
        {
            return;
        }

        var result = agent.CalculatePath(hit, path);
        if (result) {
            var cornersLen = path.GetCornersNonAlloc(corners);
            float hitToPathDistance = Vector3.Distance(corners[cornersLen-1], hit);
            if (hitToPathDistance > agentPathToHitThreshold)
            {
                return;
            }
            hit = corners[cornersLen - 1];
            float distance = 0f;
            for (int i = 1; i < cornersLen; i++) {
                distance += Vector3.Distance(corners[i - 1], corners[i]);
            }
            if (distance < maxDistance) {
                playerOrigin.position = hit - offset;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
