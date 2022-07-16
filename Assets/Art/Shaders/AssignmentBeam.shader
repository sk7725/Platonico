Shader "Platonics/AssignmentBeam"
{
    Properties
    {
        _MainTex("Main Tex", 2D) = "white" {}
        _TintColor("Tint Color", color) = (1, 1, 1, 1)
	    _Intensity("Intensity", Range(0, 20)) = 0.5
        _Alpha("AlphaCut", Range(0,1)) = 0.5
    }  

	SubShader
	{  

        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="TransparentCutout"          
            "Queue"="Alphatest"
        }

    	Pass
    	{  		
     	Name "Universal Forward"
        Tags { "LightMode" = "UniversalForward" }

       	HLSLPROGRAM

        #pragma prefer_hlslcc gles
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag

       	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"        	
   	
        float4 _TintColor;
        float _Intensity;

        float4 _MainTex_ST;
        Texture2D _MainTex;
        SamplerState sampler_MainTex;
        float _Alpha;

        struct VertexInput
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct VertexOutput
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
      	};

      	VertexOutput vert(VertexInput v)
        {
            VertexOutput o;      
            o.vertex = TransformObjectToHClip(v.vertex.xyz);
            o.uv = v.uv * _MainTex_ST.xy + _MainTex_ST.zw;

            o.uv.y += _Time.y;
            o.uv.x += _Time.x;

            return o;
        }

        half4 frag(VertexOutput i) : SV_Target
        { 
            float4 color = _MainTex.Sample(sampler_MainTex, i.uv) * _Intensity; 
            clip(color.a - _Alpha);
            return color;  
        }
        ENDHLSL  
    	}
     }

}
