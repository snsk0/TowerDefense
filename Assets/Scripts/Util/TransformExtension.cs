using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    /// <summary>
    /// 指定した文字を含む名前の子が存在すれば返す。存在しないときnullを返す。
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public static Transform GetChild(this Transform transform, string keyword)
    {
        var count = transform.childCount;
        if (count == 0)
        {
            if(transform.parent==null)
                Debug.LogWarning("子が存在しません");
            return null;
        }

        for (int i = 0; i < count; i++)
        {
            
            var child = transform.GetChild(i);

            var result = child.GetChild(keyword);
            if (result != null)
                return result;

            var IsContain = child.name.Contains("Head");
            if (IsContain)
                return child;
        }

        if(transform.parent==null)
            Debug.LogWarning($"「{keyword}」を含む子は見つかりませんでした");
        return null;
    }
}
