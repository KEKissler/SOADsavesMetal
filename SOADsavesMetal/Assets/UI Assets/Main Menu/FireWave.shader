// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// amalgamation of shader tutorials by Minions Art and @febucci

Shader "Custom/FireWave"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_Intensity("Intensity", float) = 0.1
		_Multiplier("Multiplier", float) = 0.4
		_Color("Color", Color) = (1,1,1,1)
		_Color2("Color2", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
		Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha One


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _NoiseTex;
			sampler2D _DistortionTex;
			float _Intensity;
			float _Multiplier;
			float _Transparent;
			float4 _Color;
			float4 _Color2;



			fixed4 frag (v2f i) : SV_Target
			{
			float4 Distort = tex2D(_DistortionTex, float2(i.uv.x + _Time.y*0.1, i.uv.y));
			float4 Result = tex2D(_NoiseTex, float2(i.uv.x - Distort.g, i.uv.y - Distort.r - _Time.x*_Multiplier));
			//Result.g = (Result.r + Result.g + Result.b) / 3;
			float4 gradient = lerp(float4(1.0f,1.0f,1.0f,1.0f), float4(0,0,0,1.0f), i.uv.y);

			float steping = step(Result.x, gradient.x);

			Result += steping*_Intensity;
			Result *= gradient*gradient;
			

			float4 c_gradient = lerp(_Color, _Color2, 0.5f);
			Result *= c_gradient;


			Result.a = step(0.8f, Result.a);

			return Result;	//return col;
			}
			ENDCG
		}
	}
}
