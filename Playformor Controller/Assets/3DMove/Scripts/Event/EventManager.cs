using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AllEventType {
    Platform,
    Player,
    UI,
}

public class EventManager : MonoBehaviour {
    private static Dictionary<string, List<Delegate>> EventDic = new Dictionary<string, List<Delegate>>();
    //�޲���
    public delegate void CallBack();
    public delegate void CallBack<T>(T parameter);
    public static void AddListener(CallBack callBack, string str) {
        if (EventDic.ContainsKey(str)) {
            for (int i = 0; i < EventDic[str].Count; i++) {
                Delegate item = EventDic[str][i];
                if (item != null && !item.Equals(callBack)) {
                    EventDic[str].Add(callBack);
                }
                else {
                    //throw new Exception($"{str}�����ظ������ȫһ�����¼�");
                }
            }

            //foreach (Delegate item in EventDic[str]) {
            //    if (!item.Equals(callBack)) {
            //        EventDic[str].Add(callBack);
            //    }
            //    else {
            //        throw new Exception($"{str}�����ظ������ȫһ�����¼�");
            //    }
            //}

        }
        else {
            EventDic.Add(str, new List<Delegate>() { callBack });
        }
    }

    public static void RemoveListener(CallBack callBack, string str) {

        if (EventDic.ContainsKey(str)) {
            for (int i = 0; i < EventDic[str].Count; i++) {
                Delegate item = EventDic[str][i];
                if (item != null && item.Equals(callBack)) {
                    EventDic[str].Remove(callBack);
                    EventDic[str].Clear();
                }
            }
        }
        else {
            throw new Exception($"{str}��ǰϵͳδע����¼�");
        }
    }

    public static void AddListener<T>(CallBack<T> callBack, string str) {
        if (EventDic.ContainsKey(str)) {
            for (int i = 0; i < EventDic[str].Count; i++) {
                Delegate item = EventDic[str][i];
                if (item != null && !item.Equals(callBack)) {
                    EventDic[str].Add(callBack);
                }
                else {
                    //throw new Exception($"{str}�����ظ������ȫһ�����¼�");
                }
            }

        }
        else {
            EventDic.Add(str, new List<Delegate>() { callBack });
        }
    }

    public static void RemoveListener<T>(CallBack<T> callBack, string str) {
        if (EventDic.ContainsKey(str)) {
            for (int i = 0; i < EventDic[str].Count; i++) {
                Delegate item = EventDic[str][i];
                if (item != null && item.Equals(callBack)) {
                    EventDic[str].Remove(callBack);
                    EventDic[str].Clear();
                }
            }
        }
        else {
            throw new Exception($"{str}��ǰϵͳδע����¼�");
        }
    }



    public static void Dispatch(string str) {

        if (EventDic.ContainsKey(str)) {
            for (int i = 0; i < EventDic[str].Count; i++) {
                Delegate item = EventDic[str][i];
                CallBack callBack = item as CallBack;
                callBack?.Invoke();
            }
            //foreach (Delegate item in EventDic[str]) {
            //    CallBack callBack = item as CallBack;
            //    callBack?.Invoke();
            //}
        }
        else {
            throw new Exception($"{str}��ǰ�¼�ϵͳ���������¼�");
        }
    }

    public static void Dispatch<T>(string str, T parameter) {
        if (EventDic.ContainsKey(str)) {
            foreach (Delegate item in EventDic[str]) {
                CallBack<T> callBack = item as CallBack<T>;
                callBack?.Invoke(parameter);
            }
        }
        else {
            throw new Exception($"{str}��ǰ�¼�ϵͳ���������¼�");
        }
    }
}
