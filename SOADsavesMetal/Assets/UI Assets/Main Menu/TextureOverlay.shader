//DISTORTION OVERLAY. code parts borrowed from CatLikeCoding's tutorial on Flow - Texture Distortion Faking Liquid

Shader "Custom/Overlay Texture Distortion"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DistortionTex("Overlay Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
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
			sampler2D _NoiseTex;
			float _Intensity;
			float _Multiplier;
			float _Transparent;
			float4 _Color;


			float3 DistUV(float2 uv, float2 vect, float time, int flag) {
				float prog = frac(time + 0.5*flag);
				float3 uvw;
				uvw.xy = uv - vect * prog;
				uvw.z = 1 - abs(1 - 2 * prog);
				return uvw;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 dv = float2(i.uv.x, i.uv.y);
				float4 uv = tex2D(_NoiseTex, dv);

				float3 flow = DistUV(dv, uv.rgb, _Time.x, 1);
				float4 disp = tex2D(_DistortionTex, flow.xy)*flow.z;
				flow = DistUV(dv, uv.rgb, _Time.x, 0);
				float4 disp2 = tex2D(_DistortionTex, flow.xy)*flow.z;
				
				float4 col = tex2D(_MainTex, i.uv);
				col *= (disp+disp2) * _Intensity;
				col.rgb += (disp.rgb + disp2.rgb) * _Multiplier;
				col *= _Color;
				return col;
			}
			ENDCG
		}
	}
}
