Shader "Unlit/Bhol"
{
    Properties
    {
        _Color("Color", Color) = (0,0,0,1)
        _TopColor("Top Color", Color) = (0,1,0,1)
        _OtlWidth("Width", Range(0,100)) = 1
        _Alpha("Alpha", Range(0,1)) = 1
        _BaseColor("Base Color", Color) = (1,1,1,1)
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        //ZTest Always

        //outline
        Pass
        {
            Tags { "RenderType" = "Opaque" "LightMode" = "ForwardBase" }
            Blend SrcAlpha One
            Cull Front
            ZWrite Off
            ZTest Always

            HLSLPROGRAM
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl" 

            float _OtlWidth, _Alpha;
            half4 _Color, _TopColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                half4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = v.vertex;
                o.pos.xyz += normalize(v.normal.xyz) * _OtlWidth * 0.008 * (1.1 * (0.5 - _Time.y % 0.5));
                o.pos = TransformObjectToHClip(o.pos);
                float c = smoothstep(-1.0, 1.0, sin(3.141 * v.normal.z + _Time.w)); //todo
                o.color = _Color * (1.0 - c) + _TopColor * c;
                float t = _Time.y % 0.5;
                o.color.a *= _Alpha * (t + 0.5) * saturate(2.5 - 5.0 * t);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                //clip(-negz(_OtlWidth));
                return i.color;
            }

            ENDHLSL
            }
    }
}
