// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Distortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DistortionTex("Distortion Texture", 2D) = "white" {}
		_Intensity("Intensity", float) = 0.1
		_Multiplier("Multiplier", float) = 0.4
		_Color("Color", Color) = (1,1,1,1)
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
			float _Transparent;
			float4 _Color;

			float nrand(float x, float y)
			{
				return frac(sin(dot(float2(x, y), float2(12.9898, 78.233))) * 43758.5453);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 dv = float2(i.uv.x + sin(_Time.y)*_Multiplier, i.uv.y -nrand(i.uv.y, _Time.w)*_Multiplier);
				float2 disp = tex2D(_DistortionTex, dv).xy;
				disp = ((disp * 2) - 1) * _Intensity;

				float4 col = tex2D(_MainTex, i.uv + disp);
				if (col.a == 1.0f) col.a = ((sin(_Time.x*10) + 1) / 3 + 0.2f);
				col.rgb = (col.rgb + 1 - tex2D(_DistortionTex, dv).rgb) / 4;
				//col.b = _Color[1];
				return lerp(_Color, col, col.a);
				//return col;
			}
			ENDCG
		}
	}
}
