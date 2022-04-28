Shader "Kayura/Mace"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "" {}
        _Color1 ("1.Color", Color) = (1, 1, 1, 1)
        _Color2 ("2.Color", Color) = (0.2, 0.2, 0.2, 1)
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color1;
            fixed4 _Color2;

            v2f vert(appdata v)
            {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 GetColor(fixed timeScale, fixed disturb)
            {
                float sinTime = (sin(_Time.y * timeScale + disturb) + 1) / 2;
                return lerp(_Color1, _Color2, sinTime);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float4 tex = tex2D(_MainTex, i.uv);
                
                float mask1 = step(0.5, tex);
                float mask2 = step(0.4, tex);
                float mask3 = step(0.3, tex);
                float mask4 = (1 - mask3);
                mask3 *= (1 - mask2);
                mask2 *= (1 - mask1);


                fixed4 color1 = _Color1 * 0.45;
                fixed4 color2 = _Color2 * 0.2;



                float sinTime;
                sinTime = (sin(_Time.y * 5 + i.uv.y) + 1) / 2;
                fixed4 color3 = lerp((_Color1*0.7 + _Color2*0.3), (_Color1 * 0.1 + _Color2 * 0.9), sinTime);

                sinTime = (sin(_Time.y * 3 + i.uv.x) + 1) / 2;
                fixed4 color4 = lerp((_Color1 * 0.85 + _Color2 * 0.15), (_Color1 * 0.25 + _Color2 * 0.75), sinTime)*0.8;

                return mask1*color1 + mask2*color2 + mask3*color3 + mask4*color4;
            }
            ENDCG
        }
    }
}
