using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace nitou {
    public class InputFieldBindingTest : MonoBehaviour {
        [Header("UI Elements")]
        public TMP_InputField intInputField;
        public TMP_InputField floatInputField;
        public TMP_InputField stringInputField;
        public TextMeshProUGUI intDisplay;
        public TextMeshProUGUI floatDisplay;
        public TextMeshProUGUI stringDisplay;

        private IReactiveProperty<int> _intProperty;
        private IReactiveProperty<float> _floatProperty;
        private IReactiveProperty<string> _stringProperty;
        private List<IDisposable> _disposables;

        void Start() {
            // Initialize properties and disposables list
            _intProperty = new ReactiveProperty<int>(0);
            _floatProperty = new ReactiveProperty<float>(0.0f);
            _stringProperty = new ReactiveProperty<string>("");

            _disposables = new List<IDisposable>();

            // Bind properties to input fields
            _intProperty.BindToInputField(intInputField, _disposables);
            _floatProperty.BindToInputField(floatInputField, _disposables);
            _stringProperty.BindToInputField(stringInputField, _disposables);

            // Bind properties to display text for UI feedback
            _intProperty.SubscribeToText(intDisplay).AddTo(_disposables);
            _floatProperty.SubscribeToText(floatDisplay, f => f.ToString("F2")).AddTo(_disposables);
            _stringProperty.SubscribeToText(stringDisplay).AddTo(_disposables);

            // Initialize UI with default property values
            intInputField.text = _intProperty.Value.ToString();
            floatInputField.text = _floatProperty.Value.ToString("G");
            stringInputField.text = _stringProperty.Value;
        }

        private void OnDestroy() {
            // Dispose of all subscriptions when the object is destroyed
            foreach (var disposable in _disposables) {
                disposable.Dispose();
            }
        }

        private void Update() {
            // Optional: Test changing properties programmatically for verification
            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _intProperty.Value += 1;
                _floatProperty.Value += 0.1f;
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _intProperty.Value -= 1;
                _floatProperty.Value -= 0.1f;
            }
        }
    }
}
