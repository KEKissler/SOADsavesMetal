// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/DistortionWave"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_Intensity("Intensity", float) = 0.1
		_Multiplier("Multiplier", float) = 0.4
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha

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

			sampler2D _MainTex;
			sampler2D _DistortionTex;
			float _Intensity;
			float _Multiplier;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 dv = float2(i.uv.x + _Time.x * _Multiplier, i.uv.y + _Time.x * _Multiplier);
				float2 disp = tex2D(_DistortionTex, dv).xy;
				disp = ((disp * 2) - 1) * _Intensity;

				float4 col = tex2D(_MainTex, i.uv + disp);
				return col;
			}
			ENDCG
		}
	}
}
