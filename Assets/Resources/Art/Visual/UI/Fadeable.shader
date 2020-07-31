// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Fadeable"
{
	Properties
	{
		_albedo("albedo", 2D) = "white" {}
		_opacity("opacity", Range( 0 , 1)) = 0.5
		_normal("normal", 2D) = "bump" {}
		_ao("ao", 2D) = "white" {}
		_metalicSmoothness("metalicSmoothness", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#pragma target 3.0
		#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
		#else
		#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
		#endif
		#pragma surface surf Standard keepalpha noshadow exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
			float4 screenPos;
		};

		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform sampler2D _normal;
		uniform float4 _normal_ST;
		uniform float _opacity;
		uniform sampler2D _albedo;
		uniform float4 _albedo_ST;
		ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
		uniform sampler2D _metalicSmoothness;
		uniform float4 _metalicSmoothness_ST;
		uniform sampler2D _ao;
		uniform float4 _ao_ST;


		inline float4 ASE_ComputeGrabScreenPos( float4 pos )
		{
			#if UNITY_UV_STARTS_AT_TOP
			float scale = -1.0;
			#else
			float scale = 1.0;
			#endif
			float4 o = pos;
			o.y = pos.w * 0.5f;
			o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
			return o;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 color37 = IsGammaSpace() ? float4(0.509804,0.4980392,1,0) : float4(0.223228,0.2122307,1,0);
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode38 = tex2D( _TextureSample0, uv_TextureSample0 );
			float2 uv_normal = i.uv_texcoord * _normal_ST.xy + _normal_ST.zw;
			float4 lerpResult40 = lerp( ( color37 * tex2DNode38 ) , float4( UnpackNormal( tex2D( _normal, uv_normal ) ) , 0.0 ) , _opacity);
			o.Normal = lerpResult40.rgb;
			float2 uv_albedo = i.uv_texcoord * _albedo_ST.xy + _albedo_ST.zw;
			float4 temp_output_56_0 = ( tex2DNode38 * 0.0 );
			float temp_output_42_0 = ( 1.0 - _opacity );
			float4 lerpResult61 = lerp( tex2D( _albedo, uv_albedo ) , temp_output_56_0 , temp_output_42_0);
			o.Albedo = lerpResult61.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 screenColor17 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,ase_grabScreenPos.xy/ase_grabScreenPos.w);
			float4 lerpResult20 = lerp( temp_output_56_0 , screenColor17 , temp_output_42_0);
			o.Emission = lerpResult20.rgb;
			float2 uv_metalicSmoothness = i.uv_texcoord * _metalicSmoothness_ST.xy + _metalicSmoothness_ST.zw;
			float4 tex2DNode23 = tex2D( _metalicSmoothness, uv_metalicSmoothness );
			float lerpResult49 = lerp( tex2DNode23.a , 0.0 , _opacity);
			o.Metallic = lerpResult49;
			float lerpResult50 = lerp( tex2DNode23.r , 0.0 , _opacity);
			o.Smoothness = lerpResult50;
			float2 uv_ao = i.uv_texcoord * _ao_ST.xy + _ao_ST.zw;
			float4 lerpResult51 = lerp( tex2D( _ao, uv_ao ) , tex2DNode38 , temp_output_42_0);
			o.Occlusion = lerpResult51.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17200
329;315;1906;941;1412.984;965.0482;2.030615;True;False
Node;AmplifyShaderEditor.SamplerNode;38;352,-864;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;-1;None;b6fbe64ee4beb0544afc35aa86e0b2a5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;336.8424,-133.3958;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-768,-256;Inherit;True;Property;_opacity;opacity;1;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-640,-320;Inherit;False;Constant;_Float1;Float 1;6;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;37;384,-1056;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;0.509804,0.4980392,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;487.4688,-25.75334;Inherit;True;Property;_albedo;albedo;0;0;Create;True;0;0;False;0;-1;None;10f5226420e69e04e8e006637f99917c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;24;509.764,951.5887;Inherit;True;Property;_ao;ao;3;0;Create;True;0;0;False;0;-1;None;f128ab52d00cb3a40ac97c23b96a42fd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;77.76398,855.5887;Inherit;False;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;42;-96,-240;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-82.236,679.5887;Inherit;True;Property;_metalicSmoothness;metalicSmoothness;4;0;Create;True;0;0;False;0;-1;None;39b21f23f53ae5443865cc2097126047;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;17;339.7254,-360.8966;Inherit;False;Global;_GrabScreen0;Grab Screen 0;1;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;593.0269,-161.7292;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;768,-1056;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;22;352,-640;Inherit;True;Property;_normal;normal;2;0;Create;True;0;0;False;0;-1;None;0aaeee52b748c4c479da7ab37d16c78e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;61;1076.516,0.6185374;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;50;589.764,791.5887;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;49;589.764,679.5887;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;51;1095.939,990.2297;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;60;118.113,1065.878;Inherit;False;Property;_test;test;6;0;Create;True;0;0;False;0;0;0.352;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;40;1072,-736;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;20;833.3961,-277.7303;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1466.544,-8.36882;Float;False;True;2;ASEMaterialInspector;0;0;Standard;Fadeable;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Translucent;0.5;True;False;0;False;Opaque;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;42;0;41;0
WireConnection;42;1;18;0
WireConnection;56;0;38;0
WireConnection;56;1;54;0
WireConnection;39;0;37;0
WireConnection;39;1;38;0
WireConnection;61;0;11;0
WireConnection;61;1;56;0
WireConnection;61;2;42;0
WireConnection;50;0;23;1
WireConnection;50;1;46;0
WireConnection;50;2;18;0
WireConnection;49;0;23;4
WireConnection;49;1;46;0
WireConnection;49;2;18;0
WireConnection;51;0;24;0
WireConnection;51;1;38;0
WireConnection;51;2;42;0
WireConnection;40;0;39;0
WireConnection;40;1;22;0
WireConnection;40;2;18;0
WireConnection;20;0;56;0
WireConnection;20;1;17;0
WireConnection;20;2;42;0
WireConnection;0;0;61;0
WireConnection;0;1;40;0
WireConnection;0;2;20;0
WireConnection;0;3;49;0
WireConnection;0;4;50;0
WireConnection;0;5;51;0
ASEEND*/
//CHKSM=66506F964D97A7AD5500E3FFBA5E19C66171EEFC