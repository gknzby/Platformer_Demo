Shader "Kayura/BGSlider"
{
    Properties
    {
        _LayerFarTex("Far Layer", 2D) = "gray" {}
        _LayerMidTex("Mid Layer", 2D) = "gray" {}
        _LayerNearTex("Near Layer", 2D) = "gray" {}
        
        _LayerFarSpeed("Far Layer Speed", range(0,1)) = 0.56
        _LayerMidSpeed("Mid Layer Speed", range(0,1)) = 0.72
        _LayerNearSpeed("Near Layer Speed", range(0,1)) = 0.86
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

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 wPos : TEXCOORD1;
            };

            sampler2D _LayerFarTex;
            sampler2D _LayerMidTex;
            sampler2D _LayerNearTex;

            float _LayerFarSpeed;
            float _LayerMidSpeed;
            float _LayerNearSpeed;

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = v.uv;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.wPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float InvLerp(float s, float d, float x)
            {
                return (x - s) / (d - s);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 scales = float2(length(unity_ObjectToWorld._m00_m10_m20), length(unity_ObjectToWorld._m01_m11_m21));

                float2 layerFarSpeed = float2(1 - _LayerFarSpeed, 1 - _LayerFarSpeed);
                float2 layerMidSpeed = float2(1 - _LayerMidSpeed, 1 - _LayerMidSpeed);
                float2 layerNearSpeed = float2(1 - _LayerNearSpeed, 1 - _LayerNearSpeed);

                float2 layerFarPos = float2(i.wPos / scales * (1 - layerFarSpeed) + i.uv * layerFarSpeed);
                float2 layerMidPos = float2(i.wPos / scales * (1 - layerMidSpeed) + i.uv * layerMidSpeed);
                float2 layerNearPos = float2(i.wPos / scales * (1 - layerNearSpeed) + i.uv * layerNearSpeed);

                float4 layerFarColor = tex2D(_LayerFarTex, layerFarPos);
                float4 layerMidColor = tex2D(_LayerMidTex, layerMidPos);
                float4 layerNearColor = tex2D(_LayerNearTex, layerNearPos);

                float4 finalColor = layerFarColor * ((1 - layerMidColor.a) * (1 - layerNearColor.a)) + layerMidColor * (layerMidColor.a * (1 - layerNearColor.a)) + layerNearColor * layerNearColor.a;

                return finalColor;
            }
            ENDCG
        }
    }
}
