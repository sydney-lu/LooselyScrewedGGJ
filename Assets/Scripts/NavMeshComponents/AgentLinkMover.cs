using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public enum OffMeshLinkMoveMethod
{
    Teleport,
    NormalSpeed,
    Parabola,
    Curve
}

[RequireComponent(typeof(NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
    [SerializeField]
    OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;

    [SerializeField]
    float duration = 1f;

    IEnumerator Start()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        agent.updateUpAxis = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                if (method == OffMeshLinkMoveMethod.NormalSpeed)
                    yield return StartCoroutine(NormalSpeed(agent));
                else if (method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(agent, 2.0f, duration));
                else if (method == OffMeshLinkMoveMethod.Curve)
                    yield return StartCoroutine(Curve(agent, duration));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }
    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + transform.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + transform.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * transform.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 velocity = agent.velocity;

        while ((transform.position - data.startPos).magnitude > 0.5f)
        {
            transform.position += (data.startPos - transform.position).normalized * agent.speed * Time.deltaTime;
            yield return null;
        }
        Vector3 agentPos = transform.position;

        bool rotateUp = Vector3.Dot(transform.up, (data.endPos - data.startPos).normalized) > 0;
        bool reverse = Mathf.Abs(Vector3.Dot(transform.up, Vector3.up)) > 0.5f;
        
        Vector3 midPos = reverse ? data.endPos : data.startPos;
        midPos.y = reverse ? data.startPos.y : data.endPos.y;

        Vector3 run = midPos - data.startPos;
        Vector3 rise = midPos - data.endPos;

        Quaternion startRot = agent.transform.rotation;
        Quaternion rotationDelta = Quaternion.FromToRotation(run, rise);
        Quaternion endRot = Quaternion.LookRotation(rotationDelta * -run, rotationDelta * (rotateUp ? rise : -rise));

        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            Vector3 lerp1 = Vector3.Lerp(agentPos, midPos, normalizedTime);
            Vector3 lerp2 = Vector3.Lerp(midPos, data.endPos, normalizedTime);

            agent.transform.position = Vector3.Lerp(lerp1, lerp2, normalizedTime);
            agent.transform.rotation = Quaternion.Lerp(startRot, endRot, normalizedTime);

            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
        transform.rotation = endRot;
        agent.velocity = transform.forward * agent.speed;
    }
}