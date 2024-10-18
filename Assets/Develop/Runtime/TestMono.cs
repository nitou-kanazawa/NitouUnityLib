using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using nitou;
using System.Collections.Generic;

namespace Project{

    public class TestMono : MonoBehaviour{

        [SerializeField] private List<GraphicRaycaster> raycasters; // �`�F�b�N����GraphicRaycaster�̃��X�g

        private void Update() {
            if (Input.GetMouseButtonDown(0)) // �}�E�X���N���b�N�̃`�F�b�N
            {
                CheckOverlap();
            }
        }

        private void CheckOverlap() {
            // PointerEventData��������
            var pointerEventData = new PointerEventData(EventSystem.current) {
                position = Input.mousePosition // �N���b�N�����X�N���[�����W��ݒ�
            };

            // OverlapUI���\�b�h���Ăяo���āAUI���d�Ȃ��Ă��邩���`�F�b�N
            bool isOverlap = raycasters.OverlapUI(pointerEventData);

            // ���ʂ����O�o��
            if (isOverlap) {
                Debug_.Log("UI���d�Ȃ��Ă��܂��B", Colors.Red);
            } else {
                Debug_.Log("UI���d�Ȃ��Ă��܂���B", Colors.Green);

            }
        }
    }
}
