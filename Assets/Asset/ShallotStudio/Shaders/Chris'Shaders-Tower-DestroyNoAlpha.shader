Shader "Chris'Shaders/Tower-DestroyNoAlpha" {
Properties {
	_MainColor ("Main Color", Color) = (1,1,1,1)
	_CustomColor ("Custom Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Ramp("Destroy Color Range", Range(0,1)) = 0
}
SubShader {
	Tags { "RenderType"="Opaque" }
	Lighting Off
	ZWrite On
	ZTest LEqual
	LOD 200

CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _MainColor;
fixed4 _CustomColor;
float _Ramp;

struct Input {
	float2 uv_MainTex;
	float4 color    : COLOR;
};

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = (tex2D(_MainTex, IN.uv_MainTex) * _MainColor) * (IN.color + _Ramp);
	o.Emission = c.rgb;
	//o.Alpha = c.a;
}
ENDCG
}

Fallback "Diffuse"
}
