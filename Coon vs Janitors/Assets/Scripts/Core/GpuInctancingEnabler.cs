using UnityEngine;

namespace Raccons_House_Games
{
    [RequireComponent(typeof(MeshRenderer))]
    public class GpuInctancingEnabler : MonoBehaviour
    {
        public void GpuInctancEnable()
        {
            MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}