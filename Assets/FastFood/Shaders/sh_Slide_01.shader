// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X
// source Amplify Shader Editor network: https://www.daniildemchenko.com/fastfood-interior
Shader "FastFood/sh_Slide_01"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_SlideX("SlideX", Float) = 0
		_SlideY("SlideY", Float) = 0
		_Ditail("Ditail", 2D) = "white" {}
		_Power("Power", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{


		Tags { "RenderType"="Opaque" }
		LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0



		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _MainTex;
			uniform float _SlideX;
			uniform float _SlideY;
			uniform sampler2D _Ditail;
			uniform float4 _Ditail_ST;
			uniform float _Power;

			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;

				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				float3 vertexValue =  float3(0,0,0) ;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 appendResult86 = (float4(_SlideX , _SlideY , 0.0 , 0.0));
				float2 panner81 = ( 1.0 * _Time.y * appendResult86.xy + float2( 0,0 ));
				float2 uv8 = i.ase_texcoord.xy * float2( 1,1 ) + panner81;
				float2 uv_Ditail = i.ase_texcoord.xy * _Ditail_ST.xy + _Ditail_ST.zw;


				finalColor = ( tex2D( _MainTex, uv8 ) * tex2D( _Ditail, uv_Ditail ) * _Power );
				return finalColor;
			}
			ENDCG
		}
	}
}
