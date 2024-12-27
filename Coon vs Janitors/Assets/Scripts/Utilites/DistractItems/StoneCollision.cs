using UnityEngine;

namespace Raccons_House_Games
{
    public class StoneCollision : Items
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.contacts.Length > 0)
            {
                SoundPlay();
            }
        }
    }
}
