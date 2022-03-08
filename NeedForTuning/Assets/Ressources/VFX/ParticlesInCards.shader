Shader "Unlit/ParticlesInCards"
{
    Properties
    {
        _Tex("InputTex", 2D) = "white" {}
        //_Color("Color", Color) = (1,1,1,1)
        _Contrast("Contrast", float) = 1
        _Intensity("HDRIntesity", float) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType"="Transparent" }
        LOD 100

            Zwrite Off
            Blend SrcAlpha OneMinusSrcAlpha

        Stencil{
            Ref 1
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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            sampler2D _Tex;
            float4 _Tex_ST;
            //float4 _Color;
            float _Contrast;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Tex);
                o.color = v.color* _Intensity;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = i.color*tex2D(_Tex, i.uv)*_Contrast;
                //col.rgb *= col.a;
     
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
