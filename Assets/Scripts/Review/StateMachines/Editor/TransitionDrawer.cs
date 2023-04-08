using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Review.StateMachines.Editor
{
    [CustomPropertyDrawer(typeof(Transition))]
    public class TransitionDrawer : PropertyDrawer
    {
        private class PropertyData
        {
            public SerializedProperty beforeStateProperty;
            public SerializedProperty afterStateProperty;
            public SerializedProperty usableStateObjectsProperty;
            public SerializedProperty conditionsProperty;

            public int beforeStateIndex = 0;
            public int afterStateIndex = 0;
        }

        private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
        private PropertyData _property;

        private float LineHeight { get { return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; } }

        private void Init(SerializedProperty property)
        {
            if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out _property))
            {
                return;
            }

            _property = new PropertyData();
            _property.beforeStateProperty = property.FindPropertyRelative("beforeState");
            _property.afterStateProperty = property.FindPropertyRelative("afterState");
            _property.usableStateObjectsProperty = property.FindPropertyRelative("usableStateObjects");
            _property.conditionsProperty = property.FindPropertyRelative("consitions");

            _propertyDataPerPropertyPath.Add(property.propertyPath, _property);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Init(property);
            var fieldRect = position;
            // インデントされた位置のRectが欲しければこっちを使う
            var indentedFieldRect = EditorGUI.IndentedRect(fieldRect);
            fieldRect.height = LineHeight;


            // Prefab化した後プロパティに変更を加えた際に太字にしたりする機能を加えるためPropertyScopeを使う
            using (new EditorGUI.PropertyScope(fieldRect, label, property))
            {
                // プロパティ名を表示して折り畳み状態を得る
                property.isExpanded = EditorGUI.Foldout(new Rect(fieldRect), property.isExpanded, label);
                if (property.isExpanded)
                {

                    using (new EditorGUI.IndentLevelScope())
                    {
                        if (_property.usableStateObjectsProperty == null || !_property.usableStateObjectsProperty.isArray)
                            return;

                        var arraySize = _property.usableStateObjectsProperty.arraySize;
                        string[] stateNameArray = new string[arraySize + 1];
                        for (int i = 0; i < arraySize; i++)
                        {
                            stateNameArray[i] = ((BaseStateObject)_property.usableStateObjectsProperty.GetArrayElementAtIndex(i).objectReferenceValue).stateName;
                        }
                        stateNameArray[arraySize] = "None";

                        // 遷移元の選択欄を描画
                        fieldRect.y += LineHeight;
                        _property.beforeStateIndex = EditorGUI.Popup(new Rect(fieldRect), "beforeState", _property.beforeStateIndex, stateNameArray);
                        _property.beforeStateProperty.objectReferenceValue = _property.beforeStateIndex == arraySize ? null : _property.usableStateObjectsProperty.GetArrayElementAtIndex(_property.beforeStateIndex).objectReferenceValue;

                        // 遷移後の選択欄を描画
                        fieldRect.y += LineHeight;
                        _property.afterStateIndex = EditorGUI.Popup(new Rect(fieldRect), "afterState", _property.afterStateIndex, stateNameArray);
                        _property.afterStateProperty.objectReferenceValue = _property.afterStateIndex == arraySize ? null : _property.usableStateObjectsProperty.GetArrayElementAtIndex(_property.afterStateIndex).objectReferenceValue;

                        //遷移条件を描画
                        fieldRect.y += LineHeight;
                        EditorGUI.PropertyField(new Rect(fieldRect), _property.conditionsProperty);
                    }
                }
            }

        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            Init(property);

            if (property.isExpanded)
            {
                float height = 0;
                height = LineHeight * 3;
                height += EditorGUI.GetPropertyHeight(_property.conditionsProperty);
                return height;
            }
            else
            {
                return LineHeight;
            }
        }
    }
}

