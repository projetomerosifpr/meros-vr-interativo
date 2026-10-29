/*
Shader "AlphaSelfIllum_NoFog_WithNormalMap" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Texture", 2D) = "white" { }
    _NormalMap ("Normal Map", 2D) = "bump" {}
    _LightDir ("Light Direction", Vector) = (0, 0, -1, 0)
}
SubShader {
    Tags { "Queue" = "Transparent" }
    Pass {
        Tags { "LightMode" = "Vertex" }
        Fog { Mode Off }
        
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha 
        
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        float4 _Color;
        sampler2D _MainTex;
        sampler2D _NormalMap;
        float4 _MainTex_ST;
        float4 _NormalMap_ST;
        float4 _LightDir;

        struct appdata {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 tangent : TANGENT;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 lightDirTS : TEXCOORD1;
        };

        v2f vert (appdata v)
        {
            v2f o;
            o.pos = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);

            // Create tangent-to-world matrix
            float3 tangent = normalize(v.tangent.xyz);
            float3 normal = normalize(v.normal);
            float3 binormal = cross(normal, tangent) * v.tangent.w;

            float3x3 TBN = float3x3(tangent, binormal, normal);

            // Light direction in object space (user-defined or fixed)
            float3 worldLightDir = normalize(_LightDir.xyz);
            o.lightDirTS = mul(TBN, worldLightDir); // Transform to tangent space

            return o;
        }

        half4 frag (v2f i) : COLOR
        {
            half4 texCol = tex2D(_MainTex, i.uv);
            half3 normalTS = UnpackNormal(tex2D(_NormalMap, i.uv));
            float NdotL = saturate(dot(normalTS, normalize(i.lightDirTS)));
            return texCol * _Color * NdotL;
        }
        ENDCG
    }
}
Fallback Off
}
*/
Shader "Custom/AlphaSelfIllum_NoFog_WithNormalMap"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" { }
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _LightDir ("Light Direction", Vector) = (0, 0, -1, 0)
    }
    
    SubShader
    {
        Tags 
        { 
            "RenderType" = "Transparent" 
            "Queue" = "Transparent" 
            "RenderPipeline" = "UniversalPipeline" 
        }
        LOD 100

        Pass
        {
            Name "UniversalForward"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _Color;
                float4 _MainTex_ST;
                float4 _NormalMap_ST;
                float4 _LightDir;
            CBUFFER_END

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            TEXTURE2D(_NormalMap);
            SAMPLER(sampler_NormalMap);

            struct Attributes
            {
                float4 vertex   : POSITION;
                float3 normal   : NORMAL;
                float4 tangent  : TANGENT;
                float2 uv       : TEXCOORD0;
            };

            struct Varyings
            {
                float4 pos          : SV_POSITION;
                float2 uv           : TEXCOORD0;
                float3 lightDirTS   : TEXCOORD1;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                
                output.pos = TransformObjectToHClip(input.vertex.xyz);
                output.uv = TRANSFORM_TEX(input.uv, _MainTex);

                // Create tangent-to-object (or world depending on context) matrix
                float3 tangent = normalize(input.tangent.xyz);
                float3 normal = normalize(input.normal);
                float3 binormal = cross(normal, tangent) * input.tangent.w;

                float3x3 TBN = float3x3(tangent, binormal, normal);

                float3 lightDir = normalize(_LightDir.xyz);
                output.lightDirTS = mul(TBN, lightDir);

                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 texCol = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.uv);
                
                half4 normalSample = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, input.uv);
                half3 normalTS = UnpackNormal(normalSample);
                
                float NdotL = saturate(dot(normalTS, normalize(input.lightDirTS)));
                
                return texCol * _Color * NdotL;
            }
            ENDHLSL
        }
    }
    
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}