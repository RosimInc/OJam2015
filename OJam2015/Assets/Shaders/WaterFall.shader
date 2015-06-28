// http://wiki.unity3d.com/index.php?title=Shader_Code

Shader "Pat Shaders/Waterfall"
{
	Properties
	{
		_WaterColorOn ("Water Color On", Color) = (0,0,0,0)
		_WaterColorOff ("Water Color Off", Color) = (1,1,1,1)
		_AlphaColor ("Alpha Color", Color) = (1,1,1,1)
		
		_MainTex ("Texture", 2D) = "white" {}

		_Ratio ("Ratio", Range(0,1)) = 0.5
		_UseInverseRatio ("Inverse Ratio", Float) = 0
	}
	
	SubShader
	{
		// http://docs.unity3d.com/460/Documentation/Manual/SL-SubshaderTags.html
		Tags
		{ 
			"Queue"="Transparent"
		}
	
		// http://docs.unity3d.com/Manual/SL-Blend.html
		Blend One OneMinusSrcAlpha
	
		Pass
		{
			CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				
				fixed4 _WaterColorOn;
				fixed4 _WaterColorOff;
				fixed4 _AlphaColor;
				float _Ratio;
				float _UseInverseRatio;
				sampler2D _MainTex;

				// Data transferred from application to vertex program
				struct vertexInput
				{
					float4 vertex : POSITION;
					float2 texcoord0 : TEXCOORD0; // Normalized, from 0 to 1
				};
				
				// Data transferred from vertex program to fragment program
				struct fragmentInput
				{
					float4 position : SV_POSITION;
					float2 texcoord0 : TEXCOORD0; // Normalized, from 0 to 1
				};

				// Vertex program
				fragmentInput vert(vertexInput i)
				{
					fragmentInput o;
					
					o.position = mul(UNITY_MATRIX_MVP, i.vertex); // Model*View*Projection matrix
					o.texcoord0 = i.texcoord0;

					return o;
				}

				// Fragment program
				fixed4 frag(fragmentInput i) : COLOR
				{
					float4 color;
					
					/*
					if (i.texcoord0.y > (2 - fmod(_Time.y, 2)) / 2)
					{
						color = _WaterColorOn * i.texcoord0.y + _WaterColorOff * (1 - i.texcoord0.y);
					}
					else
					{
						color = _WaterColorOff * i.texcoord0.y + _WaterColorOn * (1 - i.texcoord0.y);
					}*/
					
					if (_UseInverseRatio == 0)
					{
						if (i.texcoord0.y > _Ratio)
						{
							color = _WaterColorOn * i.texcoord0.y + _WaterColorOff * (1 - i.texcoord0.y);
						}
						else
						{
							color = _AlphaColor;
						}
					}
					else
					{
						if (i.texcoord0.y < _Ratio)
						{
							color = _WaterColorOn * i.texcoord0.y + _WaterColorOff * (1 - i.texcoord0.y);
						}
						else
						{
							color = _AlphaColor;
						}
					}
					
					
					
					

					
					color.rgb = color.rgb * color.a;
					
					return color;
				}

			ENDCG
		}
	}
}
