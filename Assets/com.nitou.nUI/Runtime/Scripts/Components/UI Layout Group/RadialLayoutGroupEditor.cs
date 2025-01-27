﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace nitou.UI.Component {

    [CustomEditor(typeof(RadialLayoutGroup))]
    public class RadialLayoutGroupEditor : Editor {

        private RadialLayoutGroup radialLayoutGroup;
        private SerializedProperty mAngleConstraint;
        private SerializedProperty mRadiusConstraint;
        private SerializedProperty mLayoutDir;
        private SerializedProperty mRadiusStart;
        private SerializedProperty mRadiusDelta;
        private SerializedProperty mRadiusRange;
        private SerializedProperty mAngleDelta;
        private SerializedProperty mAngleStart;
        private SerializedProperty mAngleCenter;
        private SerializedProperty mAngleRange;
        private SerializedProperty mChildRotate;

        public override void OnInspectorGUI() {
            this.serializedObject.Update();

            EditorGUILayout.PropertyField(this.mLayoutDir, new GUIContent("Layout Dir"));

            EditorGUILayout.PropertyField(this.mAngleConstraint, new GUIContent("Angle Constraint"));
            EditorGUILayout.PropertyField(this.mRadiusConstraint, new GUIContent("Radius Constraint"));

            // angles
            if (this.radialLayoutGroup.LayoutDir != RadialLayoutGroup.Direction.Bidirectional) {
                EditorGUILayout.PropertyField(this.mAngleStart, new GUIContent("Angle Start"));
            } else {
                EditorGUILayout.PropertyField(this.mAngleCenter, new GUIContent("Angle Center"));
            }
            if (this.radialLayoutGroup.AngleConstraint == RadialLayoutGroup.ConstraintMode.Interval) {
                EditorGUILayout.PropertyField(this.mAngleDelta, new GUIContent("Angle Delta"));
            } else {
                EditorGUILayout.PropertyField(this.mAngleRange, new GUIContent("Angle Range"));
            }
            // radius
            EditorGUILayout.PropertyField(this.mRadiusStart, new GUIContent("Radius Start"));
            if (this.radialLayoutGroup.RadiusConstraint == RadialLayoutGroup.ConstraintMode.Interval) {
                EditorGUILayout.PropertyField(this.mRadiusDelta, new GUIContent("Radius Delta"));
            } else {
                EditorGUILayout.PropertyField(this.mRadiusRange, new GUIContent("Radius Range"));
            }
            EditorGUILayout.PropertyField(this.mChildRotate, new GUIContent("Child Rotate"));

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable() {
            if (this.target == null) {
                return;
            }
            this.radialLayoutGroup = this.target as RadialLayoutGroup;

            var serObj = this.serializedObject;
            this.mAngleConstraint = serObj.FindProperty("_angleConstraint");
            this.mRadiusConstraint = serObj.FindProperty("_radiusConstraint");
            this.mLayoutDir = serObj.FindProperty("_layoutDir");
            this.mRadiusStart = serObj.FindProperty("_radiusStart");
            this.mRadiusDelta = serObj.FindProperty("_radiusDelta");
            this.mRadiusRange = serObj.FindProperty("_radiusRange");
            this.mAngleDelta = serObj.FindProperty("_angleDelta");
            this.mAngleStart = serObj.FindProperty("_angleStart");
            this.mAngleCenter = serObj.FindProperty("_angleCenter");
            this.mAngleRange = serObj.FindProperty("_angleRange");
            this.mChildRotate = serObj.FindProperty("_childRotate");
        }
    }

}

#endif