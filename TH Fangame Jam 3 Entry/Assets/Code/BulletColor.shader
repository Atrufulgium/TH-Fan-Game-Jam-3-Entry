Shader "Custom/BulletColor"
{
    Properties
    {
        [PerRenderData] _MainTex ("Texture", 2D) = "white" {}
		[PerRenderData] _OuterColor ("Outer Color (Red)", Color) = (1,0,0,1)
		[PerRenderData] _InnerColor ("Inner Color (Green)", Color) = (1,1,1,1)
    }
    SubShader
    {
        // No culling or depth, transparancy
        Cull Off ZWrite Off ZTest Always Blend One OneMinusSrcAlpha

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			fixed4 _OuterColor;
			fixed4 _InnerColor;

            fixed4 frag (v2f i) : SV_Target
            {
				// R-channel: outer, G-channel: inner
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 result = col.r * _OuterColor + col.g * _InnerColor;
				// apply transparancy
				result.a = col.a;
				result.rgb *= result.a;
                return result;
            }
            ENDCG
        }
    }
}
