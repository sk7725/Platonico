Shader "Platonics/AssignmentBeam"
{
    Properties
    {
        _MainTex("Main Tex", 2D) = "white" {}
    }  

	SubShader
	{  

        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"          
            "Queue"="Transparent"
        }

    	Pass
    	{  		
     	Name "Universal Forward"
        Tags { "LightMode" = "UniversalForward" }

        Blend SrcAlpha OneMinusSrcAlpha

       	HLSLPROGRAM

        #pragma prefer_hlslcc gles
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag

       	#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"        	
   	
        float4 _ShieldColor;
        float _ShieldPower;

        struct VertexInput
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        struct VertexOutput
        {
            float4 vertex : SV_POSITION;
            float3 normal : NORMAL;
            float3 viewDir : TEXCOORD0;
      	};

      	VertexOutput vert(VertexInput v)
        {
            VertexOutput o;      
            o.vertex = TransformObjectToHClip(v.vertex.xyz);
            o.normal = TransformObjectToWorldNormal(v.normal);
            o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(GetObjectToWorldMatrix(), float4(v.vertex.xyz, 1.0)).xyz);

            return o;
        }

        half4 frag(VertexOutput i) : SV_Target
        { 
            float NdotL = pow(1 - dot(i.normal, i.viewDir), _ShieldPower);
            return _ShieldColor * NdotL;  
        }
        ENDHLSL  
    	}
     }

}
