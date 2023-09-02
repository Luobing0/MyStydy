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
            Debug.LogError(string.Format("黑板数据里已存在Key:{0}的数据，添加数据失败!", key));
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
            Debug.LogError("返回默认值!");
            return default(T);
        }
    }





    private object GetBlackboardData(string key) {
        object value;
        if (!blackDicData.TryGetValue(key, out value)) {
            Debug.LogError(string.Format("找不到Key:{0}的黑板数据!", key));
        }
        return value;
    }
}
