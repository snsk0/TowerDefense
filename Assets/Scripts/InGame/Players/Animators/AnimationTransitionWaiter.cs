using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace InGame.Players.Animators
{
    public enum HashType
    {
        Tag,
        Name,
    }

    public static class AnimationTransitionWaiter
    {
        /// <summary>
        /// 指定レイヤーのステートが指定ステートに遷移するまで待つ
        /// </summary>
        /// <param name="layer">アニメーターのレイヤー</param>
        /// <param name="state">遷移後のステート</param>
        /// <param name="animator">レイヤーを持つアニメーター</param>
        /// <param name="token"></param>
        public static async UniTask WaitAnimationTransition(int layerNum, int stateHash, Animator animator, CancellationToken token, HashType hashType=HashType.Tag)
        {
            if (!animator) return;

            await UniTask.WaitUntil(() =>
            {
                if (!animator) return true;
                var stateInfo = animator.GetCurrentAnimatorStateInfo(layerNum);
                int currentStateHash;
                switch (hashType)
                {
                    case HashType.Tag:
                        currentStateHash = stateInfo.tagHash;
                        break;
                    case HashType.Name:
                        currentStateHash = stateInfo.shortNameHash;
                        break;
                    default:
                        currentStateHash = 0;
                        Debug.LogError("無効な引数です");
                        break;
                }
                return currentStateHash == stateHash;
                
            }, cancellationToken: token);
        }

        /// <summary>
        /// ステートが正規時間で指定秒経つまで待つ
        /// </summary>
        /// <param name="waitTime">待ちたい秒(0～1)</param>
        /// <param name="layer">アニメーターのレイヤー</param>
        /// <param name="state">待ちたいステート</param>
        /// <param name="animator">レイヤーを持つアニメーター</param>
        /// <param name="token"></param>
        public static async UniTask WaitStateTime(float waitTime, int layerNum, int stateHash, Animator animator, CancellationToken token, HashType hashType = HashType.Tag)
        {
            if (!animator) return;

            await WaitAnimationTransition(layerNum, stateHash, animator, token, hashType);

            await UniTask.WaitUntil(() =>
            {
                if (!animator) return true;
                var stateInfo = animator.GetCurrentAnimatorStateInfo(layerNum);
                return waitTime <= stateInfo.normalizedTime;
            }, cancellationToken: token);
        }
    }

}