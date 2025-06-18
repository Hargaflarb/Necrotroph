#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Malthe

Texture2D SpriteTexture;
Texture2D ShadowTexture;
Texture2D Luminescense;

sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

sampler2D ShadowTextureSampler = sampler_state
{
    Texture = <ShadowTexture>;
};

sampler2D LuminescenseTextureSampler = sampler_state
{
    Texture = <Luminescense>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
	float4 color = tex2D(SpriteTextureSampler, input.TextureCoordinates);
    float3 shadowColor = tex2D(ShadowTextureSampler, input.TextureCoordinates).rgb;
    float3 lumiColor = tex2D(LuminescenseTextureSampler, input.TextureCoordinates).rgb;
    float luminescense = (lumiColor.r + lumiColor.g + lumiColor.b) / 3;
	
	color.a = (1-color.a) - luminescense;
    color.rgb = shadowColor;
	return color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};