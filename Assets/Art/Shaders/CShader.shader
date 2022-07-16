Shader "Platonics/CShader"
{
    Properties
    {
        _TintColor("Tint Color", color) = (1, 1, 1, 1)
	    _Intensity("Intensity", Range(0, 20)) = 0.5
        _Amplitude("Amplitude", Float) = 0
        _Frequency("Frequency", Float) = 1
    }  

	SubShader
	{  

        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"          
            "Queue"="Geometry"
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
        float _Amplitude;
        float _Frequency;

        struct VertexInput
        {
            float4 vertex : POSITION;
        };

        struct VertexOutput
        {
            float4 vertex : SV_POSITION;
      	};

      	VertexOutput vert(VertexInput v)
        {
            VertexOutput o;      
            _Amplitude *= (1 + sin(_Time.z * _Frequency)) * 0.001;
            o.vertex = TransformObjectToHClip(v.vertex.xyz + normalize(v.vertex.xyz) * _Amplitude);

            return o;
        }

        half4 frag(VertexOutput i) : SV_Target
        { 
            float4 color = _TintColor * _Intensity;
            return color;  
        }
        ENDHLSL  
    	}
     }

}
