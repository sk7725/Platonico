// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.06 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.06;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:1,uamb:True,mssp:True,lmpd:False,lprd:False,rprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,blpr:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4963,x:32719,y:32712,varname:node_4963,prsc:2|spec-773-OUT,gloss-773-OUT,emission-1322-OUT;n:type:ShaderForge.SFN_Tex2d,id:1765,x:31939,y:32455,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_1765,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:6428,x:31939,y:32242,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_6428,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1612,x:32186,y:32427,varname:node_1612,prsc:2|A-6428-RGB,B-1765-RGB;n:type:ShaderForge.SFN_Slider,id:773,x:32295,y:32723,ptovrint:False,ptlb:Spec,ptin:_Spec,varname:node_773,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Tex2d,id:7138,x:32192,y:33200,ptovrint:False,ptlb:AnimTex,ptin:_AnimTex,varname:node_7138,prsc:2,ntxv:0,isnm:False|UVIN-4989-OUT;n:type:ShaderForge.SFN_Vector4Property,id:5945,x:31311,y:33163,ptovrint:False,ptlb:XYSpeed,ptin:_XYSpeed,varname:node_5945,prsc:2,glob:False,v1:0,v2:0,v3:0,v4:0;n:type:ShaderForge.SFN_TexCoord,id:1775,x:31480,y:32740,varname:node_1775,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:3551,x:31356,y:32916,varname:node_3551,prsc:2;n:type:ShaderForge.SFN_Color,id:8333,x:32157,y:32947,ptovrint:False,ptlb:AnimColor,ptin:_AnimColor,varname:node_8333,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:2928,x:32392,y:33085,varname:node_2928,prsc:2|A-8333-RGB,B-7138-RGB;n:type:ShaderForge.SFN_Append,id:4989,x:31998,y:33134,varname:node_4989,prsc:2|A-5071-OUT,B-7345-OUT;n:type:ShaderForge.SFN_Add,id:5071,x:31840,y:32989,varname:node_5071,prsc:2|A-1775-U,B-5581-OUT;n:type:ShaderForge.SFN_Add,id:7345,x:31815,y:33148,varname:node_7345,prsc:2|A-1775-V,B-5802-OUT;n:type:ShaderForge.SFN_Multiply,id:5581,x:31595,y:32989,varname:node_5581,prsc:2|A-3551-T,B-5945-X;n:type:ShaderForge.SFN_Multiply,id:5802,x:31583,y:33209,varname:node_5802,prsc:2|A-3551-T,B-5945-Y;n:type:ShaderForge.SFN_Slider,id:2623,x:31749,y:32734,ptovrint:False,ptlb:MainTexRate,ptin:_MainTexRate,varname:node_2623,prsc:2,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:837,x:32182,y:32795,varname:node_837,prsc:2|A-1612-OUT,B-2623-OUT;n:type:ShaderForge.SFN_Add,id:1322,x:32535,y:32861,varname:node_1322,prsc:2|A-837-OUT,B-2928-OUT;proporder:6428-1765-8333-7138-773-2623-5945;pass:END;sub:END;*/

Shader "Chris'Shaders/UVAnimSpec" {
    Properties {
        _MainColor ("MainColor", Color) = (0.5,0.5,0.5,1)
        _MainTex ("MainTex", 2D) = "white" {}
        _AnimColor ("AnimColor", Color) = (0.5,0.5,0.5,1)
        _AnimTex ("AnimTex", 2D) = "white" {}
        _Spec ("Spec", Range(0, 1)) = 0
        _MainTexRate ("MainTexRate", Range(0, 1)) = 0
        _XYSpeed ("XYSpeed", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float _Spec;
            uniform sampler2D _AnimTex; uniform float4 _AnimTex_ST;
            uniform float4 _XYSpeed;
            uniform float4 _AnimColor;
            uniform float _MainTexRate;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Spec;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Spec,_Spec,_Spec);
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
////// Emissive:
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_3551 = _Time + _TimeEditor;
                float2 node_4989 = float2((i.uv0.r+(node_3551.g*_XYSpeed.r)),(i.uv0.g+(node_3551.g*_XYSpeed.g)));
                float4 _AnimTex_var = tex2D(_AnimTex,TRANSFORM_TEX(node_4989, _AnimTex));
                float3 emissive = (((_MainColor.rgb*_MainTex_var.rgb)*_MainTexRate)+(_AnimColor.rgb*_AnimTex_var.rgb));
/// Final Color:
                float3 finalColor = specular + emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ForwardAdd"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            Fog { Color (0,0,0,0) }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _MainColor;
            uniform float _Spec;
            uniform sampler2D _AnimTex; uniform float4 _AnimTex_ST;
            uniform float4 _XYSpeed;
            uniform float4 _AnimColor;
            uniform float _MainTexRate;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(unity_ObjectToWorld, float4(v.normal,0)).xyz;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = _Spec;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 specularColor = float3(_Spec,_Spec,_Spec);
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow);
                float3 specular = directSpecular * specularColor;
/// Final Color:
                float3 finalColor = specular;
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
