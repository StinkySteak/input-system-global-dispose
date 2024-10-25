using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StinkySteak.Editor
{
    public class InputSystemGlobalDisposer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void DisposeInputSystemEvents()
        {
            string typeName = "UnityEngine.InputSystem.InputActionState";
            string methodName = "SaveAndResetState";

            Assembly assembly = Assembly.GetAssembly(typeof(InputSystem));

            Type internalClassType = assembly.GetType(typeName);

            if (internalClassType == null)
            {
                Debug.LogWarning($"[{nameof(InputSystemGlobalDisposer)}]: Internal class not found.");
                return;
            }

            MethodInfo resetGlobalsMethod = internalClassType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

            if (resetGlobalsMethod == null)
            {
                Debug.LogWarning($"[{nameof(InputSystemGlobalDisposer)}]: Private method ResetGlobals not found.");
                return;
            }

            resetGlobalsMethod.Invoke(null, null);

            Debug.Log($"[{nameof(InputSystemGlobalDisposer)}]: Private method ResetGlobals invoked successfully.");
        }
    }
}
