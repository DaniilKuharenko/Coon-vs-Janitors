using UnityEngine;

namespace Raccons_House_Games
{
    public class SnowPathDrawer : MonoBehaviour
    {
        [SerializeField] private ComputeShader _snowComputerShader;
        [SerializeField] private RenderTexture _snowRT;
        [SerializeField] private float _spotSize = 5.0f;

        private string _snowImageProperty = "snowImage";
        private string _colorValueProperty = "colorValueToAdd";
        private string _resolutionProperty = "resolution";
        private string _positionXProperty = "positionX";
        private string _positionYProperty = "positionY";
        private string _spotSizeProperty = "spotSize";

        private string _drawSpotKernel = "DrawSpot";
        private Vector2Int _position = new Vector2Int(256, 256);
        
        private SnowController _snowController;
        private GameObject[] _snowControllerObjects;

        private void Awake()
        {
            _snowControllerObjects = GameObject.FindGameObjectsWithTag("SnowGround");
        }

        private void FixedUpdate()
        {
            for(int i = 0; i < _snowControllerObjects.Length; i++)
            {
                if(Vector3.Distance(_snowControllerObjects[i].transform.position, transform.position) > _spotSize * 5f)
                {
                    continue;
                }

                _snowController = _snowControllerObjects[i].GetComponent<SnowController>();

            }
        }

        private void GetPosition()
        {
            float scaleX = _snowController.transform.localScale.x;
            float scaleY = _snowController.transform.localScale.z;

            float snowPosX = _snowController.transform.position.x;
            float snowPosY = _snowController.transform.position.z;

            int posX = _snowRT.width / 2 - (int)(((transform.position.x - snowPosX) * _snowRT.width / 2) / scaleX);
            int posY = _snowRT.width / 2 - (int)(((transform.position.z - snowPosY) * _snowRT.height / 2) / scaleY);
            _position = new Vector2Int(posX, posY);
        }

        private void DrawSpot()
        {
            if(_snowRT == null) return;
            if(_snowComputerShader == null) return;

            int kernel_handle = _snowComputerShader.FindKernel(_drawSpotKernel);
            _snowComputerShader.SetTexture(kernel_handle, _snowImageProperty, _snowRT);
            _snowComputerShader.SetFloat(_colorValueProperty, 0);
            _snowComputerShader.SetFloat(_resolutionProperty, _snowRT.width);
            _snowComputerShader.SetFloat(_positionXProperty, _position.x);
            _snowComputerShader.SetFloat(_positionYProperty, _position.y);
            _snowComputerShader.SetFloat(_spotSizeProperty, _spotSize);
            _snowComputerShader.Dispatch(kernel_handle, _snowRT.width / 8, _snowRT.height / 8, 1);
        }
    }
}
