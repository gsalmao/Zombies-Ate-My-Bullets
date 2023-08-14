using UnityEngine;

namespace ZAMB.PlayerScripts
{
    /// <summary>
    /// Execute this class when assigning new skin pieces to the rigged character. Auto-deletes unused rigging's transforms after being spawned.
    /// </summary>
    public class SkinPartSetter : MonoBehaviour
    {
        [SerializeField] private Transform mainRigging;

        private const string rigName = "metarig.001";

        private void Awake()
        {
            Wear();
            DestroyOldRigging();
        }

        private void DestroyOldRigging()
        {
            Transform[] children = gameObject.GetComponentsInChildren<Transform>();

            foreach (Transform curChildren in children)
            {
                if (curChildren.gameObject.name == rigName)
                {
                    Destroy(curChildren.gameObject);
                    return;
                }
            }
        }

        [ContextMenu("Wear Clothes")]
        private void Wear()
        {
            if (mainRigging == null)
            {
                Debug.LogError("References missing to wear clothes.");
                return;
            }

            SetCurrentToMainRigging(transform.gameObject, mainRigging);
        }

        private void SetCurrentToMainRigging(GameObject clothes, Transform mainRigging)
        {
            SkinnedMeshRenderer[] clothesSkinnedMeshRend = clothes.GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int index = 0; index < clothesSkinnedMeshRend.Length; index++)
            {
                Transform[] clothesBones = clothesSkinnedMeshRend[index].bones;

                Transform mainRootBone = mainRigging.GetChild(0);
                clothesSkinnedMeshRend[index].rootBone = mainRootBone;

                Transform[] mainBones = mainRootBone.GetComponentsInChildren<Transform>();

                for (int i = 0; i < clothesBones.Length; i++)
                {
                    for (int j = 0; j < mainBones.Length; j++)
                    {
                        if (clothesBones[i].name == mainBones[j].name)
                        {
                            clothesBones[i] = mainBones[j];
                            break;
                        }
                    }
                }

                clothesSkinnedMeshRend[index].bones = clothesBones;
            }
        }
    }
}