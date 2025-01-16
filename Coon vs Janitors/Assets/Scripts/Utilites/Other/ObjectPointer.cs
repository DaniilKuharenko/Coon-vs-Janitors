
using UnityEngine;

namespace Raccons_House_Games
{
    public class ObjectPointer : MonoBehaviour
    {

        public void StartPointer()
        {
            PointerManager.Instance.AddToList(this);
        }
    }
}
