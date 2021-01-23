using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
 

namespace onboard
{
    public static class Extensions
    {

        public static bool IsMouseOverUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

  
        public static void RunActions(Actions[] actions = null)
        {
            if (actions == null)
            {
#if UNITY_EDITOR
   Debug.Log("warning no action detected ");
#endif
             
            }
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].Act();
            }
        }
    }

}
