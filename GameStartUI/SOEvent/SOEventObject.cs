/// author:  helder.sousa.daniel@gmail.com
/// version: 202309111810

using UnityEngine;

namespace hadrack.gpst.core.events
{
    /// <summary>
    /// SOEventObject used to signal something and transfer Object data
    ///
    /// Notes:
    /// Need to have each subclass in its own script
    /// to avoid missing script warning in SO asset
    /// 
    /// Need to define for each message argument type a non generic
    /// subclass because generic types cannot be defined in create menu
    /// </summary>
    [CreateAssetMenu(menuName = "SO/SOEvent/object", order = 50)]
    public class SOEventObject : SOEvent<System.Object> { }
}