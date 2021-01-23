using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace onboard
{
    public class OnStartInterract : MonoBehaviour
    {

        #region properties
        [SerializeField]
        Actions[] actions;
        [SerializeField]
        bool hideOnDisable;
        #endregion

        #region methodes


        private void OnEnable()
        {
            Extensions.RunActions(actions);
        }


     
        #endregion
        
    }

}
