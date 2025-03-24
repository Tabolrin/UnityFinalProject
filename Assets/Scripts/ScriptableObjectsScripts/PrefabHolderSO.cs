using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

enum PrefubOptions { powerOrb, healthOrb, spikeTrap }

[CreateAssetMenu(fileName = "PrefabHolderSO", menuName = "Scriptable Objects/PrefabHolderSO")]
public class PrefabHolderSO : ScriptableObject
{
    [SerializeField] public GameObject powerOrb;
    [SerializeField] public GameObject healthOrb;
    [SerializeField] public GameObject spikeTrap;

    public GameObject ChooseRandomPrefub()
    {
        PrefubOptions randomNumber = (PrefubOptions)Random.Range(0, 3);

        switch (randomNumber)
        {
            case PrefubOptions.powerOrb:
                {
                    return powerOrb;
                }

            case PrefubOptions.healthOrb:
                {
                    return healthOrb;
                }

            case PrefubOptions.spikeTrap:
                {
                    return spikeTrap;
                }
        }

        return powerOrb;
    }

    public GameObject ChoosePrefub(int prefabNum)
    {
        switch (prefabNum)
        {
            case (int)PrefubOptions.powerOrb:
            {
                return powerOrb;
            }

            case(int)PrefubOptions.healthOrb:
            {
                return healthOrb;
            }

            case (int)PrefubOptions.spikeTrap:
            {
                return spikeTrap;
            }

            default:
            {
                    Debug.Log("Invalid prefab option");
                    return null;
             }
        }
}
}


