using UnityEngine;

namespace ZAMB.Application
{
    public class GameManager : MonoBehaviour
    {
        private void Awake()
        {
            Screen.SetResolution(1920, 1080, true);
            Cursor.visible = false;
        }
    }
}
