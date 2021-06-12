﻿using System;
using EditorTools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EventSystem.Events.Models
{
    /// <summary>
    /// Able to be used for any object, only requirement is a collider
    /// If you want a more detailed movement control over an object I'd recommend
    /// using CharacterMovement over ObjectMovement
    ///
    /// DO NOT PUT ANY CODE HERE, WITH THE EXCEPTION OF EDITOR CODE
    /// </summary>
    [Serializable]
    public class ObjectMovement
    {
        [Tooltip("Not required, will be prefixed to generated targets names")]
        public string descriptor;

        [Tooltip("Gameobject that will be moved")]
        public GameObject target;

        [Tooltip("Position gameobject will be moved to")] [InlineButton(nameof(GenerateTargetPosition), "Create")]
        public GameObject targetPosition;

        [Tooltip("Override the gameobjects current position")]
        [InlineButton(nameof(GenerateStartingPosition), "Create")]
        public GameObject startingPosition;

        [Tooltip(
            "A buffer zone for how close the target needs to be to the targetPosition before the event is considered 'finished'")]
        [Range(0.5f, 5)]
        public float distanceThreshold = 1f;

        [Tooltip("Speed that the object will move at, set on initialization. Default 3.5f")] [Range(0.1f, 10)]
        public float speed = 3.5f;

#if UNITY_EDITOR
        private void GenerateTargetPosition()
        {
            var positionTargetGameObject = UnityEngine.Resources.Load<GameObject>("Prefabs/editorTools/YellowTarget");
            if (positionTargetGameObject == null) return;

            //Assign object back to self
            var instantiatedTarget = Tools.InstantiateObject(positionTargetGameObject);
            instantiatedTarget.name =
                string.IsNullOrEmpty(descriptor) ? "TargetPosition" : $"{descriptor}TargetPosition";
            targetPosition = instantiatedTarget;
        }

        private void GenerateStartingPosition()
        {
            var positionTargetGameObject = UnityEngine.Resources.Load<GameObject>("Prefabs/editorTools/GreenTarget");
            if (positionTargetGameObject == null) return;

            //Assign object back to self
            var instantiatedTarget = Tools.InstantiateObject(positionTargetGameObject);
            instantiatedTarget.name =
                string.IsNullOrEmpty(descriptor) ? "StartingPosition" : $"{descriptor}StartingPosition";
            startingPosition = instantiatedTarget;
        }
#endif
    }
}