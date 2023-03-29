using UnityEngine;


namespace Runtime.Enemy.Animation
{
    public class EnemyDissolve : MonoBehaviour
    {
        //パラメータ名
        private static readonly string parameterName = "_Cutoff";


        //コンポーネント
        [SerializeField] private Material dissolveMaterial;
        [SerializeField] private new Renderer renderer;

        //設定
        [SerializeField] private float time;
        [SerializeField] private AnimationCurve curve;

        //マテリアルの保持
        private Material defaultMaterial;

        //フラグ
        public bool isExcuting { get; private set; }
        private float timer;



        //初期化処理
        public void Awake()
        {
            //デフォルトマテリルはsharedMaterialで取得する
            defaultMaterial = renderer.sharedMaterial;
            dissolveMaterial.SetFloat(parameterName, 0.0f); //ディゾルブを初期化
            timer = 0;
            isExcuting = false;
        }

        //消される時に初期化する
        public void OnDisable()
        {
            isExcuting = false;
            dissolveMaterial.SetFloat(parameterName, 0.0f);
            renderer.material = defaultMaterial;
            timer = 0;
        }

        //実行
        public void StartDissolve()
        {
            isExcuting = true;
            renderer.material = dissolveMaterial;
        }




        private void Update()
        {
            if (isExcuting)
            {
                timer += Time.deltaTime;

                if(timer >= time)
                {
                    timer = time;
                    isExcuting = false;
                }
                renderer.material.SetFloat(parameterName, curve.Evaluate(timer/time));
            }
        }

    }
}
