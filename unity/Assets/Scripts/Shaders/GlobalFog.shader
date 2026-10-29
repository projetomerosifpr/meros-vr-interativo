Shader "Hidden/GlobalFog"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.5,0.5,0.5,1)
        _FogDensity ("Fog Density", Float) = 0.01
        _FogStart ("Fog Start", Float) = 0
        _FogEnd ("Fog End", Float) = 50
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            float4 _FogColor;
            float _FogDensity;
            float _FogStart;
            float _FogEnd;

            float4 frag(v2f_img i) : SV_Target
            {
                // cor original
                float4 col = tex2D(_MainTex, i.uv);

                // depth normalizado
                float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
                float depth01 = Linear01Depth(rawDepth);
                float depth = depth01 * _ProjectionParams.z; // converte p/ distância real

                // fator de fog mais suave
                float fogFactor = smoothstep(_FogStart, _FogEnd, depth);
                fogFactor = saturate(fogFactor * _FogDensity);

                // aplica fog multiplicativo (simula água turva)
                ///col.rgb = lerp(col.rgb, col.rgb * _FogColor.rgb, fogFactor);
                col.rgb = lerp(col.rgb, lerp(col.rgb, _FogColor.rgb, 0.5), fogFactor);

                return col;
            }
            ENDHLSL
        }
    }
}
/*Shader "Hidden/UnderwaterFog"
{
    Properties
    {
        _FogColor ("Fog Color", Color) = (0.5,0.5,0.5,1)
        _FogDensity ("Fog Density", Float) = 0.5
        _FogStart ("Fog Start", Float) = 1
        _FogEnd ("Fog End", Float) = 30
        _WaterLevel ("Water Level (Y)", Float) = 0
    }

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            float4 _FogColor;
            float _FogDensity;
            float _FogStart;
            float _FogEnd;
            float _WaterLevel;

            // ✅ NÃO declarar _WorldSpaceCameraPos, já existe

            float4 frag(v2f_img i) : SV_Target
            {
                float4 col = tex2D(_MainTex, i.uv);

                // aplica só se a camera está abaixo da água
                if (_WorldSpaceCameraPos.y < _WaterLevel)
                {
                    float rawDepth = SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv);
                    float depth01 = Linear01Depth(rawDepth);
                    float depth = depth01 * _ProjectionParams.z;

                    // transição suave
                    float fogFactor = smoothstep(_FogStart, _FogEnd, depth);
                    fogFactor = saturate(fogFactor * _FogDensity);

                    // mistura aditiva em vez de sobrescrever (mantém detalhes)
                    col.rgb = lerp(col.rgb, lerp(col.rgb, _FogColor.rgb, 0.5), fogFactor);
                }

                return col;
            }
            ENDHLSL
        }
    }
}
*/