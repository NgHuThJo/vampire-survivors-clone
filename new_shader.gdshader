shader_type canvas_item;

const float radius = 0.5;

vec2 rotateVertex(vec2 vertex, float angle) {
	float newX = vertex.x * cos(angle) - vertex.y * sin(angle);
	float newY = vertex.x * sin(angle) + vertex.y * cos(angle);

	return vec2(newX, newY);
}

vec2 scaleVertex(vec2 vertex, float scalar){
	return scalar * vertex;
}

void vertex()
{
	// float wind = sin(TIME * 2.0);
	// float amplitude = 50.0;
	// scaleVertex(VERTEX, 2.0);
	// VERTEX += wind * amplitude;
	// VERTEX = rotateVertex(VERTEX, TIME * PI);
}

void fragment()
{
	// UV coords of each fragment shifted by (-0.5, -0.5)
	vec2 uv = UV - 0.5;
	// Distance from origin where the texture map center also is after the coord shift
	float distanceFromCenter = length(uv);	
	// 
	// float ringMask = step(radius, distanceFromCenter); 
	float ringMask = smoothstep(0.4, 0.5, distanceFromCenter); 
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
