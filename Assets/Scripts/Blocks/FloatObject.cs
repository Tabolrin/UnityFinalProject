using System.Collections;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float waitDuration = 1f;

    private Vector3 originalPosition;
    private bool movingDown = true;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        while (true)
        {
            Vector3 targetPosition;

            if (movingDown)
                targetPosition = originalPosition + Vector3.down * moveDistance;
            else
                targetPosition = originalPosition;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;

            yield return new WaitForSeconds(waitDuration);

            movingDown = !movingDown;
        }
    }
}
