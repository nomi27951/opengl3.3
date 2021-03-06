#version 330 core
struct Material {
	sampler2D diffuse;
	sampler2D specular;

	float shininess;
};

struct Light {
	vec3 ambient;
	vec3 diffuse;
	vec3 specular;

	vec3 lightPos;
};

in vec3 FragPos;
in vec3 Normal;
in vec2 TexCoords;

out vec4 FragColor;

uniform vec3 viewPos;
uniform vec3 objectColor;
uniform vec3 lightColor;

uniform Material material;
uniform Light light;

float ambientStrength = 0.1f;
float specularStrength = 0.5f;

void main()
{
	vec3 ambient = light.ambient * vec3(texture(material.diffuse,TexCoords));

	vec3 norm = normalize(Normal);
	vec3 lightDir = normalize(light.lightPos - FragPos);

	float diff = max(dot(norm,lightDir),0.0f);
	vec3 diffuse = vec3(texture(material.diffuse,TexCoords));

	//vec3 viewDir = normalize(viewPos - FragPos);
	//vec3 reflectDir = reflect(-lightDir,norm);

	//float spec = pow(max(dot(reflectDir,viewDir),0.0),material.shininess);

	//vec3 specular = light.specular * spec * vec3(texture(material.specular,TexCoords));

	vec3 result = ambient + diffuse; //+ specular;
	FragColor = vec4(result,1.0);
}