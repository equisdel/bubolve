Shader "Custom/OnionSkinEffect"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _BlendAmount("Blend Amount", Range(0,1)) = 0.5
    }
        SubShader
        {
            Tags {"Queue" = "Overlay" "RenderType" = "Transparent"}
            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                sampler2D _MainTex;
                float _BlendAmount;

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 currentFrame = tex2D(_MainTex, i.uv);
                    fixed4 previousFrame = tex2D(_MainTex, i.uv + float2(0.002, 0.002)); // Offset to simulate motion trail
                    return lerp(currentFrame, previousFrame, _BlendAmount);
                }
                ENDCG
            }
        }
}
