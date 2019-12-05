Shader "Custom/DistortKaleidoscope"
{
	// Code obtained and modified from http://unity3d.ru/distribution/viewtopic.php?f=35&t=2783

	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
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

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = v.uv;
		return o;
	}

	sampler2D _MainTex;
	float _Intensity;
	float _Multiplier;
	float4 _Color;

	fixed4 frag(v2f i) : SV_Target
	{

		float2 position = i.uv;

		//fixed4 col = tex2D(_MainTex, position);

		half2 resolution = half2(1.0,1.0);

		half2 p = -1.0 + 2.0 * i.uv / resolution.xy;
		half2 uv;

		float a = atan2(p.y,p.x);
		float r = -1 * sqrt(dot(p,p));

		uv.x = _Intensity * a / 3.1416;
		uv.y = -_Time.x + sin(_Intensity*r + _Time.y) + .7*cos(_Time.y + _Intensity * a);

		float w = .8 + .5*(sin(_Time.y + _Intensity * r) + .7*cos(_Time.y + _Intensity * a));

		float3 col = tex2D(_MainTex, uv*_Multiplier).xyz;

		half4 color = half4(col*w,1.0);
		color = lerp(color, _Color, color.b);
		return color;
	}
		ENDCG
	}
	}
}
