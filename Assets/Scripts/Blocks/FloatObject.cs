using System.Collections;
using UnityEngine;

public class FloatObject : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;
    public float moveDuration = 1f;
    public float waitDuration = 1f;
    [Header("Refrence")]
    [SerializeField] Collider col;

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
            Vector3 startMovePosition = transform.position;
            if (movingDown)
                targetPosition = originalPosition + Vector3.down * moveDistance;
            else
                targetPosition = originalPosition;

            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position = Vector3.Lerp(startMovePosition, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;

            if (movingDown)
                col.enabled = false;
            yield return new WaitForSeconds(waitDuration);
            if (movingDown)
                col.enabled = true;

            movingDown = !movingDown;
        }
    }
}
