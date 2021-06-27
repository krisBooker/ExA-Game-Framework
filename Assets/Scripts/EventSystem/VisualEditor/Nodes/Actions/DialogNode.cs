﻿using System.Collections.Generic;
using EditorTools;
using EventSystem.Models;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace EventSystem.VisualEditor.Nodes.Actions
{
    /// <summary>
    /// DO NOT PUT ANY CODE HERE, WITH THE EXCEPTION OF EDITOR CODE
    /// </summary>
    [NodeTint("#277DA1")]
    public class DialogNode : BaseNode
    {
        [Input] public NodeLink entry;
        
        [Output, HideIf("@this.options.Count > 0")]
        public NodeLink exit;

        [Header("Dialog behaviour")]
        [LabelWidth(150), Tooltip("Once the user has clicked continue, the dialog box will be hidden")]
        public bool hideUIOnComplete;
        
        [Header("Character speaking")]
        
        //TODO: Add back once character work is complete
        // [LabelWidth(100), Tooltip("Used for character name, arrow position")]
        // public GameObject character;

        //TODO: Remove once character work is complete
        [LabelWidth(100),
         Tooltip("Not required, if character is provided details will be used from there. Can be used to override.")]
        public string characterName;

        //TODO: Add back once character work is complete
        // [LabelWidth(100),
        //  Tooltip("Not required, if character is provided details will be used from there. Can be used to override.")]
        // public GameObject arrowPosition;

        //TODO: Remove 'OnValueChanged', change to OnValidate
        [LabelWidth(100), Tooltip("User for localization")] [DelayedProperty, OnValueChanged("GetMessage")]
        public string key;

        [LabelWidth(100),
         Tooltip("Allows user to edit the field localized text. Warning this will edit everywhere this key is used.")]
        public bool textEditable = true;

        //TODO: Remove 'OnValueChanged', change to OnValidate
        [OnValueChanged("UpdateLocalizationFile"), TextArea]
        [ShowIf("@this.textEditable == true")]
        public string text;

        [ReadOnly, ShowIf("@this.textEditable == false"), TextArea]
        public string localizedText;

        //The maximum ports is a soft limit. This is defined by the amount of options setup in the dialog manager
        [SerializeField, Output(dynamicPortList = true), Tooltip("Options user has to select, maximum ports depends on amount defined on UI")]
        public List<DialogOption> options = new List<DialogOption>();
        
        //These aren't ideal but are required as a result of the xNode cloning element issue
        [HideInInspector] public int nCount;
        [HideInInspector] public List<string> optionsKeyTracker = new List<string>();
        
        /// <summary>
        /// Unused, required by xNode
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public override object GetValue(NodePort port)
        {
            return null;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Once a key has been entered, check if it exists in the (default)messages.xml
        /// If it does, load the text.
        /// If not create the entry
        /// </summary>
        private void GetMessage()
        {
            var message = Tools.GetMessage(key);
            if (string.IsNullOrEmpty(message))
            {
                text = null;
                localizedText = null;
                textEditable = true;

                if (!string.IsNullOrEmpty(key))
                {
                    Tools.UpdateMessage(key, string.Empty);
                }
            }
            else
            {
                text = message;
                localizedText = message;
                textEditable = false;
            }
        }

        /// <summary>
        /// NOTICE: Options are not updated via the DialogNode class, unfortunately it had to be done in the
        /// DialogNodeEditor as the ODIN OnValueChanged is blocked by xNodes dynamicPortList 
        /// </summary>
        private void UpdateLocalizationFile()
        {
            Tools.UpdateMessage(key, text);
        }
#endif
    }
}