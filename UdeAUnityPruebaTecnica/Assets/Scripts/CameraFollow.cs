using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    private Vector3 offset = new Vector3(0f, 1.5f, -1.5f);
    private float angleOffsetX = 20f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.up * offset.y + target.forward * offset.z;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        Quaternion offsetRotation = Quaternion.Euler(angleOffsetX, lookRotation.eulerAngles.y, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, offsetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        followSpeed = 1f;
        rotationSpeed = 1f;
        target = newTarget;
        StartCoroutine(SmoothlyIncreaseSpeed(5f, 20f));
    }

    IEnumerator SmoothlyIncreaseSpeed(float duration, float targetSpeed)
    {
        yield return new WaitForSeconds(3f);
        float elapsedTime = 0f;
        float initialFollowSpeed = followSpeed;
        float initialRotationSpeed = rotationSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            followSpeed = Mathf.Lerp(initialFollowSpeed, targetSpeed, t);
            rotationSpeed = Mathf.Lerp(initialRotationSpeed, targetSpeed, t);

            yield return null;
        }

        followSpeed = targetSpeed;
        rotationSpeed = targetSpeed;
    }

}
