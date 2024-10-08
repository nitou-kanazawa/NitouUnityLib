using UnityEngine;

namespace Project
{

    /// <summary>
    /// 
    /// </summary>
    public class WorldPointer : MonoBehaviour{

        [SerializeField] float _offset = 1;
        
        void Update() {

            // Screenç¿ïW
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane + _offset;

            var pos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = pos;
            Debug.Log($"screen: {mousePos}, world: {pos}");

            // 
        }


        private void UpdatePosition() {


        }


    }
}
