﻿using System.Reflection;
using System.Collections.Generic;

namespace GlobalEvents {

    using Type = System.Type;

    public static class RelativeEventHandler {

        private static IDictionary<string, IDictionary<object, IList<string>>> relativeEventTable = new Dictionary<string, IDictionary<object, IList<string>>>();

        private static bool TryAddMethod(string method, ref IList<string> subscribedMethods) {
            if (!subscribedMethods.Contains(method)) {
                subscribedMethods.Add(method);
                return true;
            } else {
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarningFormat("{0} has already been subscribed to the relative event table.", method);
#endif
                return false;
            }
        }

        private static void AddRelativeEvent(object key, string method, ref IDictionary<object, IList<string>> relativeEvent) {
            var subscribedMethods = default(IList<string>);
            if (relativeEvent.TryGetValue(key, out subscribedMethods)) {
                TryAddMethod(method, ref subscribedMethods);
                relativeEvent[key] = subscribedMethods;
            } else {
                var methods = new List<string>();
                methods.Add(method);
                relativeEvent.Add(key, methods);
                UnityEngine.Debug.LogFormat("{0}, {1}", key, method);
            }
        }

        private static IDictionary<object, IList<string>> GetRelativeEventTable(string eventName) {
            var eventTable = default(IDictionary<object, IList<string>>);
            relativeEventTable.TryGetValue(eventName, out eventTable);
            return eventTable;
        }

        private static void InvokeEvent(Type type, object instance, IList<string> methods, object[] args, Type[] typeArgs) {
            foreach(var method in methods) {
                try {
                    UnityEngine.Debug.Log(method);
                    var methodInfo = type.GetMethod(
                            method,
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod,
                            null,
                            typeArgs,
                            null);
                    UnityEngine.Debug.Log(methodInfo == null);
                    methodInfo.Invoke(instance, args);
                } catch (System.Exception err) {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError(err);
#endif
                }
            }
        }

        public static void InvokeEvent(string eventName, object instance, object[] args, Type[] typeArgs) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                var subscribedMethods = default(IList<string>);

                if (eventTable.TryGetValue(instance, out subscribedMethods)) {
                    var instanceType = instance.GetType();
                    InvokeEvent(instanceType, instance, subscribedMethods, args, typeArgs);
                }
            }
        }

        public static void SubscribeEvent(string eventName, object instance, string method) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                AddRelativeEvent(instance, method, ref eventTable);
            } else {
                IDictionary<object, IList<string>> relativeEvent = new Dictionary<object, IList<string>>();
                AddRelativeEvent(instance, method, ref relativeEvent);
                relativeEventTable.Add(eventName, relativeEvent);
            }
        }

        public static void UnsubscribeEvent(string eventName, object instance, string method) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                var subscribedMethods = default(IList<string>);
                if (eventTable.TryGetValue(instance, out subscribedMethods)) {
                    subscribedMethods.Remove(method);
                }
            }
        }
    }
}
