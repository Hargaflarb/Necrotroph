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

static const float aspectRatio = 9.0 / 16.0;
static const float fadeLength = 0.3;
static const float resizer = 1.0 / fadeLength;


sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 pixelColor = float4(0,0,0,1);
    
    float2 pixelPosition = input.TextureCoordinates;
    float2 lightPosition = float2(0.5, 0.5);
    float1 lightRadius = float1(0.5);
    
    float distance = length(pixelPosition - lightPosition);
    pixelColor.a -= 1 - clamp((distance - (lightRadius - fadeLength)) * resizer, 0.0, 1.0);
    
    pixelColor.a = 1 - pixelColor.a;

    return pixelColor;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};