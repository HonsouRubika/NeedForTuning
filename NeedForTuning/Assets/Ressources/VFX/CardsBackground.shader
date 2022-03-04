Shader "Unlit/CardsBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Color2("Color2", Color) = (0,0,0,1)
        _NbPattern("NbPattern", float) = 50
        _ThicknessPattern("ThicknessPattern", float) = 0.1
        _OpacityMask("OpacityMask", float) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _Color2;
            float _Pattern;
            float2 _UVPattern;
            float _MaskPattern;
            float _NbPattern;
            float _ThicknessPattern;
            float4 _ColoredPattern;
            float _OpacityMask;

            float2 Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation /*, out float2 Out*/)
            {
                //rotation matrix
                UV -= Center;
                float s = sin(Rotation);
                float c = cos(Rotation);

                //center rotation matrix
                float2x2 rMatrix = float2x2(c, -s, s, c);
                rMatrix *= 0.5;
                rMatrix += 0.5;
                rMatrix = rMatrix * 2 - 1;

                //multiply the UVs by the rotation matrix
                UV.xy = mul(UV.xy, rMatrix);
                UV += Center;

                //Out = UV;
                return UV;
            }

            float2 Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale /*, out float2 Out*/)
            {
                float2 delta = UV - Center;
                float radius = length(delta) * 2 * RadialScale;
                float angle = atan2(delta.x, delta.y) * 1.0 / 6.28 * LengthScale;
                //Out = float2(radius, angle);
                return float2(radius, angle);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                _UVPattern = Unity_PolarCoordinates_float(Unity_Rotate_Radians_float(i.uv, float2(0.5,0.5), _Time.g), float2(0.5,0.5), 1, 1);
                _MaskPattern = (1 - _UVPattern.r) + _OpacityMask;
                _Pattern = step(_ThicknessPattern, sin(_UVPattern.g * _NbPattern));
                _ColoredPattern = lerp(_Color, _Color2,_Pattern) * _MaskPattern;
                
                // sample the texture
                fixed4 col = _ColoredPattern; //tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
