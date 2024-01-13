using System.Collections.Generic;
using UnityEngine;

namespace Devolvist.UnityReusableSolutions.SaveLoad
{
    public class RegisteredSavableObjectsStorage : ScriptableObject
    {
        public static List<ISavable> Objects;
    }
}