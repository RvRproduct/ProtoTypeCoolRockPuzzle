using UnityEngine;

// 使脚本在编辑模式下运行
[ExecuteInEditMode]
public class AdjustTiling : MonoBehaviour
{
    public float initSize = 1.0f;
    private Vector3 initialScale;
    private Renderer rend;
    private bool initialized = false;

    void OnEnable()
    {
        // 初始化变量
        Initialize();
    }

    void Update()
    {
        // 如果我们在编辑模式下，且变量没有初始化
        if (!Application.isPlaying && !initialized)
        {
            Initialize();
        }

        // 检查初始缩放以避免除零错误
        if (initialScale.x != 0 && initialScale.y != 0 && rend != null)
        {
            // 计算缩放比例
            Vector2 scaleChange = new Vector2(transform.localScale.x * initSize, transform.localScale.z * initSize);
            // 动态调整材质的 Tiling 属性
            rend.material.SetTextureScale("_MainTex", scaleChange);
        }
    }

    void Initialize()
    {
        initialScale = transform.localScale;
        rend = GetComponent<Renderer>();
        initialized = true;
    }
}