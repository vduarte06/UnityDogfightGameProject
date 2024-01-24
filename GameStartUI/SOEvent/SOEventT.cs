/// author:  helder.sousa.daniel@gmail.com
/// version: 202309111810

using System;
using UnityEngine;

namespace hadrack.gpst.core.events
{

    /// <summary>
    /// SOEvent<T> used to signal something and transfer data
    /// T data is transferred along with the SOEvent
    /// </summary>
    public abstract class SOEvent<T> : ScriptableObject
    {
        private Action<T> observers;

        public void invoke(T arg) { observers?.Invoke(arg); }

        public void subscribe  (Action<T> observer) { observers += observer; }
        public void unsubscribe(Action<T> observer) { observers -= observer; }
    }
    
}