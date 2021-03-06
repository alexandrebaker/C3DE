// Matrix
float4x4 World;
float4x4 View;
float4x4 Projection;

// Material
float4 AmbientColor = float4(0.1, 0.1, 0.1, 1.0);
float4 DiffuseColor = float4(1.0, 1.0, 1.0, 1.0);
float4 EmissiveColor = float4(0.0, 0.0, 0.0, 1.0);

// Misc
float2 TextureTiling = float2(1, 1);
float2 TextureOffset = float2(0, 0);
float Time = 0;

texture MainTexture;
sampler2D textureSampler = sampler_state 
{
	Texture = (MainTexture);
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

texture BumpTexture;
sampler2D bumpSampler = sampler_state 
{
	Texture = (BumpTexture);
	MinFilter = Linear;
	MagFilter = Linear;
	MipFilter = Linear;
	AddressU = Wrap;
	AddressV = Wrap;
};

struct VertexShaderInput
{
#if SM4
	float4 Position : SV_Position;
#else
	float4 Position : POSITION0;
#endif
	float2 UV : TEXCOORD0;
};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 UV : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);
	output.Position = mul(viewPosition, Projection);
	output.UV = float2(0.5, 0.5) * input.UV;

	return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
	float3 fogColor = float3(0.0, 0.0, 0.0);
	float4 noise = tex2D(bumpSampler, (input.UV + TextureOffset) * TextureTiling);
	float2 T1 = input.UV + float2(1.5, -1.5) * Time * 0.02;
	float2 T2 = input.UV + float2(-0.5, 2.0) * Time * 0.01;
	
	T1.x += noise.x * 2.0;
	T1.y += noise.y * 2.0;
	T2.x -= noise.y * 0.2;
	T2.y += noise.z * 0.2;
	
	float p = tex2D(bumpSampler, ((T1 * 3.0) + TextureOffset) * TextureTiling).a;
	
	float4 color = tex2D(textureSampler, ((T2 * 4.0) + TextureOffset) * TextureTiling);
	float4 temp = color * (float4(p, p, p, p) * 2.0) + (color * color - 0.1);
	
	if (temp.r > 1.0)
		temp.bg += clamp(temp.r - 2.0, 0.0, 100.0);
	
	if (temp.g > 1.0)
		temp.rb += temp.g - 1.0;
	
	if (temp.b > 1.0)
		temp.rg += temp.b - 1.0;
		
	return AmbientColor + (DiffuseColor * temp) + EmissiveColor;
}

technique Basic
{
	pass Pass1
	{
#if SM4
		VertexShader = compile vs_4_0_level_9_1 VertexShaderFunction();
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
#else
		VertexShader = compile vs_3_0 VertexShaderFunction();
		PixelShader = compile ps_3_0 PixelShaderFunction();
#endif
	}
}