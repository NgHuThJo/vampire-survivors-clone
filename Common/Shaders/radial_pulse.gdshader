shader_type canvas_item;

// uniform float radius : hint_range(0.0, 0.5) = 0.25;
// uniform float radius: hint_range(0.0, 0.5) = 0.3;
uniform float radius : hint_range(0.0, 0.5) = 0.4;
uniform float rimWidth : hint_range(0.0, 0.3) = 0.2;
uniform float softness: hint_range(0.0, 0.2) = 0.01;
uniform vec3 colorRim: source_color = vec3(0.02, 0.04, 0.51);
uniform vec3 colorInner: source_color = vec3(0.23, 0.35, 0.65);
uniform float breathingStrength: hint_range(0.0, 0.1, 0.001) = 0.02;
uniform float displacementStrength: hint_range(0.0, 0.2) = 0.05;
uniform vec2 noiseDirection = vec2(1.0, 1.0);
uniform float noiseSpeed : hint_range(0.0, 1.0) = 0.1;

void fragment() {
	vec3 portalColor = vec3(0.23, 0.35, 0.65);	
	vec2 center = vec2(0.5, 0.5);
	float oscillation = sin(TIME);
	vec2 sampleUV = UV + TIME * noiseDirection * noiseSpeed;
	float noiseDisplacement = texture(TEXTURE, sampleUV).r;
	float distanceFromCenter = distance(UV, center) + noiseDisplacement * displacementStrength + oscillation * breathingStrength;
	float maskOuter = 1.0 - smoothstep(radius - softness, radius, distanceFromCenter);
	float maskInner = 1.0 - smoothstep(rimWidth - softness, rimWidth, distanceFromCenter);
	float maskRim = maskOuter - maskInner;
	vec3 finalColor = maskRim * colorRim + maskInner * colorInner;

	// if (
	//   UV.y + 0.04 > oscillation / breathingStrength &&
	//   UV.y - 0.04 < oscillation / breathingStrength
	// ) {
	// 	COLOR = vec4(1.0, 0.0, 0.0, 1.0);
	// }
	COLOR = vec4(finalColor, maskOuter);

}


