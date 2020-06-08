// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline-Shader"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Offset("Outline Size", float) = 0.05
		//_Color("Outline Color", Color) = (1,1,1,1)
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
					float4 color : COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					float4 color: COLOR;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.color = v.color;
					return o;
				}

				sampler2D _MainTex;
				float _Offset;
				//float4 _Color;



				fixed4 frag(v2f i) : SV_Target
				{
					float4 col = tex2D(_MainTex, i.uv);

					float right = tex2D(_MainTex,  float2(i.uv.x + _Offset, i.uv.y)).a - col.a;
					float left = tex2D(_MainTex,  float2(i.uv.x - _Offset, i.uv.y)).a - col.a;
					float up = tex2D(_MainTex,  float2(i.uv.x, i.uv.y + _Offset)).a - col.a;
					float down = tex2D(_MainTex,  float2(i.uv.x, i.uv.y - _Offset)).a - col.a;

					float outline = clamp(right + left + up + down, 0, 1);
					
					float4 outlinecol = i.color * outline;


					col += outlinecol;

					return col;
				}
				ENDCG
			}
		}
}
