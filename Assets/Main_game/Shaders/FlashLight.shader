﻿	Shader "VLS2D/FlashLight" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _RotationSpeed ("Rotation Speed", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            ZTest Always
            ZWrite Off
            
            Fog {Mode Off}
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _RotationSpeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
/////// Vectors:
////// Lighting:
////// Emissive:
				float rotateZ = 0;
                float4 node_5695 = _Time + _TimeEditor;
                float node_68_ang = node_5695.g;
                float node_68_spd = _RotationSpeed;
                /*float node_68_cos = cos(node_68_spd*node_68_ang);
                float node_68_sin = sin(node_68_spd*node_68_ang);*/
				float node_68_cos = cos(_RotationSpeed);
				float node_68_sin = sin(_RotationSpeed);
                float2 node_68_piv = float2(0.5,0.5);
                float2 node_68 = (mul(i.uv0-node_68_piv,float2x2( node_68_cos, -node_68_sin, node_68_sin, node_68_cos))+node_68_piv);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_68, _MainTex));
                float3 emissive = saturate((i.vertexColor.rgb*_MainTex_var.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

