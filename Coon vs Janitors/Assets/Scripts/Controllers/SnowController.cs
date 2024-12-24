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

        private string _csMainKernel = "CSMain";
        private string _fillWhiteKernel = "FillWhite";

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            CreateRenderTexture();
            SetRTColorToWhite();
            SetMaterialTexture();
        }

        private void CreateRenderTexture()
        {
            _snowRT = new RenderTexture(_resolution, _resolution, 24);
            _snowRT.enableRandomWrite = true;
            _snowRT.Create();
        }

        private void SetRTColorToWhite()
        {
            int kernel_handle = _snowComputerShader.FindKernel(_fillWhiteKernel);
            _snowComputerShader.SetTexture(kernel_handle, _snowImageProperty, _snowRT);
            _snowComputerShader.SetFloat(_colorValueProperty, _colorValueToAdd);
            _snowComputerShader.SetFloat(_resolutionProperty, _resolution);
            _snowComputerShader.SetFloat(_positionXProperty, 0);
            _snowComputerShader.SetFloat(_positionYProperty, 0);
            _snowComputerShader.SetFloat(_spotSizeProperty, 0);
            _snowComputerShader.Dispatch(kernel_handle, _snowRT.width / 8, _snowRT.height / 8, 1);
        }

        private void SetMaterialTexture()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.material.SetTexture("_PathTexture", _snowRT);
        }
    }
}
