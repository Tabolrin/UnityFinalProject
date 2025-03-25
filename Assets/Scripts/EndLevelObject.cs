using UnityEngine;

public class EndLevelObject : MonoBehaviour
{
    [SerializeField] string SceneToLoad;
    [SerializeField] SceneChanger sceneChanger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            sceneChanger.ChangeScene(SceneToLoad);
    }
}
