using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
#endif

namespace Raccons_House_Games
{
    [RequireComponent(typeof(ParticleSystem))]
    public class BakeParticle : MonoBehaviour
    {
        public string FolderPath = "Meshes";
        public string FileName = "NewBakeParticle";
        public bool KeepVertexColor = true;
        public NormalTypes HandleNormals;

#if UNITY_EDITOR
        [ContextMenu("Bake To Mesh Asset")]
        public void SaveAsset()
        {
            // Bake mesh from ParticleSystem
            Mesh mesh = new Mesh();
            var renderer = GetComponent<ParticleSystemRenderer>();
            renderer.BakeMesh(mesh, ParticleSystemBakeMeshOptions.Default);

            if (!KeepVertexColor)
            {
                mesh.colors32 = null;
            }

            switch (HandleNormals)
            {
                case NormalTypes.KeepNormals:
                    break;
                case NormalTypes.NormalizedVertexPosition:
                    Vector3[] normals = mesh.vertices;
                    int length = normals.Length;
                    for (int i = 0; i < length; i++)
                    {
                        normals[i] = normals[i].normalized;
                    }
                    mesh.normals = normals;
                    break;
                default:
                case NormalTypes.ClearNormals:
                    mesh.normals = null;
                    break;
            }

            // Setup Path
            string fileName = Path.GetFileNameWithoutExtension(FileName) + ".asset";
            string fullPath = Path.Combine("Assets/GameFiles/Particles", FolderPath);
            Directory.CreateDirectory(fullPath);
            string assetPath = Path.Combine(fullPath, fileName);

            // Create or override the mesh asset
            Object existingAsset = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);
            if (existingAsset == null)
            {
                AssetDatabase.CreateAsset(mesh, assetPath);
            }
            else
            {
                Mesh existingMesh = existingAsset as Mesh;
                if (existingMesh != null)
                {
                    existingMesh.Clear();
                    EditorUtility.CopySerialized(mesh, existingMesh);
                }
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
#endif
    }
}
