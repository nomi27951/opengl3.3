#version 330 core

struct Material
{
	sampler2D diffuse;
	sampler2D specular;


	float shininess;
};

struct Light
{
	vec3 lightPos;
	vec3 direction;

	vec3 diffuse;
	vec3 specular;
	vec3 ambient;

	//attenuation
	float constant;
	float linear;
	float quadratic;
};

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

uniform vec3 viewPos;

uniform Material material;
uniform Light light;

out vec4 FragColor;

void main()
{
	vec3 ambient = light.ambient * texture(material.diffuse,TexCoords).rgb;

	vec3 norm = normalize(Normal);

	//point light
	vec3 lightDir = normalize(light.lightPos - FragPos);
	//directional light
	//vec3 lightDir = normalize(-light.direction);
	float diff = max(dot(lightDir,norm),0.0);

	vec3 diffuse = light.diffuse * diff * texture(material.diffuse,TexCoords).rgb;

	vec3 reflectDir = reflect(-lightDir,norm);
	vec3 viewDir = normalize(viewPos - FragPos);
	float spec = pow(max(dot(reflectDir,viewDir),0.0),material.shininess);

	vec3 specular = light.specular * spec * texture(material.specular,TexCoords).rgb;

	float distance = length(light.lightPos - FragPos);
	float attenuation = 1/(light.constant + light.linear * distance + light.quadratic * (distance * distance));

	FragColor = vec4((ambient * attenuation) + (diffuse * attenuation) + (specular * attenuation),1.0);
}

