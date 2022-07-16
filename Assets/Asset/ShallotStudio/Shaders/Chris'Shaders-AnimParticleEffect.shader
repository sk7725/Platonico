// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Chris'Shaders/AnimParticleEffect" {
Properties{
		_ColorIntensity01 ("Main Color", Color) = (0,0,0,0)
		_Tex01 ("Anim Texture 01", 2D) = "black" {}
		_AnimVector ("X and Y axis' speed", Vector) = (0,0,0,0)
		_FlashTime ("Flash Time", Float) = 0
		_BirightRange ("Brightness Range", Range(0,0.5)) = 0
	}
	
	subshader{
		Tags {
		"Queue"="Transparent"
		"IgnoreProjector"="True"
		"RenderType"="Transparent"
		"PreviewType"="Plane"
	}
	Cull Off Lighting Off ZWrite Off  Fog { Mode Off }
	Blend One One
		
		Pass{
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			sampler2D _Tex01;
			uniform half4 _ColorIntensity01;
			uniform float4 _AnimVector;
			float4 _MainTex_ST;
			float _FlashTime;
			fixed _BirightRange;
			
			struct vertexInput{
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			
			struct vertexOutput{
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
			};
			
			
			
			vertexOutput vert (vertexInput i)
			{
				vertexOutput o;
				
				o.pos = UnityObjectToClipPos(i.vertex);
				o.uv = i.texcoord;
				
				return o;
			}
			
			float4 frag(vertexOutput i) :COLOR
			{
				float xsp = _AnimVector.x * _Time;
				float ysp = _AnimVector.y * _Time;
				float flash = abs(sin(_FlashTime * _Time + 90)) + _BirightRange;
				
				return tex2D(_Tex01, float2(i.uv.x + xsp, i.uv.y + ysp)) * _ColorIntensity01 * flash;
			}
			
			ENDCG
		}
	}
	FallBack "Diffuse"
}
