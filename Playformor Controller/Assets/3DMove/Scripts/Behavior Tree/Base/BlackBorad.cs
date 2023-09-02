using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using odin.OdinSerializer;

[System.Serializable]
public class BlackBorad {

    public Vector3 pos;
    public Dictionary<string, object> blackDicData;

    public BlackBorad() {
        blackDicData = new Dictionary<string, object>();
    }

    public bool AddData(string key,object data) {
        if (!blackDicData.ContainsKey(key)) {
            blackDicData.Add(key,data);
            return true;
        }
        else {
            Debug.LogError(string.Format("�ڰ��������Ѵ���Key:{0}�����ݣ��������ʧ��!", key));
            return false;
        }
    }

    public bool RemoveData(string key) {
        return blackDicData.Remove(key);
    }


    public T GetData<T>(string key) {
        var value = GetBlackboardData(key);
        if (value != null) {
            return (T)value;
        }
        else {
            Debug.LogError("����Ĭ��ֵ!");
            return default(T);
        }
    }





    private object GetBlackboardData(string key) {
        object value;
        if (!blackDicData.TryGetValue(key, out value)) {
            Debug.LogError(string.Format("�Ҳ���Key:{0}�ĺڰ�����!", key));
        }
        return value;
    }
}
