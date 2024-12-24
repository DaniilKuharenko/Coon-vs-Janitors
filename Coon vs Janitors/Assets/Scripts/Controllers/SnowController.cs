using UnityEngine;

namespace Raccons_House_Games
{
    public class SnowController : MonoBehaviour
    {
        [SerializeField] private ComputeShader _snowComputerShader;
        [SerializeField] private RenderTexture _snowRT;
        [SerializeField] private float _colorValueToAdd;
        [SerializeField] private int _resolution = 512;
        [SerializeField] private float _spotSize = 10;
        [SerializeField] private Vector2Int _postion = new Vector2Int(256, 256);

        private string _snowImageProperty = "snowImage";
        private string _colorValueProperty = "colorValueToAdd";
        private string _resolutionProperty = "resolution";
        private string _positionXProperty = "positionX";
        private string _positionYProperty = "positionY";
        private string _spotSizeProperty = "spotSize";

        private string _csMainKernel = "";
        private string _fillWhiteKernel = "";

        private MeshRenderer _meshRenderer;

    }
}
