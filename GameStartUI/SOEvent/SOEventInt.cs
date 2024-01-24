/// author:  helder.sousa.daniel@gmail.com
/// version: 202309111810

using System;
using UnityEngine;

namespace hadrack.gpst.core.events
{

    /// <summary>
    /// SOEventInt used to signal something and transfer int data
    ///
    /// Notes:
    /// Need to have each subclass in its own script
    /// to avoid missing script warning in SO asset
    /// 
    /// Need to define for each message argument type a non generic
    /// subclass because generic types cannot be defined in create menu
    /// </summary>
    [CreateAssetMenu(menuName = "SO/SOEvent/int", order = 20)]
    public class SOEventInt : SOEvent<int> { }
    
}