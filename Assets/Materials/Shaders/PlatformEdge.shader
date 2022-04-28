Shader "Kayura/PlatformEdge"
{
    Properties
    {
        _EdgeTex("Edge Texture", 2D) = "gray" {}
        _FillTex("Fill Texture", 2D) = "gray" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : TEXCOORD1;
                float3 wPos : TEXCOORD2;
                float3 tangent : TEXCOORD3;
            };

            sampler2D _EdgeTex;
            sampler2D _FillTex;

            float InvLerp(float s, float d, float x)
            {
                return (x - s) / (d - s);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(v.normal);
                o.tangent = v.tangent.xyz; // UnityObjectToWorldDir(v.tangent.xyz);
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 sinTan = sin(i.tangent);
                float2 cosTan = cos(i.tangent);
                float normal = step(0.3, sinTan.x);

                float4 finalColor = tex2D(_EdgeTex, i.uv) * normal + tex2D(_FillTex, i.uv) * (1 - normal);

                return finalColor;
            }
            ENDCG
        }
    }
}