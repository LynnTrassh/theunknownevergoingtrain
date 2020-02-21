using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Z
{
    [CustomEditor(typeof(CharacterMod))]
    public class Editor_CharacterMod : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Apply"))
            {
                CharacterMod Mod = (CharacterMod)target;
                Undo.RegisterFullObjectHierarchyUndo(Mod.gameObject, "ModApply");
                //Undo.RegisterCompleteObjectUndo(Mod.gameObject, "ModApply");
                Mod.EditorApply();
            }
        }
    }
}