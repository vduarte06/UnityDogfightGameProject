/// author:  helder.sousa.daniel@gmail.com
/// version: 202309111810

using System;
using UnityEngine;

namespace hadrack.gpst.core.events
{

    /// <summary>
    /// SOEvent SO used to signal something
    /// No data is transferred along with the SOEvent
    /// </summary>
    [CreateAssetMenu(menuName = "SO/SOEvent/call", order=10)]
    public class SOEvent : ScriptableObject
    {
        private Action observers;

        public void invoke() { observers?.Invoke(); }

        public void subscribe  (Action observer) { observers += observer; }
        public void unsubscribe(Action observer) { observers -= observer; }
    }
    
}