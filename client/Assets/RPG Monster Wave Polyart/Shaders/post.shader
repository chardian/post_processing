Shader "my/post"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _SplitTex;

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 split_col = tex2D(_SplitTex, i.uv);
                if (split_col.r > 0.9)
                {
                    //monster
                    float grey = col.r * 0.3 + col.g * 0.4 + col.b * 0.3;
                    return fixed4(grey, grey, grey, 1);
                }
                if (split_col.g > 0.9)
                {
                    //hero
                    return col;
                }
                if (split_col.b > 0.9)
                {
                    //bg
                    return fixed4(col.r, col.r, col.b, 1);
                }
                return col;
            }
            ENDCG
        }
    }
}