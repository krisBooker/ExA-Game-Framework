using System.Collections;
using EventSystem.VisualEditor.Graphs;
using UnityEngine;

namespace EventSystem.Triggers
{
    /// <summary>
    /// Attach to a gameObject with a trigger collider, will be triggered on Player character entry
    /// </summary>
    public class EntryTrigger : MonoBehaviour
    {
        public EventSequenceSceneGraph triggerEventSequence;
        private EventTimelineParser _triggerEventTimelineParser;

        [Tooltip("Should the trigger event sequence be replayable? If unchecked event sequence can only run once.")]
        public bool resetTriggerSequence;

        private bool _hasTriggered;

        public IEnumerator BeginTriggerEvent()
        {
            if (triggerEventSequence == null)
                yield return null;

            if (resetTriggerSequence && _hasTriggered)
                yield return null;

            _hasTriggered = true;

            //Add trigger event timeline parser
            if (_triggerEventTimelineParser == null)
            {
                _triggerEventTimelineParser = gameObject.AddComponent<EventTimelineParser>();
            }

            //Start trigger event sequence
            StartCoroutine(_triggerEventTimelineParser.StartEventSequence(triggerEventSequence));
            yield return new WaitUntil(_triggerEventTimelineParser.IsEventSequenceFinished);

            //Reset trigger event sequence 
            if (resetTriggerSequence)
            {
                GameManager.Instance.eventSystemManager.ResetEventSequenceSceneGraph(triggerEventSequence);
            }
        }
    }
}