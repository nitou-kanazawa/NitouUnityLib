using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Develop
{
    public class ListCell : MonoBehaviour {

        [SerializeField] TextMeshProUGUI _text;

        public int Index { get; private set; } = -1;

        public void SetIndex(int index) {
            Index = index;
            _text.text = index.ToString();
        }
    }
}
