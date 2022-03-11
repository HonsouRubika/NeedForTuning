Shader "Custom/TuktukInCardsRare"
{
    Properties
    {
        _Tex ("InputTex", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _TexOpacity ("Texture opacity", range(0,1)) = 1
        _Emissive("Emissive", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Stencil{
            Ref 3
            Comp equal
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _Tex;
            float4 _Tex_ST;
            float4 _Color;
            float _TexOpacity;
            float4 _Emissive;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Tex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = lerp(_Color, _Color*tex2D(_Tex, i.uv), _TexOpacity);
                //col *= _Emissive;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
