shader_type canvas_item;

// uniform float radius : hint_range(0.0, 0.5) = 0.25;
// uniform float radius: hint_range(0.0, 0.5) = 0.3;
uniform float radius : hint_range(0.0, 0.5) = 0.4;
uniform float rimWidth : hint_range(0.0, 0.3) = 0.2;
uniform float softness: hint_range(0.0, 0.2) = 0.01;
uniform vec3 colorRim: source_color = vec3(0.02, 0.04, 0.51);
uniform vec3 colorInner: source_color = vec3(0.23, 0.35, 0.65);

void fragment() {
	vec3 portalColor = vec3(0.23, 0.35, 0.65);	
	vec2 center = vec2(0.5, 0.5);
	float distanceFromCenter = distance(UV, center);
	float maskOuter = 1.0 - smoothstep(radius - softness, radius, distanceFromCenter);
	float maskInner = 1.0 - smoothstep(rimWidth - softness, rimWidth, distanceFromCenter);
	float maskRim = maskOuter - maskInner;
	vec3 finalColor = maskRim * colorRim + maskInner * colorInner;
	float portal = 1.0 - smoothstep(radius - softness, radius, distanceFromCenter);

	COLOR = vec4(finalColor, maskOuter);
}

